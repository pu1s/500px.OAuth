namespace ags.OAuth
{
    /// <summary>
    ///     Константы для создания запроса
    /// </summary>
    public static class OAuthConst
    {
        /// <summary>
        ///     адрес для получения токена доступа к защищенным ресурсам
        /// </summary>
        public const string AccessUrl = "https://api.500px.com/v1/oauth/access_token";

        /// <summary>
        ///     адрес для авторизации пользователя
        /// </summary>
        public const string AuthorizeUrl = "https://api.500px.com/v1/oauth/authorize";

        /// <summary>
        ///     адрес для получения токена запроса
        /// </summary>
        public const string RequestUrl = "https://api.500px.com/v1/oauth/request_token";

        /// <summary>
        ///     название метода шифрования подписи
        /// </summary>
        public const string OAuthSignatureMethod = "HMAC-SHA1";

        /// <summary>
        ///     версия протокола OAuth
        /// </summary>
        public const string OAuthVersion = "1.0";
    }
}