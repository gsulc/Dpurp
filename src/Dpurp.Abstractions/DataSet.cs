using System;
using System.Collections;
using System.Collections.Generic;

namespace Dpurp
{
    public abstract class DataSet<TItem> : IEnumerable<TItem>, IEnumerable where TItem : class
    {
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
        IDictionary<Type, IList<object>> Sets { get; } = new Dictionary<Type, IList<object>>();

        public IList<object> this[Type type]
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
