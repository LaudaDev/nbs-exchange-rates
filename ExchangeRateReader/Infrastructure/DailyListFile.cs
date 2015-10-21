using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExchangeRateReader.Interfaces;

namespace ExchangeRateReader.Infrastructure
{
    public class DailyListFile
    {
        private readonly string FileName = "rates.txt";

        public IEnumerable<IDailyList> GetAll()
        {

            if (!File.Exists(this.FileName))
                return new IDailyList[0];

            using (TextReader reader = new StreamReader(this.FileName))
            {
                return this.GetAll(reader).ToList();
            }
        }

        public void Append(IDailyList dailyList)
        {

            DailyListSerializer serializer = new DailyListSerializer();

            using (StreamWriter writer = File.AppendText(this.FileName))
            {
                string line = serializer.Serialize(dailyList);
                writer.WriteLine(line);
            }
        }

        private IEnumerable<IDailyList> GetAll(TextReader reader)
        {

            DailyListSerializer serializer = new DailyListSerializer();

            while (true)
            {

                string line = reader.ReadLine();

                if (line == null)
                    yield break;

                yield return serializer.Deserialize(line);

            }
        }
    }
}
