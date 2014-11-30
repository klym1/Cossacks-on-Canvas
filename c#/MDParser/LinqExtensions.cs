using System.Collections.Generic;

namespace MDParser
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> SelectEven<T>(this IEnumerable<T> enumerable)
        {
            bool even = true;

            foreach (var item in enumerable)
            {
                if (even)
                    yield return item;

                even =!even;
            }
        }

        public static IEnumerable<T> SelectOdd<T>(this IEnumerable<T> enumerable)
        {
            bool odd = false;

            foreach (var item in enumerable)
            {
                if (odd)
                    yield return item;

                odd = !odd;
            }
        }
    }
}