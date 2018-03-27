using System;
using System.Collections.Generic;

namespace AsyncNet.Common
{
    public static class EnumerableExtensions
    {
        public static void Each<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action == null || source == null)
            {
                return;
            }

            foreach (var item in source)
            {
                action(item);
            }
        }
    }
}
