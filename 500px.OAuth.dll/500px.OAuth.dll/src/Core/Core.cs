using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using OAuth.OAuth;

namespace OAuth
{
    public partial class OAuthBroker
    {


        public class Core
        {
            /// <summary>
            ///     Подписывает запрос
            /// </summary>
            /// <param name="httpMethod">метод, используемый для запроса</param>
            /// <param name="requestUrl">адрес для запроса</param>
            /// <param name="requestParameters">параметры запроса</param>
            /// <param name="consumerInfo">информация о программе клиенте</param>
            /// <param name="token">токен</param>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="ArgumentOutOfRangeException"></exception>
            /// <returns>
            ///     подписанная сторока запроса
            /// </returns>
            public static string Sign(HttpMethods httpMethod, string requestUrl,
                List<RequestParameter> requestParameters,
                ConsumerInfo consumerInfo,
                OAuthBroker.OAuthToken token)
            {
                if (consumerInfo.CallbackUrl == null && consumerInfo.ConsumerKey == null &&
                    consumerInfo.ConsumerSecret == null) throw new ArgumentNullException("consumerInfo");
                // Подготовка и нормализация операндов для подписи
                // Определяем метод запроса
                if ((httpMethod < 0) && (httpMethod > (HttpMethods) 3))
                    throw new ArgumentOutOfRangeException("httpMethod");
                var baseHttpMethod = httpMethod.ToString().ToUpper() + "&";
                // определяем и нормализуем Url запроса
                if (string.IsNullOrWhiteSpace(requestUrl)) throw new ArgumentNullException("requestUrl");
                var baseRequesUrl = Uri.EscapeDataString(requestUrl) + "&";
                // Определяем и нормализуем параметры запроса
                if (requestParameters == null) throw new ArgumentNullException("requestParameters");
                // нормализуем и сортируем список параметров
                requestParameters = NormalizeRequestParameters(requestParameters);

                var baseParameters = string.Join("&",
                    requestParameters.Select(param => param.Name + "=" + param.Value));
                //строка для подписывания
                var baseRequestString = baseHttpMethod + baseRequesUrl + Uri.EscapeDataString(baseParameters);
#if DEBUG
                Debug.WriteLine(string.Format("Подписываемая строка {0}", baseRequestString));
#endif

                // Определяем ключ для подписи
                var baseKey = token == null
                    ? consumerInfo.ConsumerSecret + "&" + string.Empty
                    : consumerInfo.ConsumerSecret + "&" + token.Secret;
                // осуществляем шифрование
                var encoder = new HMACSHA1 {Key = Encoding.ASCII.GetBytes(baseKey)};
                var buffer = Encoding.ASCII.GetBytes(baseRequestString);
                var signatureByte = encoder.ComputeHash(buffer, 0, buffer.Length);
                // получаем стороку подписи в представлении Base64
                var sigmature = Convert.ToBase64String(signatureByte);
#if DEBUG
                Debug.WriteLine(string.Format("Подписанная строка {0}", sigmature));
#endif

                /* Формируем подписанную строку запроса */

                // Для формирования стоки запроса к параметрам добавляем параметр с цифровой подписью
                requestParameters.Add(new RequestParameter(OAuthBroker.OAuthParam.Signature,
                    NormalizeParameter(sigmature)));
                // формируем строку
                var signedUrlString = requestUrl + "?" +
                                      string.Join("&", requestParameters.Select(param => param.Name + "=" + param.Value));
#if DEBUG
                Debug.WriteLine(string.Format("Подписанная строка запроса {0}", signedUrlString));
#endif

                // возвращаем подписанную строку
                return signedUrlString;
            }

            /// <summary>
            ///     Нормализует значение параметра
            /// </summary>
            /// <param name="str">строка параметра</param>
            /// <returns>
            ///     нормализованная строка параметра
            /// </returns>
            private static string NormalizeParameter(string str) => WebUtility.UrlEncode(str);

            /// <summary>
            ///     Нормализует параметры запроса
            /// </summary>
            /// <param name="requestParameters"></param>
            /// <returns>
            ///     список нормализованных параметров запроса
            /// </returns>
            private static List<RequestParameter> NormalizeRequestParameters(List<RequestParameter> requestParameters)
            {
                var tempList =
                    requestParameters.Select(
                        requestParameter =>
                            new RequestParameter(requestParameter.Name, NormalizeParameter(requestParameter.Value)))
                        .ToList();
                tempList.Sort(new Comparer());
                requestParameters.Clear();
                requestParameters = tempList;
                return requestParameters;
            }
        }
    }
}