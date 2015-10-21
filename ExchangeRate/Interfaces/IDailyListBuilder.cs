using System;

namespace ExchangeRateReader.Interfaces
{
    interface IDailyListBuilder
    {
        IDailyList BuildFor(DateTime date);
    }
}
