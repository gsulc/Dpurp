using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp
{
    public static class ListExtensions
    {
        public static IList ToGenericList<T>(this IList<T> list)
        {
            return list.AsEnumerable().ToList();
        }

        public static IList<T> ToSpecificList<T>(this IList list)
        {
            return list.AsEnumerable<T>().ToList();
        }

        public static IEnumerable<T> AsEnumerable<T>(this IList list)
        {
            foreach (var item in list)
                yield return (T)item;
        }
    }
}
