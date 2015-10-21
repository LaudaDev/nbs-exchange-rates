using ExchangeRateReader.DTOs;
using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Interfaces
{
    interface IExchangeListBuilder
    {
        IEnumerable<ExchangeRate> BuildFor(IEnumerable<DateTime> dates);
    }
}
