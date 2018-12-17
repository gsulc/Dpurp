using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dpurp
{
    public static class ListExtensions
    {
        public static IList ToGenericList<T>(this IList<T> list)
        {
            return list.AsEnumerable().ToList();
            //var genericList = new List();
            //foreach (var item in list)
            //    genericList.Add(item);
            //return genericList;
        }
    }
}
