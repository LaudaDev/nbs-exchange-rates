using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Interfaces
{
    interface IExchangeListBuilder
    {
        IExchangeList BuildFor(IEnumerable<DateTime> dates);
    }
}
