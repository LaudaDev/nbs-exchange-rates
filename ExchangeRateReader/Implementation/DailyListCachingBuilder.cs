using System;
using System.Linq;
using ExchangeRateReader.Common;
using ExchangeRateReader.Interfaces;

namespace ExchangeRateReader.Implementation
{
    public class DailyListCachingBuilder: IDailyListBuilder
    {
        private readonly IDailyListBuilder fallbackBuilder;
        private readonly IDailyListRepository listRepository;

        public DailyListCachingBuilder(IDailyListBuilder fallbackBuilder, IDailyListRepository listRepository)
        {

            if (fallbackBuilder == null)
                throw new ArgumentNullException(nameof(fallbackBuilder));

            if (listRepository == null)
                throw new ArgumentNullException(nameof(listRepository));

            this.fallbackBuilder = fallbackBuilder;
            this.listRepository = listRepository;

        }

        public IDailyList BuildFor(DateTime date)
        {

            Option<IDailyList> list = this.listRepository.TryGet(date);

            if (list.Any())
                return list.Single();

            IDailyList newList = this.fallbackBuilder.BuildFor(date);
            this.listRepository.Add(newList);

            return newList;

        }
    }
}
