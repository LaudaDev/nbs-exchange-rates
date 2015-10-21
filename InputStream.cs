using System;

namespace ExchangeRateReader
{
    class InputStream
    {

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

        public DateTime ReadDate()
        {

            int day = ReadInt("  Day: ");
            int month = ReadInt("Month: ");
            int year = ReadInt(" Year: ");

            return new DateTime(year, month, day);

        }

    }
}
