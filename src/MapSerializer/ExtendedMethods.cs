using System;
using System.Collections;
using System.Collections.Generic;

namespace MapSerializer
{
    internal static class ExtendedMethods
    {
        public static string ToDateTimeString(this object value)
        {
            return ((DateTime)value).ToString("o");
        }

        public static int Count(this IEnumerable enumerable)
        {
            int res = 0;
            foreach (var item in enumerable)
                res++;

            return res;
        }

        public static void ForEachAndBetween(this IEnumerable enumerable, Action<object> each, Action between)
        {
            var count = enumerable.Count();
            var index = 0;

            foreach (var item in enumerable)
            {
                each.Invoke(item);

                index++;
                if (index < count)
                    between.Invoke();
            }
        }

        public static void ForEachAndBetween<T>(this IEnumerable<T> enumerable, Action<T> each, Action between)
        {
            var count = enumerable.Count();
            var index = 0;

            foreach (var item in enumerable)
            {
                each.Invoke(item);

                index++;
                if (index < count)
                    between.Invoke();
            }
        }
    }
}
