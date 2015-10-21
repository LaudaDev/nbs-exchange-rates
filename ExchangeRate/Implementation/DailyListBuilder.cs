using ExchangeRateReader.DTOs;
using ExchangeRateReader.Interfaces;
using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Implementation
{
    class DailyListBuilder: IDailyListBuilder
    {
        public IDailyList BuildFor(DateTime date)
        {
            DailyListWebLoader loader = new DailyListWebLoader();
            IEnumerable<ExchangeRate> content = loader.LoadFor(date);
            return new DailyList(content);
        }
    }
}
