using System;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRateReader
{
    class Program
    {

        private static InputStream Input { get; } = new InputStream();
        private static OutputStream Output { get; } = new OutputStream();

        static void Main(string[] args)
        {

            DateTime startingDate = Input.ReadOptionalDate("Startig date - ENTER for today (DD.MM.YYYY.): ", DateTime.UtcNow.Date);

            string currency = Input.ReadString("Currency: ");

            IEnumerable<ExchangeRate> rates =
                ExchangeList
                .LoadFrom(startingDate)
                .Where(rate => string.Compare(rate.Currency, currency, true) == 0);

            new OutputStream().Print(rates);

            Input.WaitEnter();

        }
    }
}
