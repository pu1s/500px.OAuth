using System;

namespace ags.OAuth
{
    /// <summary>
    ///     Функции для вычисления переменных параметров запроса
    /// </summary>
    public static class OAuthHelpers
    {
        /// <summary>
        ///     Вычисляет Nonce
        /// </summary>
        /// <returns>
        ///     случайная строка
        /// </returns>
        public static string GenerateNonce() => new Random().Next(1234567, 9999999).ToString();

        /// <summary>
        ///     Вычисляет промежуток времени от начала исчисления универсального Unix времени до времени генерации запроса в
        ///     секундах
        /// </summary>
        /// <returns>
        ///     строка, представляет количество секунд прошедших от начала времени Unix
        /// </returns>
        public static string GenerateTimeStamp()
        {
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
