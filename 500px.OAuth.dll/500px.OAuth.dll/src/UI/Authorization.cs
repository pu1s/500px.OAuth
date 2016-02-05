using System.Threading.Tasks;
using OAuth.UI;

namespace OAuth
{
    /// <summary>
    ///     Осуществляет запрос авторизации
    /// </summary>
    public static class Authorization
    {
        private static OAuthBroker _broker;

        /// <summary>
        ///     Обновляет Access Token
        /// </summary>
        /// <param name="broker">
        ///     экземпляр класса реализации протокола OAuth 1.0
        /// </param>
        public static void RealiseToken(OAuthBroker broker)
        {
            _broker = broker;
            // асинхронный запрос токена
            _broker.Token = Task.Run(async () => await _broker.GetRequestTokenAsync()).GetAwaiter().GetResult();
            // получение строки адреса авторизации пользователя
            var url = _broker.GetAuthorizationUrl(_broker.Token);
            // вызов формы авторизации пользователя
            var form = new UserAuthForm(_broker, url);
            // показываем форму
            form.Navigate();
            // асинхронный запрос Access Token
            _broker.AccessToken =
                Task.Run(async () => await _broker.GetAccessTokenAsync(_broker.Token)).GetAwaiter().GetResult();
        }
    }
}