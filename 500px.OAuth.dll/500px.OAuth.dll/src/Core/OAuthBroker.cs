using System;

namespace OAuth
{
    public partial class OAuthBroker
    {
        /// <summary>
        ///     Информация о пользовательском приложении
        /// </summary>
        public struct ConsumerInfo
        {
            /// <summary>
            ///     "Пустой consumer info"
            /// </summary>
            public static readonly ConsumerInfo Empty;

            /// <summary>
            ///     Хранит информацию о пользовательском приложении
            /// </summary>
            /// <param name="consumerKey">
            ///     ключ приложения
            /// </param>
            /// <param name="consumerSecret">
            ///     секрет приложения
            /// </param>
            /// <param name="callbackUrl">
            ///     валидный Url
            /// </param>
            /// <exception cref="ArgumentNullException"></exception>
            public ConsumerInfo(string consumerKey, string consumerSecret, string callbackUrl)
            {
                if (string.IsNullOrWhiteSpace(consumerKey)) throw new ArgumentNullException("consumerKey");
                if (string.IsNullOrWhiteSpace(consumerSecret)) throw new ArgumentNullException("consumerSecret");
                if (string.IsNullOrWhiteSpace(callbackUrl)) throw new ArgumentNullException("callbackUrl");
                ConsumerKey = consumerKey;
                ConsumerSecret = consumerSecret;
                CallbackUrl = callbackUrl;
            }

            /// <summary>
            ///     возвращает правда, если структура пустая
            /// </summary>
            public bool IsEmpty => string.IsNullOrWhiteSpace(CallbackUrl) &&
                                   string.IsNullOrWhiteSpace(ConsumerKey) &&
                                   string.IsNullOrWhiteSpace(ConsumerSecret);

            /// <summary>
            ///     возвращает ключ приложения
            /// </summary>
            public string ConsumerKey { get; }

            /// <summary>
            ///     возвращает секрет приложения
            /// </summary>
            public string ConsumerSecret { get; }

            /// <summary>
            ///     возвращает валидный Url, используемый при аутентификации
            /// </summary>
            public string CallbackUrl { get; }
        }
    }
}