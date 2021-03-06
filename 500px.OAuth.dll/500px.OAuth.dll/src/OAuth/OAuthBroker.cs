﻿#define DEBUG

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OAuth
{
    /// <summary>
    ///     Объект, представляющий доступ для аутентификации по протоколу OAuth 1.0
    /// </summary>
    public partial class OAuthBroker
    {
        /// <summary>
        ///     Конструктор
        /// </summary>
        public OAuthBroker()
        {
            Token = new OAuthToken();
            AccessToken = new OAuthToken();
            RequestParameters = new List<RequestParameter>();
        }

        /// <summary>
        ///     Возвращает и задает Токен
        /// </summary>
        public OAuthToken Token { get; set; }

        /// <summary>
        ///     Возвращает AccessToken
        /// </summary>
        public OAuthToken AccessToken { get; set; }

        /// <summary>
        ///     Возвращает состояние аутентификации
        /// </summary>
        public bool IsAuth => !string.IsNullOrWhiteSpace(AccessToken.Secret) &&
                              !string.IsNullOrWhiteSpace(AccessToken.Token);

        /// <summary>
        ///     Возвращает состояние регистрации клиента
        /// </summary>
        public bool IsRegistred => !string.IsNullOrWhiteSpace(Consumer.ConsumerKey) &&
                                   !string.IsNullOrWhiteSpace(Consumer.ConsumerSecret) &&
                                   !string.IsNullOrWhiteSpace(Consumer.CallbackUrl);

        /// <summary>
        ///     Возвращает данные пользовательской программы
        /// </summary>
        public ConsumerInfo Consumer { get; private set; }

        /// <summary>
        ///     Возвращает или задает список параметров запроса
        /// </summary>
        public List<RequestParameter> RequestParameters { get; set; }

        /// <summary>
        ///     Регистриует клиент атентификации 500px.com
        ///     <example>
        ///         <code>
        /// ConsumerInfo consumer = new ConsumerInfo("consumerKey", "consumerSecret", "callbackUrl");
        /// OAuthBroker client = new OAuthBroker().RegisterClient("name", consumer);
        ///         </code>
        ///     </example>
        /// </summary>
        /// <param name="consumerInfo">
        ///     <see cref="ConsumerInfo" />
        ///     информация о клиентском приложении
        /// </param>
        /// <returns>
        ///     объект клиента аутентификации 500px.com
        /// </returns>
        public OAuthBroker RegisterClient(ConsumerInfo consumerInfo)
        {
            try
            {
                if (consumerInfo.IsEmpty) throw new ArgumentNullException("consumerInfo");
                Consumer = consumerInfo;
            }
            catch (ArgumentNullException exception)
            {
                MessageBox.Show(exception.Message);
            }
            return this;
        }

        /// <summary>
        ///     Получает request token
        /// </summary>
        /// <returns>
        ///     задача, получения request token
        /// </returns>
        public async Task<OAuthToken> GetRequestTokenAsync()
        {
            // возвращаемый токен
            var token = new OAuthToken();
            // определяем параметры запроса
            RequestParameters?.Clear();
            RequestParameters = new List<RequestParameter>
            {
                new RequestParameter(OAuthParam.Callback, Consumer.CallbackUrl),
                new RequestParameter(OAuthParam.ConsumerKey, Consumer.ConsumerKey),
                new RequestParameter(OAuthParam.Nonce, OAuthHelpers.GenerateNonce()),
                new RequestParameter(OAuthParam.SignatureMethod, OAuthConst.OAuthSignatureMethod),
                new RequestParameter(OAuthParam.Timestamp, OAuthHelpers.GenerateTimeStamp()),
                new RequestParameter(OAuthParam.Version, OAuthConst.OAuthVersion)
            };

            // подписываем строку запроса
            var requestUrl = Core.Sign(HttpMethods.Post, OAuthConst.RequestUrl, RequestParameters, Consumer,
                null);
            // инициализируем HTTP Client
            using (var httpClient = new HttpClient())
            {
                // осуществляем запрос
                var response = await httpClient.PostAsync(requestUrl, null);
                //проверяем результат
                if (response.StatusCode == HttpStatusCode.Unauthorized ||
                    response.StatusCode == HttpStatusCode.InternalServerError) return token;
                if (response.IsSuccessStatusCode)
                {
                    //асинхронное чтение ответа сервера
                    var contentResponse = await response.Content.ReadAsStringAsync();
                    //"режем" по амперсанду
                    var responsesegments = contentResponse.Split('&');
                    if (responsesegments.Length <= 0) return token;
                    foreach (var item in responsesegments.Select(responsesegment => responsesegment.Split('=')))
                    {
                        switch (item[0])
                        {
                            case "oauth_token":
                                //возвращаеи токен
                                token.Token = item[1];
                                break;
                            case "oauth_token_secret":
                                //возвращаем секрет
                                token.Secret = item[1];
                                break;
                        }
                    }
                }
            }
#if DEBUG
            Debug.Indent();
            Debug.WriteLineIf(!string.IsNullOrEmpty(token.Secret) && (!string.IsNullOrEmpty(token.Token)),
                string.Format("Token: {0}, Secret: {1}", token.Token, token.Secret));
            Debug.Unindent();
            Debug.WriteLine("Request token complete!");
#endif
            Token = token;
            return token;
        }

        /// <summary>
        ///     возвращает строку ресурса для авторизации пользователя
        /// </summary>
        /// <param name="requestToken">
        ///     request token
        /// </param>
        /// <returns>
        ///     возвращает строку ресурса для авторизации пользователя
        /// </returns>
        public string GetAuthorizationUrl(OAuthToken requestToken)
        {
            RequestParameters?.Clear();
            RequestParameters = new List<RequestParameter>
            {
                new RequestParameter(OAuthParam.Callback, Consumer.CallbackUrl),
                new RequestParameter(OAuthParam.Token, requestToken.Token)
            };
            var resultString = Core.Sign(HttpMethods.Post, OAuthConst.AuthorizeUrl, RequestParameters,
                Consumer, requestToken);
#if DEBUG
            Debug.WriteLine(string.Format("Authorize Url:  {0}", resultString));
#endif
            return resultString;
        }

        /// <summary>
        ///     Получает Access Token
        /// </summary>
        /// <param name="requestToken">токен</param>
        /// <returns>
        ///     задача, получения токена
        /// </returns>
        public async Task<OAuthToken> GetAccessTokenAsync(OAuthToken requestToken)
        {
            RequestParameters?.Clear();
            RequestParameters = new List<RequestParameter>
            {
                new RequestParameter(OAuthParam.Callback, Consumer.CallbackUrl),
                new RequestParameter(OAuthParam.ConsumerKey, Consumer.ConsumerKey),
                new RequestParameter(OAuthParam.Nonce, OAuthHelpers.GenerateNonce()),
                new RequestParameter(OAuthParam.SignatureMethod, OAuthConst.OAuthSignatureMethod),
                new RequestParameter(OAuthParam.Timestamp, OAuthHelpers.GenerateTimeStamp()),
                new RequestParameter(OAuthParam.Version, OAuthConst.OAuthVersion),
                new RequestParameter(OAuthParam.Verifier, Token.Verifier),
                new RequestParameter(OAuthParam.Token, Token.Token)
            };

            var signature = Core.Sign(HttpMethods.Post, OAuthConst.AccessUrl, RequestParameters, Consumer,
                requestToken);
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(signature, null);
                if (response == null || !response.IsSuccessStatusCode) return AccessToken;
                var content = await response.Content.ReadAsStringAsync();
                var parameters = content.Split('&');
                foreach (var parameterParts in parameters.Select(parameter => parameter.Split('=')))
                {
                    switch (parameterParts[0])
                    {
                        case "oauth_token":
                            AccessToken.Token = parameterParts[1];
                            break;
                        case "oauth_token_secret":
                            AccessToken.Secret = parameterParts[1];
                            break;
                    }
                }
            }
#if DEBUG
            Debug.Indent();
            Debug.WriteLineIf(!string.IsNullOrEmpty(AccessToken.Secret) && (!string.IsNullOrEmpty(AccessToken.Token)),
                string.Format("Access Token: {0}, Secret: {1}", AccessToken.Token, AccessToken.Secret));
            Debug.Unindent();
            Debug.WriteLine("Access token complete!");
#endif
            return AccessToken;
        }

        /// <summary>
        /// </summary>
        /// <param name="url"></param>
        /// <returns>
        /// Объект брокера авторизации
        /// </returns>
        public OAuthBroker GetVerifier(Uri url)
        {
            var urlpart = url.OriginalString.Split('?');
            if (urlpart[0] == OAuthConst.AuthorizeUrl) return this;
            var param = url.Query;
            var part = param.Split('&');
            foreach (var a in part.Select(s => s.Split('=')))
            {
                switch (a[0])
                {
                    case "oauth_token":
                        Token.Token = a[1];
                        break;
                    case "oauth_token_secret":
                        Token.Secret = a[1];
                        break;
                    case "oauth_verifier":
                        Token.Verifier = a[1];
                        break;
                }
            }
            return this;
        }
    }
}