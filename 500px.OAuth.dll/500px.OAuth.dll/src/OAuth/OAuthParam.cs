namespace OAuth
{
    public partial class OAuthBroker
    {


        /// <summary>
        ///     Параметры запроса, используемые при OAuth 1.0a аутентификации
        /// </summary>
        public static class OAuthParam
        {
            /// <summary>
            ///     валидный Url
            /// </summary>
            public const string Callback = "oauth_callback";

            /// <summary>
            ///     ключ приложения
            /// </summary>
            public const string ConsumerKey = "oauth_consumer_key";

            /// <summary>
            ///     случайная строка 32 бита
            /// </summary>
            public const string Nonce = "oauth_nonce";

            /// <summary>
            ///     префикс для формирования параметра
            /// </summary>
            public const string Prefix = "oauth_";

            /// <summary>
            ///     подпись
            /// </summary>
            public const string Signature = "oauth_signature";

            /// <summary>
            ///     метод подписывания запроса
            /// </summary>
            public const string SignatureMethod = "oauth_signature_method";

            /// <summary>
            ///     интервал времени в секундах от начала времени Unix до формирования текущего запроса
            /// </summary>
            public const string Timestamp = "oauth_timestamp";

            /// <summary>
            ///     Токен
            /// </summary>
            public const string Token = "oauth_token";

            /// <summary>
            ///     верификатор
            /// </summary>
            public const string Verifier = "oauth_verifier";

            /// <summary>
            ///     версия OAuth
            /// </summary>
            public const string Version = "oauth_version";
        }
    }
}