using System;
using System.Collections.Generic;
using ExchangeRateReader.Interfaces;
using System.Linq;
using ExchangeRateReader.DTOs;

namespace ExchangeRateReader.Implementation
{
    public class ExchangeListBuilder : IExchangeListBuilder
    {

        private readonly IDailyListBuilder dailyListBuilder;

        public ExchangeListBuilder(IDailyListBuilder dailyListBuilder)
        {

            if (dailyListBuilder == null)
                throw new ArgumentNullException(nameof(dailyListBuilder));

            this.dailyListBuilder = dailyListBuilder;

        }

        public IEnumerable<ExchangeRate> BuildFor(IEnumerable<DateTime> dates)
        {

            if (dates == null)
                throw new ArgumentNullException(nameof(dates));

            return
                dates
                .SelectMany(date => this.dailyListBuilder.BuildFor(date));

        }
    }
}
