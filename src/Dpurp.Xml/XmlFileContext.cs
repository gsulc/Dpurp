using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Dpurp.Xml
{
    // TODO: Performance
    public class XmlFileContext
    {
        private readonly string _folderPath;

        public XmlFileContext(string folderPath)
        {
            _folderPath = folderPath ?? throw new ArgumentNullException("folderPath");
        }

        private string GetFullPath<TItem>()
        {
            return Path.Combine(_folderPath, $"{typeof(TItem).Name}.xml");
        }

        public void SaveChanges<TItem>(IList<TItem> items)
        {
            if (items == null || items.Count == 0)
                return;
            string filePath = GetFullPath<TItem>();
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<TItem>));
                serializer.Serialize(writer, items);
                writer.Flush();
            }
        }

        public IList<TItem> Set<TItem>()
        {
            string filePath = GetFullPath<TItem>();
            IList<TItem> items = new List<TItem>();
            if (File.Exists(filePath))
            {
                using (var stream = File.OpenRead(filePath))
                {
                    var serializer = new XmlSerializer(typeof(List<TItem>));
                    items = serializer.Deserialize(stream) as List<TItem>;
                }
            }
            return items;
        }
    }
}
