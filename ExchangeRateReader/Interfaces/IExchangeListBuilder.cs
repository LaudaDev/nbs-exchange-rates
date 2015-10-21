using ExchangeRateReader.DTOs;
using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Interfaces
{
    public interface IExchangeListBuilder
    {
        IEnumerable<ExchangeRate> BuildFor(IEnumerable<DateTime> dates);
    }
}
