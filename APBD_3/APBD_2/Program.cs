namespace APBD_3;

using System;
using System.Collections.Generic;

public class Program
{
    static List<ContainerShip> _ships = new List<ContainerShip>();
    static List<Container> _containers = new List<Container>();

    static void Main(string[] args)
    {
        while (true)
        {
            DisplayMenu();
            string choice = GetUserChoice();
            switch (choice)
            {
                case "1":
                    AddShip();
                    break;
                case "2":
                    RemoveShip();
                    break;
                case "3":
                    AddContainer();
                    break;
                case "4":
                    AssignContainerToShip();
                    break;
                case "5":
                    RemoveContainer();
                    break;
                case "6":
                    DisplayShipsAndContainers();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                    break;
            }
        }
    }

    static void DisplayMenu()
    {
        DisplayShipsAndContainers();
        Console.WriteLine("\nMożliwe akcje:");
        Console.WriteLine("1. Dodaj kontenerowiec");
        if (_containers.Count != 0)
        {
            Console.WriteLine("2. Usun kontenerowiec");
            Console.WriteLine("3. Dodaj kontener");
            Console.WriteLine("4. Przypisz kontener do statku");
            Console.WriteLine("5. Usuń kontener");
            Console.WriteLine("6. Wyświetl statki i kontenery");
            Console.WriteLine("7. Wyjście");
        }
    }

    static string GetUserChoice()
    {
        Console.Write("Wybierz akcję: ");
        return Console.ReadLine();
    }

    static void AddShip()
    {
        Console.Write("Podaj nazwę statku: ");
        string name = Console.ReadLine();
        Console.Write("Podaj prędkość statku: ");
        double speed = double.Parse(Console.ReadLine());
        Console.Write("Podaj maksymalną liczbę kontenerów: ");
        int maxContainerNum = int.Parse(Console.ReadLine());
        Console.Write("Podaj maksymalny ciężar: ");
        double maxWeight = double.Parse(Console.ReadLine());

        _ships.Add(new ContainerShip(name, speed, maxContainerNum, maxWeight));
    }

    static void RemoveShip()
    {
        Console.Write("Podaj nazwę statku do usunięcia: ");
        string name = Console.ReadLine();
        var shipToRemove = _ships.Find(s => s.ContainerShipName == name);
        if (shipToRemove != null)
        {
            _ships.Remove(shipToRemove);
            Console.WriteLine("Stąg usunięty.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono statku.");
        }
    }

    static void AddContainer()
    {
        Console.Write("Podaj typ kontenera (L - Liquid, G - Gas, C - Cooling): ");
        char type = Console.ReadLine()[0];
        if (!(type=='L' || type == 'l' || type == 'G' || type == 'g' || type == 'C' || type=='c' ))
        {
            Console.WriteLine("Nieprawidłowy typ kontenera.");
        }
        else
        {
            Console.Write("Podaj wysokość kontenera: ");
            double height = double.Parse(Console.ReadLine());
            Console.Write("Podaj wagę kontenera: ");
            double weight = double.Parse(Console.ReadLine());
            Console.Write("Podaj głębokość kontenera: ");
            double depth = double.Parse(Console.ReadLine());
            Console.Write("Podaj maksymalną wagę ładunku: ");
            double maxLoadWeight = double.Parse(Console.ReadLine());
            Console.Write("Czy kontener jest niebezpieczny (y/n)? ");
            bool isDangerous = Console.ReadLine() == "y";

            if (type == 'L' || type == 'l' )
            {
                _containers.Add(new LiquidContainer(height, weight, depth, maxLoadWeight, isDangerous));
            }
            else if (type == 'G' || type == 'g' )
            {
                Console.Write("Podaj ciśnienie w kontenerze: ");
                double pressure = double.Parse(Console.ReadLine());
                _containers.Add(new GasContainer(height, weight, depth, maxLoadWeight, pressure));
            }
            else if (type == 'C'|| type=='c')
            {
                Console.Write("Podaj temperaturę w kontenerze: ");
                double temperature = double.Parse(Console.ReadLine());
                _containers.Add(new CoolingContainer(height, weight, depth, maxLoadWeight, temperature));
            }
        }
    }

    static void AssignContainerToShip()
    {
        Console.Write("Podaj nazwę statku: ");
        string shipName = Console.ReadLine();
        Console.Write("Podaj numer seryjny kontenera: ");
        string serialNumber = Console.ReadLine();

        var ship = _ships.Find(s => s.ContainerShipName == shipName);
        var container = _containers.Find(c => c.SerialNumber == serialNumber);

        if (ship != null && container != null)
        {
            ship.AddContainer(container);
            Console.WriteLine("Kontener przypisany do statku.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono statku lub kontenera.");
        }
    }

    static void RemoveContainer()
    {
        Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
        string serialNumber = Console.ReadLine();

        var containerToRemove = _containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToRemove != null)
        {
            _containers.Remove(containerToRemove);
            Console.WriteLine("Kontener usunięty.");
        }
        else
        {
            Console.WriteLine("Nie znaleziono kontenera.");
        }
    }

    static void DisplayShipsAndContainers()
    {
        Console.WriteLine("Lista kontenerowców:");
        if (_ships.Count == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            foreach (var ship in _ships)
            {
                Console.WriteLine(
                    $"{ship.ContainerShipName} (speed={ship.Speed}, maxContainerNum={ship.MaxContainerNum}, maxWeight={ship.MaxWeight})");
            }
        }

        Console.WriteLine("\nLista kontenerów:");
        if (_containers.Count == 0)
        {
            Console.WriteLine("Brak");
        }
        else
        {
            foreach (var container in _containers)
            {
                Console.WriteLine($"{container.SerialNumber} - {container.CargoProductName}");
            }
        }
    }
}
