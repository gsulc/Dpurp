using System;
using System.Collections.Generic;

namespace Dpurp
{
    public interface IRepository<TItem> where TItem : class
    {
        TItem Get(int id);
        IEnumerable<TItem> GetAll();
        IEnumerable<TItem> Find(Func<TItem, bool> predicate);
        void Add(TItem item);
        void AddRange(IEnumerable<TItem> items);
        void Remove(TItem item);
        void RemoveRange(IEnumerable<TItem> items);
        void SaveChanges();
    }
}
