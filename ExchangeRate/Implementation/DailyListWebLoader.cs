using ExchangeRateReader.DTOs;
using ExchangeRateReader.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace ExchangeRateReader.Implementation
{
    public class DailyListWebLoader: IDailyListBuilder
    {

        private readonly NumberStyles IntegerStyle = NumberStyles.Integer;
        private readonly NumberStyles DecimalStyle = NumberStyles.Number;
        private readonly IFormatProvider FormatProvider = CultureInfo.GetCultureInfo("sr");

        public IDailyList BuildFor(DateTime date)
        {

            Uri url = this.GetServiceUrl(date);
            WebRequest req = WebRequest.CreateHttp(url);

            IEnumerable<ExchangeRate> data = LoadFrom(req, date);

            return new DailyList(data);

        }

        private Uri GetServiceUrl(DateTime day)
        {
            string raw = string.Format("http://www.nbs.rs/kursnaListaModul/srednjiKurs.faces?listno=&year=2015&listtype=3&lang=cir&date={0:dd.MM.yyyy.}", day);
            return new Uri(raw);
        }

        private IEnumerable<ExchangeRate> LoadFrom(WebRequest req, DateTime date)
        {
            using (WebResponse resp = req.GetResponse())
            {
                return LoadFrom(resp, date);
            }
        }

        private IEnumerable<ExchangeRate> LoadFrom(WebResponse resp, DateTime date)
        {
            using (Stream stream = resp.GetResponseStream())
            {
                return LoadFrom(stream, date);
            }
        }

        private IEnumerable<ExchangeRate> LoadFrom(Stream stream, DateTime date)
        {
            using (TextReader reader = new StreamReader(stream))
            {
                string content = reader.ReadToEnd();
                return LoadFrom(content, date);
            }
        }

        private IEnumerable<ExchangeRate> LoadFrom(string content, DateTime date)
        {

            string tableBegin = @"<tbody id=""index:srednjiKursLista:tbody_element"">";
            string tableEnd = @"</tbody>";

            string tableBody = this.ContentBetween(content, tableBegin, tableEnd);

            return this.LoadFromTableBody(tableBody, date);

        }

        private string ContentBetween(string content, string begin, string end)
        {

            int beginIndex = content.IndexOf(begin);
            if (beginIndex < 0)
                return string.Empty;
            beginIndex += begin.Length;

            int endIndex = content.IndexOf(end, beginIndex);
            if (endIndex < 0)
                return string.Empty;

            return content.Substring(beginIndex, endIndex - beginIndex);

        }

        private IEnumerable<ExchangeRate> LoadFromTableBody(string tableBody, DateTime date)
        {
            return
                this.GetTableRowsFromTableBody(tableBody)
                .SelectMany(row => this.FromTableRow(row, date));
        }

        private IEnumerable<string> GetTableRowsFromTableBody(string tableBody)
        {

            string rowBegin = "<tr>";
            string rowEnd = "</tr>";

            return this.ValuesBetween(tableBody, rowBegin, rowEnd);

        }

        private IEnumerable<ExchangeRate> FromTableRow(string row, DateTime date)
        {
            IEnumerable<string> cells = TableFieldsFromTableRow(row);
            return this.FromRawCells(cells, date);
        }

        private IEnumerable<string> TableFieldsFromTableRow(string row)
        {
            return
                this.ValuesBetween(row, "<td", "</td>")
                .Select(cell => cell.Substring(cell.IndexOf(">") + 1))
                .Select(cell => HttpUtility.HtmlDecode(cell));
        }

        private IEnumerable<string> ValuesBetween(string content, string begin, string end)
        {

            int index = 0;
            while (true)
            {

                int beginIndex = content.IndexOf(begin, index);
                if (beginIndex < 0)
                    yield break;
                beginIndex += begin.Length;

                int endIndex = content.IndexOf(end, beginIndex);
                if (endIndex < 0)
                    yield break;

                string row = content.Substring(beginIndex, endIndex - beginIndex);
                yield return row;

                index = endIndex + end.Length;

            }
        }

        private IEnumerable<ExchangeRate> FromRawCells(IEnumerable<string> cells, DateTime date)
        {

            string[] cellsArray = cells.ToArray();

            if (cellsArray.Length < 5)
                return new ExchangeRate[0];

            string currency = cellsArray[2];
            string multiplierRaw = cellsArray[3];
            string rateRaw = cellsArray[4];

            return FromRawCells(date, currency, multiplierRaw, rateRaw);

        }

        private IEnumerable<ExchangeRate> FromRawCells(DateTime date, string currency, string multiplierRaw, string rateRaw)
        {

            int multiplier = 0;
            if (!int.TryParse(multiplierRaw, this.IntegerStyle, this.FormatProvider, out multiplier))
                return new ExchangeRate[0];

            return FromRawCells(date, currency, multiplier, rateRaw);

        }

        private IEnumerable<ExchangeRate> FromRawCells(DateTime date, string currency, int multiplier, string rateRaw)
        {

            decimal rate = 0;
            if (!decimal.TryParse(rateRaw, this.DecimalStyle, this.FormatProvider, out rate))
                return new ExchangeRate[0];

            rate /= (decimal) multiplier;

            return new[] {new ExchangeRate(date, currency, rate)};

        }
    }
}
