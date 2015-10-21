using System.Collections.Generic;

namespace ExchangeRateReader.Common
{
    public static class DictionaryExtensions
    {
        public static Option<TValue> TryGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {

            TValue value;

            if (!dictionary.TryGetValue(key, out value))
                return Option<TValue>.CreateNone();

            return Option<TValue>.CreateSome(value);

        }

    }
}
