using ExchangeRateReader.DTOs;
using ExchangeRateReader.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ExchangeRateReader.Implementation
{
    class DailyList : IDailyList
    {

        private IEnumerable<ExchangeRate> Rates { get; }

        public DailyList(IEnumerable<ExchangeRate> rates)
        {

            if (rates == null)
                throw new ArgumentNullException(nameof(rates));

            this.Rates = rates;
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
