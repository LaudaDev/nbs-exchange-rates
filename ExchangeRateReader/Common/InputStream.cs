﻿using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace ExchangeRateReader.Common
{
    public class InputStream
    {

        private readonly Regex DateRegex = new Regex(@"(?<day>[0-9]+)\. *(?<month>[0-9]+)\. *(?<year>[0-9]+)\.?");

        private readonly Regex OptionalDateRegex = new Regex(@"(?<day>[0-9]+)\. *(?<month>[0-9]+)\. *(?<year>[0-9]+)\.?|(?<missing>) *");

        public string ReadString(string label)
        {
            Console.Write("{0}", label);
            return Console.ReadLine();
        }

        public bool ReadYesNo(string label)
        {
            while (true)
            {

                string answer = this.ReadString(label).ToLower();

                if (answer == "yes")
                    return true;

                if (answer == "no")
                    return false;

            }
        }

        public DateTime ReadOptionalDate(string label, DateTime defaultDate)
        {
            return this.ReadDateOptionUntilSome(() => this.TryReadOptionalDate(label, defaultDate));
        }

        public DateTime ReadOptionalDate(string label, DateTime defaultDate, Func<DateTime, bool> predicate)
        {
            while (true)
            {
                DateTime date = this.ReadOptionalDate(label, defaultDate);
                if (predicate(date))
                    return date;
            }
        }

        private DateTime ReadDateOptionUntilSome(Func<Option<DateTime>> readFunction)
        {
            while (true)
            {
                Option<DateTime> date = readFunction();
                if (date.Any())
                    return date.Single();
            }
        }

        public Option<DateTime> TryReadOptionalDate(string label, DateTime defaultValue)
        {

            Match dateMatch = this.ReadMatchingInput(label, this.OptionalDateRegex);

            if (dateMatch.Groups["missing"].Success)
                return Option<DateTime>.CreateSome(defaultValue);

            return DateMatchToDateTime(dateMatch);

        }

        private Match ReadMatchingInput(string label, Regex expression)
        {

            while (true)
            {

                string raw = this.ReadString(label);

                Match match = expression.Match(raw);

                if (match.Success)
                    return match;

            }

        }

        private Option<DateTime> DateMatchToDateTime(Match match)
        {

            int day = GroupToInt(match, "day");
            int month = GroupToInt(match, "month");
            int year = GroupToInt(match, "year");

            if (!IsValidDate(day, month, year))
                return Option<DateTime>.CreateNone();

            return Option<DateTime>.CreateSome(new DateTime(year, month, day));

        }

        private bool IsValidDate(int day, int month, int year)
        {

            if (year < DateTime.MinValue.Year || year > DateTime.MaxValue.Year)
                return false;

            if (month < 1 || month > 12)
                return false;

            if (day < 1 || day > DateTime.DaysInMonth(year, month))
                return false;

            return true;

        }

        private int GroupToInt(Match match, string groupName)
        {
            return int.Parse(match.Groups[groupName].Value);
        }
    }
}
