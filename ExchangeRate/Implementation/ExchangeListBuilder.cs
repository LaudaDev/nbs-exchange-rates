using System;
using System.Collections.Generic;
using ExchangeRateReader.Interfaces;
using System.Linq;
using ExchangeRateReader.DTOs;

namespace ExchangeRateReader.Implementation
{
    class ExchangeListBuilder : IExchangeListBuilder
    {

        private readonly IDailyListBuilder dailyListBuilder;

        public ExchangeListBuilder(IDailyListBuilder dailyListBuilder)
        {
            if (dailyListBuilder == null)
                throw new ArgumentNullException(nameof(dailyListBuilder));
            this.dailyListBuilder = dailyListBuilder;
        }

        public IExchangeList BuildFor(IEnumerable<DateTime> dates)
        {

            IEnumerable<ExchangeRate> rates =
                dates
                .SelectMany(date => this.dailyListBuilder.BuildFor(date));

            return new ExchangeList(rates);

        }
    }
}
