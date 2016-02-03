using System;

namespace ags.Core
{
    /// <summary>
    ///     Информация о пользовательском приложении
    /// </summary>
    public struct ConsumerInfo
    {
        public static readonly ConsumerInfo Empty;
        private readonly string _callbackUrl;
        private readonly string _consumerKey;
        private readonly string _consumerSecret;

        /// <summary>
        ///     Хранит информацию о пользовательском приложении
        /// </summary>
        /// <param name="consumerKey">ключ приложения</param>
        /// <param name="consumerSecret">секрет приложения</param>
        /// <param name="callbackUrl">валидный Url</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ConsumerInfo(string consumerKey, string consumerSecret, string callbackUrl)
        {
            if (string.IsNullOrWhiteSpace(consumerKey)) throw new ArgumentNullException("ConsumerKey");
            if (string.IsNullOrWhiteSpace(consumerSecret)) throw new ArgumentNullException("ConsumerSecret");
            if (string.IsNullOrWhiteSpace(callbackUrl)) throw new ArgumentNullException("ConsumerKey");
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
            _callbackUrl = callbackUrl;
        }

        /// <summary>
        ///     возвращает правда, если структура пустая
        /// </summary>
        public bool IsEmpty => string.IsNullOrWhiteSpace(_callbackUrl) && string.IsNullOrWhiteSpace(_consumerKey) &&
                               string.IsNullOrWhiteSpace(_consumerSecret);

        /// <summary>
        ///     возвращает ключ приложения
        /// </summary>
        public string ConsumerKey
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_consumerKey)) throw new ArgumentNullException("ConsumerKey");
                return _consumerKey;
            }
        }

        /// <summary>
        ///     возвращает секрет приложения
        /// </summary>
        public string ConsumerSecret
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_consumerSecret)) throw new ArgumentNullException("ConsumerSecret");
                return _consumerSecret;
            }
        }

        /// <summary>
        ///     возвращает валидный Url, используемый при аутентификации
        /// </summary>
        public string CallbackUrl
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_callbackUrl)) throw new ArgumentNullException("ConsumerKey");
                return _callbackUrl;
            }
        }
    }
}