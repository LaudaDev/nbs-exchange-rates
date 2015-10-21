using System;
using ExchangeRateReader.Common;
using ExchangeRateReader.Interfaces;

namespace ExchangeRateReader.Implementation
{
    public interface IDailyListRepository
    {
        Option<IDailyList> TryGet(DateTime date);
        void Add(IDailyList dailyList);
    }
}
