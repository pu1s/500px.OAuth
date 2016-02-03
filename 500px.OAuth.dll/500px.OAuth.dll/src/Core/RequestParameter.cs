namespace ags.Core
{
    /// <summary>
    ///     Параметр запроса
    /// </summary>
    public class RequestParameter
    {
        /// <summary>
        ///     Конструктор по умолчанию
        /// </summary>
        /// <param name="name">имя папаметра</param>
        /// <param name="value">значение параметра</param>
        public RequestParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        ///     Имя параметра
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Значение параметра
        /// </summary>
        public string Value { get; set; }
    }
}