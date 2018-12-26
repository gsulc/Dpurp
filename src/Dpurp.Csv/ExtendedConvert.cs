using System;

namespace Dpurp.Csv
{
    public static class ExtendedConvert
    {
        public static object ChangeType(object obj, Type type)
        {
            if (type.IsEnum)
                return (obj is string str) ? Enum.Parse(type, str) : Enum.ToObject(type, obj);
            else
                return Convert.ChangeType(obj, type);
        }
    }
}
