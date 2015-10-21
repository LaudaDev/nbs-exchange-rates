using ExchangeRateReader.DTOs;
using System.Collections.Generic;

namespace ExchangeRateReader.Interfaces
{
    interface IExchangeList: IEnumerable<ExchangeRate>
    {
    }
}
