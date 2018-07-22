using Dpurp.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp.Json
{
    public class JsonFileRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private JsonFileContext _jsonFileContext;
        private IList<TItem> _jsonSet;

        public JsonFileRepository(JsonFileContext jsonFileContext)
        {
            _jsonFileContext = jsonFileContext;
        }

        public IList<TItem> JsonSet
        {
            get
            {
                return _jsonSet ?? (_jsonSet = _jsonFileContext.Set<TItem>());
            }
        }

        public void SaveChanges()
        {
            _jsonFileContext.SaveChanges(JsonSet);
        }

        public void Add(TItem item)
        {
            JsonSet.Add(item);
            SaveChanges();
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                JsonSet.Add(item);
            SaveChanges();
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return JsonSet.Where(predicate);
        }

        public TItem Get(int id)
        {
            return JsonSet[id];
        }

        public IEnumerable<TItem> GetAll()
        {
            return JsonSet.AsEnumerable();
        }

        public void Remove(TItem item)
        {
            JsonSet.Remove(item);
            SaveChanges();
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                JsonSet.Remove(item);
            SaveChanges();
        }

        public void Update(TItem item)
        {
            SaveChanges();
        }
    }
}
