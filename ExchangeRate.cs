using System;
using System.Collections.Generic;

namespace ExchangeRateReader
{
    class ExchangeRate
    {

        public DateTime Date { get; }
        public string Currency { get; }
        public decimal Rate { get; }

        public ExchangeRate(DateTime date, string currency, decimal rate)
        {
            this.Date = date;
            this.Currency = currency;
            this.Rate = rate;
        }

        public override string ToString()
        {
            return string.Format("{0:MM-dd-yyyy} {1,5} {2:#,##0.0000}", this.Date, this.Currency, this.Rate);
        }
    }
}
