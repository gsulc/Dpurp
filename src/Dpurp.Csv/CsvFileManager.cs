using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dpurp.Csv
{
    public class CsvFileManager
    {
        private readonly Type _type;
        private PropertyInfo[] _configProperties;
        
        public CsvFileManager(Type type)
        {
            _type = type;
            _configProperties = _type.GetProperties();
        }

        public bool UsingHeader { get; set; } = true;

        public IList<object> Load(string path)
        {
            var list = new List<object>();
            var lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                var elements = line.Split(',');
                var firstLine = lines.First();
                if (UsingHeader && line == firstLine)
                {
                    ParseHeader(line);
                }
                else
                {
                    var obj = CreateObject(elements);
                    list.Add(obj);
                }
            }
            return list;
        }

        // preserves the order of the properties in the file
        private void ParseHeader(string line)
        {
            var elements = line.Split(',');
            var properties = _type.GetProperties();
            _configProperties = new PropertyInfo[properties.Length];
            for (int i = 0; i < elements.Length; ++i)
            {
                var name = elements[i];
                _configProperties[i] = properties.Where(p => p.Name == name).First();
            }
        }

        private object CreateObject(string[] propertyStringValues)
        {
            var obj = Activator.CreateInstance(_type);
            for (int i = 0; i < _configProperties.Length; ++i)
            {
                var stringValue = propertyStringValues[i];
                if (string.IsNullOrEmpty(stringValue))
                    continue;
                var type = _configProperties[i].PropertyType;
                var value = ExtendedConvert.ChangeType(stringValue, type);
                _configProperties[i].SetValue(obj, value);
            }
            return obj;
        }

        public void Save(IEnumerable<object> items, string path)
        {
            using (var stream = new StreamWriter(path, false))
            {
                if (UsingHeader)
                    stream.WriteLine(GetHeader());
                foreach (var item in items)
                    stream.WriteLine(CreateItemLine(item));
            }
        }

        private string GetHeader()
        {
            return BuildLine(_configProperties.Select(p => p.Name));
        }

        private string CreateItemLine(object item)
        {
            var values = _configProperties.Select(p => GetItemString(p.GetValue(item)));
            return BuildLine(values);
        }

        private string GetItemString(object item)
        {
            return item != null ? item.ToString() : "";
        }

        private string BuildLine(IEnumerable<string> elements)
        {
            var builder = new StringBuilder();
            var lastElement = elements.Last();
            foreach (var element in elements)
            {
                builder.Append(element);
                bool atTheEnd = element == lastElement;
                if (!atTheEnd)
                    builder.Append(",");
            }
            return builder.ToString();
        }
    }
}
