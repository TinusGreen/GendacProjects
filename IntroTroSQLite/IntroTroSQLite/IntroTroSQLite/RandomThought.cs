using System;
using SQLite.Net.Attributes;

namespace IntroToSQLite
{
    public class RandomThought
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public RandomThought()
        {
        }
    }
}