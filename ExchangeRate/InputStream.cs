using System;
using System.Text.RegularExpressions;

namespace ExchangeRateReader
{
    class InputStream
    {

        private readonly Regex DateRegex = new Regex(@"(?<day>[0-9]+)\. *(?<month>[0-9]+)\. *(?<year>[0-9]+)\.?");

        private readonly Regex OptionalDateRegex = new Regex(@"(?<day>[0-9]+)\. *(?<month>[0-9]+)\. *(?<year>[0-9]+)\.?|(?<missing>) *");

        public void WaitEnter()
        {
            this.ReadString("Press ENTER to continue . . . ");
        }

        public string ReadString(string label)
        {
            Console.Write("{0}", label);
            return Console.ReadLine();
        }

        public int ReadInt(string label)
        {
            string value = ReadString(label);
            return Int32.Parse(value);
        }

        public DateTime ReadDate(string label)
        {

            Match dateMatch = this.ReadMatchingInput(label, this.DateRegex);

            return DateMatchToDateTime(dateMatch);

        }

        public DateTime ReadOptionalDate(string label, DateTime defaultValue)
        {

            Match dateMatch = this.ReadMatchingInput(label, this.OptionalDateRegex);

            if (dateMatch.Groups["missing"].Success)
                return defaultValue;

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

        private static DateTime DateMatchToDateTime(Match match)
        {

            int day = GroupToInt(match, "day");
            int month = GroupToInt(match, "month");
            int year = GroupToInt(match, "year");

            return new DateTime(year, month, day);
        }

        private static int GroupToInt(Match match, string groupName)
        {
            return int.Parse(match.Groups[groupName].Value);
        }
    }
}
