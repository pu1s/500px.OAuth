using System;
using System.Collections.Generic;

namespace ags.Core
{
    /// <summary>
    ///     Компраматор для сравнения элементов списка
    /// </summary>
    public class Comparer : IComparer<RequestParameter>
    {
        /// <summary>
        ///     Сравнивает 2 параметра
        /// </summary>
        /// <param name="param1">параметр 1</param>
        /// <param name="param2">параметр 2</param>
        /// <returns>
        ///     0 - если строки равны, отрицательное число, если первая сторока меньше второй, положительное число, если
        ///     наоборот
        /// </returns>
        public int Compare(RequestParameter param1, RequestParameter param2)
        {
            return string.Compare(param1.Name, param2.Name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}