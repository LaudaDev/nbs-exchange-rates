using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Common
{
    class OutputStream
    {
        public void Print<T>(IEnumerable<T> sequence)
        {
            foreach (T value in sequence)
                Print(value);
        }

        public void Print(object obj)
        {
            Console.WriteLine(obj.ToString());
        }
    }
}
