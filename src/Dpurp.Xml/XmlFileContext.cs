using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Dpurp.Xml
{
    // TODO: Performance
    public class XmlFileContext : FileContext
    {
        private readonly string _folderPath;
        private readonly IDictionary<Type, IList<object>> _sets = new Dictionary<Type, IList<object>>();

        public XmlFileContext(string folderPath)
            : base(folderPath)
        {
        }

        public override string FileExtension => "xml";

        public override IList<TItem> ReadItems<TItem>()
        {
            string filePath = GetFullPath<TItem>();
            using (var stream = File.OpenRead(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<TItem>));
                return serializer.Deserialize(stream) as List<TItem>;
            }
        }

        public override void SaveChanges<TItem>(IList<TItem> items)
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

        public override void SaveChanges(IList items, Type type)
        {
            if (items == null || items.Count == 0)
                return;
            string filePath = GetFullPath(type);
            using (var writer = new StreamWriter(filePath))
            {
                var serializer = new XmlSerializer(typeof(List<object>));
                serializer.Serialize(writer, items);
                writer.Flush();
            }
        }
    }
}
