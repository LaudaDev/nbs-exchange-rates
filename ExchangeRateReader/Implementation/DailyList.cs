using ExchangeRateReader.DTOs;
using ExchangeRateReader.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ExchangeRateReader.Implementation
{
    public class DailyList : IDailyList
    {

        public DateTime Date { get; }
        private IEnumerable<ExchangeRate> Rates { get; }

        public DailyList(DateTime date, IEnumerable<ExchangeRate> rates)
        {

            if (rates == null)
                throw new ArgumentNullException(nameof(rates));

            this.Date = date;
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
