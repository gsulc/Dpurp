using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dpurp.Json
{
    public class JsonFileContext
    {
        private readonly string _folderPath;

        public JsonFileContext(string folderPath)
        {
            _folderPath = folderPath ?? throw new ArgumentNullException("folderPath");
        }

        private string GetFullPath<TItem>()
        {
            return Path.Combine(_folderPath, $"{typeof(TItem).Name}.json");
        }

        public void SaveChanges<TItem>(IList<TItem> items)
        {
            if (items == null || items.Count == 0)
                return;
            string filePath = GetFullPath<TItem>();

            var serializer = new JsonSerializer();

            using (var stream = new FileStream(filePath, FileMode.Create))
            using (var streamWriter = new StreamWriter(stream))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
            {
                serializer.Serialize(jsonWriter, items);
                streamWriter.Flush();
            }
        }

        public IList<TItem> Set<TItem>()
        {
            string filePath = GetFullPath<TItem>();
            IList<TItem> items = new List<TItem>();
            if (File.Exists(filePath))
            {
                using (var stream = File.OpenRead(filePath))
                using (var streamReader = new StreamReader(stream))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var serializer = new JsonSerializer();
                    items = serializer.Deserialize<IList<TItem>>(jsonReader);
                }
            }
            return items;
        }
    }
}
