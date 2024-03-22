using System;

namespace APBD_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            // Generowanie losowych danych dla LiquidContainer
            LiquidContainer liquidContainer = new LiquidContainer(
                random.NextDouble() * 10, // wysokość
                random.NextDouble() * 10, // waga
                random.NextDouble() * 10, // głębokość
                random.NextDouble() * 1000 // maksymalna waga ładunku
            );

            // Generowanie losowych danych dla GasContainer
            GasContainer gasContainer = new GasContainer(
                random.NextDouble() * 10, // wysokość
                random.NextDouble() * 10, // waga
                random.NextDouble() * 10, // głębokość
                random.NextDouble() * 1000 // maksymalna waga ładunku
            );

            // Generowanie losowych danych dla CoolingContainer
            CoolingContainer coolingContainer = new CoolingContainer(
                random.NextDouble() * 10, // wysokość
                random.NextDouble() * 10, // waga
                random.NextDouble() * 10, // głębokość
                random.NextDouble() * 1000, // maksymalna waga ładunku
                -10
            );

            // Przykładowe użycie
            Console.WriteLine($"LiquidContainer: {liquidContainer}");
            Console.WriteLine($"GasContainer: {gasContainer}");
            Console.WriteLine($"CoolingContainer: {coolingContainer}");
        }
    }
}