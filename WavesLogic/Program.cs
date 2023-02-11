namespace WavesLogic
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> waves = new() { "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", "1.7", "1.8", "1.9", "2.0" };
            int[] dates = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            List<string> weekDays = new() { "dom", "seg", "ter", "qua", "qui", "sex", "sab" };

            int countDays = dates.Length;
            int countWaves = waves.Count;
            int countWeekDays = weekDays.Count;

            int dateIndex = 0;
            int waveIndex = 0;
            int weekDayIndex = 0;

            while (dateIndex < countDays)
            {
                Console.WriteLine("Day: " + weekDays[weekDayIndex]);
                Console.WriteLine("Date: " + dates[dateIndex]);
                Console.WriteLine("Waves: " + (waveIndex < countWaves ? waves[waveIndex] : "0.0"));
                Console.WriteLine();

                dateIndex++;
                waveIndex++;
                weekDayIndex++;

                if (weekDayIndex == countWeekDays)
                {
                    weekDayIndex = 0;
                }
                if (waveIndex == countWaves)
                {
                    waveIndex = 0;
                }
            }
        }
    }
}