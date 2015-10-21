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

        public static ExchangeList Load(IEnumerable<DateTime> dates)
        {

            IEnumerable<ExchangeRate> rates =
                dates
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

    }
}
