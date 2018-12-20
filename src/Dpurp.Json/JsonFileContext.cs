using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Dpurp.Json
{
    public class JsonFileContext : FileContext
    {
        public JsonFileContext(string folderPath)
            : base(folderPath)
        {
        }

        public override string FileExtension => "json";

        public override IList<TItem> ReadItems<TItem>()
        {
            var filePath = GetFullPath<Type>();
            using (var stream = File.OpenRead(filePath))
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<IList<TItem>>(jsonReader);
            }
        }

        public override void SaveChanges<TItem>(IList<TItem> items)
        {
            if (items == null || items.Count == 0)
                return;
            string filePath = GetFullPath<TItem>();
            SaveChanges(items.ToGenericList(), filePath);
        }

        public override void SaveChanges(IList items, Type type)
        {
            if (items == null || items.Count == 0)
                return;
            string filePath = GetFullPath(type);
            SaveChanges(items, filePath);
        }

        private void SaveChanges(IList items, string filePath)
        {
            var serializer = new JsonSerializer();
            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var streamWriter = new StreamWriter(stream))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, items);
                streamWriter.Flush();
            }
        }
    }
}
