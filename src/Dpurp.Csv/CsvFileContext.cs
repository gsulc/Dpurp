using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Dpurp.Csv
{
    public class CsvFileContext : FileContext
    {
        public CsvFileContext(string folderPath) : base(folderPath)
        {
        }

        public override string FileExtension => "csv";

        public override void SaveChanges<TItem>(IList<TItem> items)
        {
            if (items == null || items.Count == 0)
                return;
            var fileManager = new CsvFileManager(typeof(TItem));
            fileManager.Save(items as IEnumerable<object>, GetFullPath<TItem>());
        }

        public override void SaveChanges(IList items, Type type)
        {
            if (items == null || items.Count == 0)
                return;
            var fileManager = new CsvFileManager(type);
            fileManager.Save(items as IEnumerable<object>, GetFullPath(type));
        }

        public override IList<TItem> ReadItems<TItem>()
        {
            var fileManager = new CsvFileManager(typeof(TItem));
            var items = fileManager.Load(GetFullPath<TItem>());
            return Convert<TItem>(items).ToList();
        }

        private IEnumerable<TItem> Convert<TItem>(IEnumerable<object> items)
        {
            foreach (var item in items)
                yield return (TItem)item;
        }
    }
}
