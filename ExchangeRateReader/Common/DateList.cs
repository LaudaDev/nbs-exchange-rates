using System;
using System.Collections.Generic;

namespace ExchangeRateReader.Common
{
    public static class DateList
    {
        public static IEnumerable<DateTime> BetweenInclusive(DateTime startingDate, DateTime endingDate)
        {
            DateTime cur = startingDate.Date;
            DateTime last = endingDate.Date;

            if (cur > last)
                yield break;

            while (true)
            {

                yield return cur;

                if (cur >= last)
                    yield break;

                cur = cur.AddDays(1);

            }

        }
    }
}
