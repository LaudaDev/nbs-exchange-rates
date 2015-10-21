using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ExchangeRateReader
{
    class ExchangeList: IEnumerable<ExchangeRate>
    {

        private IEnumerable<ExchangeRate> Rates { get; }

        private ExchangeList(IEnumerable<ExchangeRate> rates)
        {
            this.Rates = rates;
        }

        public static ExchangeList LoadFrom(DateTime startingDate)
        {

            IEnumerable<ExchangeRate> rates =
                GetDatesStartingFrom(startingDate)
                .SelectMany(date => DailyList.LoadFor(date));

            return new ExchangeList(rates);

        }

        public IEnumerator<ExchangeRate> GetEnumerator()
        {
            return this.Rates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Rates.GetEnumerator();
        }

        private static IEnumerable<DateTime> GetDatesStartingFrom(DateTime startingDate)
        {

            DateTime today = DateTime.UtcNow.Date;
            DateTime cur = startingDate.Date;

            if (cur > today)
                yield break;

            while (true)
            {

                yield return cur;

                if (cur >= today)
                    yield break;

                cur = cur.AddDays(1);

            }

        }
    }
}
