using ExchangeRateReader.Common;
using ExchangeRateReader.DTOs;
using ExchangeRateReader.Implementation;
using ExchangeRateReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ExchangeRateReader.Infrastructure;

[assembly: CLSCompliant(true)]

namespace ExchangeRateReader
{
    class Program
    {

        private static InputStream Input { get; } = new InputStream();
        private static OutputStream Output { get; } = new OutputStream();

        static void Main(string[] args)
        {

            do
            {
                Run();
            } while (ShouldRepeat());

        }

        private static void Run()
        {

            DateTime today = DateTime.UtcNow.Date;

            DateTime startingDate = ReadStartingDate(today);
            DateTime endingDate = ReadEndingDate(startingDate, today);
            string currency = ReadCurrency();

            try
            {
                IEnumerable<ExchangeRate> rates = LoadExchangeRates(startingDate, endingDate, currency);
                Output.Print(rates);
            }
            catch (Exception ex)
            {
                Output.Print(ex.Message);
            }

        }

        private static bool ShouldRepeat()
        {
            Output.NewLine();
            return Input.ReadYesNo("Do you want to repeat the query? (yes/no) ");
        }

        private static DateTime ReadStartingDate(DateTime maxDate)
        {
            return Input.ReadOptionalDate("Startig date - ENTER for maxDate (DD.MM.YYYY.): ", maxDate, (date) => date <= maxDate);
        }

        private static DateTime ReadEndingDate(DateTime minDate, DateTime maxDate)
        {
            return Input.ReadOptionalDate(" Ending date - ENTER for maxDate (DD.MM.YYYY.): ", maxDate, (date) => date >= minDate && date <= maxDate);
        }

        private static string ReadCurrency()
        {
            return Input.ReadString("Currency: ");
        }

        private static IEnumerable<ExchangeRate> LoadExchangeRates(DateTime startingDate, DateTime endingDate, string currency)
        {

            IExchangeListBuilder listBuilder =
                new ExchangeListBuilder(
                    new DailyListCachingBuilder(
                       new DailyListWebLoader(),
                       new DailyListRepository()));

            return
                listBuilder
                    .BuildFor(DateList.BetweenInclusive(startingDate, endingDate))
                    .Where(rate => String.Compare(rate.Currency, currency, StringComparison.OrdinalIgnoreCase) == 0);

        }
    }
}
