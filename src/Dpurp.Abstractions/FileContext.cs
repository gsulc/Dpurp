using System;
using System.Collections.Generic;
using System.IO;

namespace Dpurp
{
    public abstract class FileContext
    {
        private string _fullPath;

        public FileContext(string folderPath)
        {
            FolderPath = folderPath ?? throw new ArgumentNullException("folderPath");
        }

        protected IDictionary<Type, IList<object>> Sets { get; } = new Dictionary<Type, IList<object>>();

        public string FolderPath { get; protected set; }
        public abstract string FileExtension { get; }

        protected string GetFullPath<TItem>()
        {
            return _fullPath ?? (_fullPath = GetFullPath(typeof(TItem)));
        }

        protected string GetFullPath(Type type)
        {
            return Path.Combine(FolderPath, $"{type.Name}.{FileExtension}");
        }

        public abstract void SaveChanges<TItem>(IList<TItem> items);
        public abstract void SaveChanges(IList<object> items, Type type);
        public abstract IList<TItem> ReadItems<TItem>();

        public IList<TItem> Set<TItem>()
        {
            Type type = typeof(TItem);
            if (Sets.Keys.Contains(type))
                return (Sets[type] as IList<TItem>);
            else
                return GetFromFileIfItExists<TItem>();
        }

        private IList<TItem> GetFromFileIfItExists<TItem>()
        {
            IList<TItem> items = new List<TItem>();
            if (File.Exists(GetFullPath<TItem>()))
                items = ReadItems<TItem>();
            AddToSets(items);
            return items;
        }

        public void SaveChanges()
        {
            foreach (var set in Sets)
                SaveChanges(set.Value, set.Key);
        }

        protected void AddToSets<TItem>(IList<TItem> items)
        {
            AddToSets(items as IList<object>, typeof(TItem));
        }

        protected void AddToSets(IList<object> items, Type type)
        {
            if (!Sets.Keys.Contains(type))
                Sets.Add(type, items ?? new List<object>());
            else
                Sets[type] = items;
        }
    }
}
