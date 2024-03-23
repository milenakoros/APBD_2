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
            if (_ships.Count()==0 && choice != "1")
            {
                Console.WriteLine("\nInvalid choice. Try again.");
            }
            else
            {
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
                        LoadContainerContents();
                        break;
                    case "7":
                        UnloadContainerContents();
                        break;
                    case "8":
                        RemoveContainerFromShip();
                        break;
                    case "9":
                        ReplaceContainerOnShip();
                        break;
                    case "10":
                        TransferContainerToAnotherShip();
                        break;
                    case "11":
                        DetailedInformationAboutShipsAndContainers();
                        break;
                    case "12":
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Try again.");
                        break;
                }
            }
        }
    }

    static void DisplayMenu()
    {
        DisplayShipsAndContainers();
        Console.WriteLine("\nPossible actions:");
        Console.WriteLine("1. Add a container ship");
        if (_ships.Count != 0)
        {
            Console.WriteLine("2. Remove a container ship");
            Console.WriteLine("3. Add a container");
            Console.WriteLine("4. Assign a container to a ship");
            Console.WriteLine("5. Remove a container");
            Console.WriteLine("6. Load container contents");
            Console.WriteLine("7. Unload container contents");
            Console.WriteLine("8. Remove container from ship");
            Console.WriteLine("9. Replace containers on ship");
            Console.WriteLine("10. Transfer container to another ship");
            Console.WriteLine("11. Display ships and containers");
            Console.WriteLine("12. Exit");
        }
    }

    static string GetUserChoice()
    {
        Console.Write("Choose an action: ");
        return Console.ReadLine();
    }

    static void AddShip()
    {
        Console.Write("Enter the ship name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter the ship speed: ");
        double speed = double.Parse(Console.ReadLine());
        Console.Write("Enter the maximum number of containers: ");
        int maxContainerNum = int.Parse(Console.ReadLine());
        Console.Write("Enter the maximum weight: ");
        double maxWeight = double.Parse(Console.ReadLine());

        _ships.Add(new ContainerShip(name, speed, maxContainerNum, maxWeight));
    }

    static void RemoveShip()
    {
        Console.Write("Enter the name of the ship to remove: ");
        string name = Console.ReadLine();
        var shipToRemove = _ships.Find(s => s.ContainerShipName == name);
        if (shipToRemove != null)
        {
            _ships.Remove(shipToRemove);
            Console.WriteLine("Ship removed.");
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }

    static void AddContainer()
    {
        _containers.Add(CreateNewContainer());
    }

    static Container? CreateNewContainer()
    {
        bool isCorrectValue = false;
        Console.Write("Enter the container type (L - Liquid, G - Gas, C - Cooling): ");
        char type = Console.ReadLine()[0];
        
        while (!isCorrectValue)
        {
            if (!(type=='L' || type == 'l' || type == 'G' || type == 'g' || type == 'C' || type=='c' ))
            {
                Console.WriteLine("Nieprawidłowy typ kontenera. Sprobuj ponownie.");
            }
            else
            {
                isCorrectValue = true;
            }
        }
        Console.Write("Enter the container height: ");
        double height = double.Parse(Console.ReadLine());
        Console.Write("Enter the container weight: ");
        double weight = double.Parse(Console.ReadLine());
        Console.Write("Enter the container depth: ");
        double depth = double.Parse(Console.ReadLine());
        Console.Write("Enter the maximum load weight: ");
        double maxLoadWeight = double.Parse(Console.ReadLine());

        Container newContainer = null;
        
        switch (type)
        {
            case char c when (c == 'L' || c == 'l'):
                newContainer = new LiquidContainer(height, weight, depth, maxLoadWeight);
                break;
            case char c when (c == 'G' || c == 'g'):
                Console.Write("Enter the pressure in the container: ");
                double pressure = double.Parse(Console.ReadLine());
                newContainer = new GasContainer(height, weight, depth, maxLoadWeight, pressure);
                break;
            case char c when (c == 'C' || c == 'c'):
                Console.Write("Enter the temperature in the container: ");
                double temperature = double.Parse(Console.ReadLine());
                newContainer = new CoolingContainer(height, weight, depth, maxLoadWeight, temperature);
                break;
            default:
                Console.WriteLine("Invalid container type. Try again.");
                break;
        }
        return newContainer;
    }


    static void AssignContainerToShip()
    {
        Console.Write("Enter the ship name: ");
        string shipName = Console.ReadLine();
        Console.Write("Enter the container serial number: ");
        string serialNumber = Console.ReadLine();

        var ship = _ships.Find(s => s.ContainerShipName == shipName);
        var container = _containers.Find(c => c.SerialNumber == serialNumber);

        if (ship != null && container != null)
        {
            ship.AddContainer(container);
            Console.WriteLine("Container assigned to the ship.");
        }
        else
        {
            Console.WriteLine("Ship or container not found.");
        }
    }

    static void RemoveContainer()
    {
        Console.Write("Enter the serial number of the container to remove: ");
        string serialNumber = Console.ReadLine();

        var containerToRemove = _containers.Find(c => c.SerialNumber == serialNumber);
        if (containerToRemove != null)
        {
            _containers.Remove(containerToRemove);
            Console.WriteLine("Container removed.");
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }

    static void DisplayShipsAndContainers()
    {
        Console.WriteLine("\nList of container ships:");
        if (_ships.Count == 0)
        {
            Console.WriteLine("None");
        }
        else
        {
            foreach (var ship in _ships)
            {
                Console.WriteLine($"{ship.ContainerShipName} (speed={ship.Speed}, maxContainerNum={ship.MaxContainerNum}, maxWeight={ship.MaxWeight})");
            }
        }

        Console.WriteLine("\nList of containers:");
        if (_containers.Count == 0)
        {
            Console.WriteLine("None");
        }
        else
        {
            foreach (var container in _containers)
            {
                Console.WriteLine($"{container.SerialNumber} - {container.CargoProductName}");
            }
        }
    }

    static void DetailedInformationAboutShipsAndContainers()
    {
        Console.WriteLine("\nList of container ships:");
        if (_ships.Count == 0)
        {
            Console.WriteLine("None");
        }
        else
        {
            foreach (var ship in _ships)
            {
                Console.WriteLine(ship);
            }
        }

        Console.WriteLine("\nList of containers:");
        if (_containers.Count == 0)
        {
            Console.WriteLine("None");
        }
        else
        {
            foreach (var container in _containers)
            {
                Console.WriteLine(container);
            }
        }
    }
    static void LoadContainerContents()
    {
        Console.Write("Enter the container serial number: ");
        string serialNumber = Console.ReadLine();
        Console.Write("Enter the cargo name: ");
        string cargoName = Console.ReadLine();
        Console.Write("Enter the cargo weight: ");
        double cargoWeight = double.Parse(Console.ReadLine());

        var container = _containers.Find(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            try
            {
                container.Load(cargoName, cargoWeight);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine($"Container {serialNumber} cannot be loaded due to overfill.");
            }
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }

    static void UnloadContainerContents()
    {
        Console.Write("Enter the container serial number: ");
        string serialNumber = Console.ReadLine();

        var container = _containers.Find(c => c.SerialNumber == serialNumber);
        if (container != null)
        {
            container.Unload();
            Console.WriteLine("Container emptied successfully.");
        }
        else
        {
            Console.WriteLine("Container not found.");
        }
    }
    static void RemoveContainerFromShip()
    {
        Console.Write("Enter the ship name: ");
        string shipName = Console.ReadLine();
        Console.Write("Enter the container serial number: ");
        string serialNumber = Console.ReadLine();

        var ship = _ships.Find(s => s.ContainerShipName == shipName);
        if (ship != null)
        {
            ship.RemoveContainer(serialNumber);
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }

    static void ReplaceContainerOnShip()
    {
        Console.Write("Enter the ship name: ");
        string shipName = Console.ReadLine();
        Console.Write("Enter the old container serial number: ");
        string oldSerialNumber = Console.ReadLine();

        Console.WriteLine("Do you want to use an existing container or create a new one? (1 for existing, 2 for new)");
        string choice = Console.ReadLine();

        Container newContainer;
        if (choice == "1")
        {
            Console.Write("Enter the new container serial number: ");
            string newSerialNumber = Console.ReadLine();
            newContainer = _containers.Find(c => c.SerialNumber == newSerialNumber);
            if (newContainer == null)
            {
                Console.WriteLine("Container not found.");
                return;
            }
        }
        else if (choice == "2")
        {
            newContainer = CreateNewContainer();
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }
        var ship = _ships.Find(s => s.ContainerShipName == shipName);
        if (ship != null)
        {
            ship.ReplacingContainer(oldSerialNumber, newContainer);
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }


    static void TransferContainerToAnotherShip()
    {
        Console.Write("Enter the source ship name: ");
        string sourceShipName = Console.ReadLine();
        Console.Write("Enter the target ship name: ");
        string targetShipName = Console.ReadLine();
        Console.Write("Enter the container serial number: ");
        string serialNumber = Console.ReadLine();

        var sourceShip = _ships.Find(s => s.ContainerShipName == sourceShipName);
        var targetShip = _ships.Find(s => s.ContainerShipName == targetShipName);
        if (sourceShip != null && targetShip != null)
        {
            try
            {
                sourceShip.TransferContainer(targetShip, serialNumber);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Ship not found.");
        }
    }
}