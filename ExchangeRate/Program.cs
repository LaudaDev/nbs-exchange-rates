using ExchangeRateReader.Common;
using ExchangeRateReader.DTOs;
using ExchangeRateReader.Implementation;
using ExchangeRateReader.Interfaces;
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

            DateTime today = DateTime.UtcNow.Date;

            DateTime startingDate = Input.ReadOptionalDate("Startig date - ENTER for today (DD.MM.YYYY.): ", today, (date) => date <= today);
            DateTime endingDate = Input.ReadOptionalDate(" Ending date - ENTER for today (DD.MM.YYYY.): ", today, (date) => date >= startingDate && date <= today);

            string currency = Input.ReadString("Currency: ");

            try
            {
                IEnumerable<ExchangeRate> rates = LoadExchangeRates(startingDate, endingDate, currency);
                Output.Print(rates);
            }
            catch (Exception ex)
            {
                Output.Print(ex.Message);
            }

            Input.WaitEnter();

        }

        private static IEnumerable<ExchangeRate> LoadExchangeRates(DateTime startingDate, DateTime endingDate, string currency)
        {

            IExchangeListBuilder listBuilder =
                new ExchangeListBuilder(
                    new DailyListWebLoader());

            return
                listBuilder
                .BuildFor(DateList.BetweenInclusive(startingDate, endingDate))
                .Where(rate => string.Compare(rate.Currency, currency, true) == 0);

        }
    }
}
