using System;
using ExchangeRateReader.DTOs;
using System.Collections.Generic;

namespace ExchangeRateReader.Interfaces
{
    public interface IDailyList: IEnumerable<ExchangeRate>
    {
        DateTime Date { get; }
    }
}
