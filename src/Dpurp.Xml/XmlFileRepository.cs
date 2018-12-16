using System;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp.Xml
{
    public class XmlFileRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private XmlFileContext _xmlFileContext;
        private IList<TItem> _items;

        public XmlFileRepository(XmlFileContext xmlFileContext)
        {
            _xmlFileContext = xmlFileContext;
        }

        public IList<TItem> Items
        {
            get
            {
                return _items ?? (_items = _xmlFileContext.Set<TItem>());
            }
        }

        public void SaveChanges()
        {
            _xmlFileContext.SaveChanges(Items);
        }

        public void Add(TItem item)
        {
            Items.Add(item);
            SaveChanges();
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Items.Add(item);
            SaveChanges();
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public TItem Get(int id)
        {
            return Items[id];
        }

        public IEnumerable<TItem> GetAll()
        {
            return Items.AsEnumerable();
        }

        public void Remove(TItem item)
        {
            Items.Remove(item);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Items.Remove(item);
            SaveChanges();
        }
    }
}
