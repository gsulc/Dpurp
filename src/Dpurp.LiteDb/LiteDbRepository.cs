using LiteDB;
using System;
using System.Collections.Generic;

namespace Dpurp.LiteDb
{
    public class LiteDbRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private readonly LiteDbContext _context;
        private LiteCollection<TItem> _items;

        public LiteDbRepository(LiteDbContext context)
        {
            _context = context;
        }

        protected LiteCollection<TItem> Items => _items ?? (_items = _context.Set<TItem>());

        public void Add(TItem item)
        {
            Items.Insert(item);
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            Items.Insert(items);
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return Items.Find(x => predicate(x));
        }

        public TItem Get(int id)
        {
            return Items.FindById(id);
        }

        public IEnumerable<TItem> GetAll()
        {
            return Items.FindAll();
        }

        public void Remove(TItem item)
        {
            Items.Delete(i => i == item);
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Remove(item);
        }

        public void SaveChanges()
        {
            Items.Update(_items.FindAll()); // yuck!
        }
    }
}
