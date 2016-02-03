namespace ags.OAuth
{
    /// <summary>
    ///     Токен, используемый при аутентификации
    /// </summary>
    public class OAuthToken
    {
        /// <summary>
        ///     Токен
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        ///     Секрет
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        ///     Верификатор
        /// </summary>
        public string Verifier { get; set; }
    }
}