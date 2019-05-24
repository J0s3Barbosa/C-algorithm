using System;
using System.Collections.Generic;

namespace ConsoleAppTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var waves = new List<string>() {  "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0" };

            var dates = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

            var weekDays = new List<string>() { "dom", "seg", "ter", "qua", "qui", "sex", "sab" };

            var countDays = dates.Length;
            var countWaves = waves.Count;
            var countWeekDays = weekDays.Count;

            for (var i = 0; i < countDays; i++)
            {
                for (var w = 0; w < countWaves; w++)
                {
                    for (var j = 0; j < countWeekDays; j++)
                    {
                        Console.WriteLine("Day: " + weekDays[j]);
                        if (i <= countDays - 1)
                        {
                            Console.WriteLine("Date: " + dates[i++]);
                        }
                        else
                        {
                            break;
                        }
                        if (w <= countWaves - 1)
                        {
                            Console.WriteLine("waves: " + waves[w++]);
                        }
                        else
                        {
                            Console.WriteLine("waves: 0.0");
                        }
                        if (j == countWeekDays - 1)
                        {
                            j = -1;
                        }


                    }
                }
            };

            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

        }
    }
}
