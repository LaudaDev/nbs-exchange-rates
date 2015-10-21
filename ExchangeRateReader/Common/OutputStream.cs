using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Common
{
    public class OutputStream
    {
        public void Print<T>(IEnumerable<T> sequence)
        {

            if (sequence == null)
                throw new ArgumentNullException(nameof(sequence));

            foreach (T value in sequence)
                Print(value);

        }

        public void Print(object obj)
        {

            if (obj == null)
                return;

            Console.WriteLine(obj.ToString());

        }

        public void Print(string message)
        {
            Console.WriteLine(message);
        }

        public void NewLine()
        {
            Console.WriteLine();
        }
    }
}
