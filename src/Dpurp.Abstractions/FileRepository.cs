using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Dpurp
{
    public class FileRepository<TItem> : IRepository<TItem> where TItem : class
    {
        private IList<TItem> _items;
        private FileContext _fileContext;

        public FileRepository(FileContext fileContext)
        {
            _fileContext = fileContext;
        }

        private IList<TItem> Items
        {
            get
            {
                return _items ?? (_items = _fileContext.Set<TItem>());
            }
        }

        public void Add(TItem item)
        {
            TrySetNewId(item);
            Items.Add(item);
        }

        public void AddRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public IEnumerable<TItem> Find(Func<TItem, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public TItem Get(int id)
        {
            var property = GetIdProperty();
            if (property != null)
                return Items.Where(i => GetIdValue(i) == id).First();
            else
                return Items[id];
        }

        public IEnumerable<TItem> GetAll()
        {
            return Items;
        }

        public void Remove(TItem item)
        {
            Items.Remove(item);
        }

        public void RemoveRange(IEnumerable<TItem> items)
        {
            foreach (var item in items)
                Items.Remove(item);
        }

        public void SaveChanges()
        {
            _fileContext.SaveChanges(Items);
        }

        private void TrySetNewId(TItem item)
        {
            var property = GetIdProperty();
            if (property != null)
            {
                var last = Items.LastOrDefault();
                var lastId = GetIdValue(last);
                SetIdValue(item, ++lastId);
            }
        }

        private PropertyInfo GetIdProperty()
        {
            return typeof(TItem).GetRuntimeProperties().Where(
                p => IsIdProperty(p.Name)).FirstOrDefault();
        }

        private bool IsIdProperty(string propertyName)
        {
            if (string.Equals(propertyName, "Id", StringComparison.OrdinalIgnoreCase))
                return true;
            else if (propertyName.Contains(typeof(TItem).Name) && ContainsValidIdString(propertyName))
                return true;
            return false;
        }

        private bool ContainsValidIdString(string propertyName)
        {
            return propertyName.Contains("Id") ||
                propertyName.Contains("ID") ||
                propertyName.Contains("id");
        }

        private int GetIdValue(TItem item)
        {
            var property = GetIdProperty();
            if (property != null)
                return (int)property.GetValue(item);
            else return 0;
        }

        private void SetIdValue(TItem item, int id)
        {
            var property = GetIdProperty();
            if (property != null)
                property.SetValue(item, id);
        }
    }
}
