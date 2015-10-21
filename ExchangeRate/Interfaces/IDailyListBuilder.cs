using System;

namespace ExchangeRateReader.Interfaces
{
    public interface IDailyListBuilder
    {
        IDailyList BuildFor(DateTime date);
    }
}
