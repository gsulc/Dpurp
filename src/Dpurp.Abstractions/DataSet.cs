using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp
{
    public class DataSet
    {

    }

    public abstract class DataSet<TItem> : IEnumerable<TItem>, IEnumerable where TItem : class
    {
        private IList<TItem> _items;

        public IEnumerator<TItem> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class DataSets
    {
        IDictionary<Type, IList> Sets { get; } = new Dictionary<Type, IList>();

        public IList<object> this[Type type]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IList<TItem> GetItems<TItem>()
        {
            return (Sets as IEnumerable<TItem>).ToList();
        }
    }
}
