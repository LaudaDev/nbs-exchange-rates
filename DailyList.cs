using System;
using System.Collections;
using System.Collections.Generic;

namespace ExchangeRateReader
{
    class DailyList : IEnumerable<ExchangeRate>
    {

        private IEnumerable<ExchangeRate> Rates { get; }

        private DailyList(IEnumerable<ExchangeRate> rates)
        {
            this.Rates = rates;
        }

        public static DailyList LoadFor(DateTime date)
        {
            DailyListWebLoader loader = new DailyListWebLoader();
            IEnumerable<ExchangeRate> content = loader.LoadFor(date);
            return new DailyList(content);
        }

        public IEnumerator<ExchangeRate> GetEnumerator()
        {
            return this.Rates.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
