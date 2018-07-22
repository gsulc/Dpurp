using Dpurp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp.Xml
{
    public class XmlFileRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private XmlFileContext _xmlFileContext;
        private IList<TItem> _xmlSet;

        public XmlFileRepository(XmlFileContext xmlFileContext)
        {
            _xmlFileContext = xmlFileContext;
        }

        public IList<TItem> XmlSet
        {
            get
            {
                return _xmlSet ?? (_xmlSet = _xmlFileContext.Set<TItem>());
            }
        }

        public void SaveChanges()
        {
            _xmlFileContext.SaveChanges(XmlSet);
        }

        public void Add(TItem item)
        {
            XmlSet.Add(item);
            SaveChanges();
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                XmlSet.Add(item);
            SaveChanges();
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return XmlSet.Where(predicate);
        }

        public TItem Get(int id)
        {
            return XmlSet[id];
        }

        public IEnumerable<TItem> GetAll()
        {
            return XmlSet.AsEnumerable();
        }

        public void Remove(TItem item)
        {
            XmlSet.Remove(item);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                XmlSet.Remove(item);
            SaveChanges();
        }

        public void Update(TItem item)
        {
            SaveChanges();
        }
    }
}
