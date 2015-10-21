using ExchangeRateReader.DTOs;
using ExchangeRateReader.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace ExchangeRateReader.Implementation
{
    class ExchangeList: IExchangeList
    {

        private IEnumerable<ExchangeRate> Rates { get; }

        public ExchangeList(IEnumerable<ExchangeRate> rates)
        {
            this.Rates = rates;
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
