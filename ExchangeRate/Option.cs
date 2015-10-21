using System;
using System.Collections;
using System.Collections.Generic;

namespace ExchangeRateReader
{
    class Option<T>: IEnumerable<T>
    {
        private readonly T[] data;

        private Option(T[] data)
        {
            this.data = data;
        }

        public static Option<T> CreateSome(T value)
        {
            return new Option<T>(new[] { value });
        }

        public static Option<T> CreateNone()
        {
            return new Option<T>(new T[0]);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)this.data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
