using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Place.Core.Helpers
{
    public static class ConstantHelper
    {
        public static Dictionary<string, string> GetConstantFields<T>(T obj)
        {
            return obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(string))
                .ToDictionary(f => f.Name,
                    f => (string)f.GetValue(null));
        }
    }
}
