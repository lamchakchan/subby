using System.Collections.Generic;
using System.Linq;

namespace Subby.Core.Extensions
{
    public static class DictionaryExtensions
    {
        public static T MergeLeft<T, K, V>(this T source, params IDictionary<K, V>[] others)
        where T : IDictionary<K, V>, new()
        {
            T newMap = new T();
            foreach (IDictionary<K, V> src in
                (new List<IDictionary<K, V>> { source }).Concat(others))
            {
                // ^-- echk. Not quite there type-system.
                foreach (KeyValuePair<K, V> p in src)
                {
                    newMap[p.Key] = p.Value;
                }
            }
            return newMap;
        }
    }
}
