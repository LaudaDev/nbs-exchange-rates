using System;
using ExchangeRateReader.Common;
using ExchangeRateReader.Implementation;
using ExchangeRateReader.Interfaces;
using System.Collections.Generic;

namespace ExchangeRateReader.Infrastructure
{
    public class DailyListRepository: IDailyListRepository
    {

        private IDictionary<DateTime, IDailyList> dateToList;

        private IDictionary<DateTime, IDailyList> GetDateToList()
        {

            if (this.dateToList == null)
                this.dateToList = this.LoadData();

            return this.dateToList;

        }

        public Option<IDailyList> TryGet(DateTime date)
        {
            return this.GetDateToList().TryGetValue(date);
        }

        public void Add(IDailyList dailyList)
        {
            if (!this.GetDateToList().ContainsKey(dailyList.Date))
                new DailyListFile().Append(dailyList);
        }

        private IDictionary<DateTime, IDailyList> LoadData()
        {

            IDictionary<DateTime, IDailyList> map = new Dictionary<DateTime, IDailyList>();

            foreach (IDailyList list in new DailyListFile().GetAll())
                map[list.Date] = list;

            return map;

        }

    }
}
