using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ExchangeRateReader.DTOs;
using ExchangeRateReader.Implementation;
using ExchangeRateReader.Interfaces;

namespace ExchangeRateReader.Infrastructure
{
    public class DailyListSerializer
    {
        public string Serialize(IDailyList list)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0}\t{1}\t{2}", list.Date.Year, list.Date.Month, list.Date.Day);

            foreach (ExchangeRate rate in list)
                sb.AppendFormat("\t{0}\t{1}", rate.Currency, rate.Rate.ToString(CultureInfo.InvariantCulture));

            return sb.ToString();
        }

        public IDailyList Deserialize(string line)
        {

            string[] parts = line.Split('\t');

            DateTime date = DeserializeDate(parts);
            IEnumerable<ExchangeRate> rates = this.DeserializeRates(date, parts);

            return new DailyList(date, rates);

        }

        private DateTime DeserializeDate(string[] parts)
        {

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            return new DateTime(year, month, day);

        }

        private IEnumerable<ExchangeRate> DeserializeRates(DateTime date, string[] parts)
        {

            int pos = 3;

            while (pos < parts.Length)
            {

                string currency = parts[pos++];
                decimal rate = decimal.Parse(parts[pos++], CultureInfo.InvariantCulture);

                yield return new ExchangeRate(date, currency, rate);

            }
        }
    }
}
