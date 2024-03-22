namespace APBD_3;


public class CoolingContainer : Container
{
    private static Dictionary<string, double> productsList= new Dictionary<string, double>()
    {
        { "bananas", 13.3 }, { "chocolate", 18 }, { "fish", 2 }, { "meat", -15 }, { "ice cream", -18 },
        {"frozen pizza", -30}, {"cheese", 7.2}, {"sausages", 5},{"butter",20.5},{"eggs",19}
    };
    public double ContainerTemperature { get; set; }

    public CoolingContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight,
        double containerTemperature)
        : base(containerHeight, containerWeight, containerDepth, maxLoadWeight, 'C')
    {
        ContainerTemperature = containerTemperature;
        LoadWeight = 0;
    }

    public override void Load(string cargoName, double cargoWeight)
    {
        if (IsEmpty() || (cargoName == CargoProductName && cargoWeight < MaxLoadWeight - LoadWeight))
        {
            if (cargoWeight > MaxLoadWeight)
            {
                throw new OverfillException(SerialNumber);
            }

            if (!productsList.ContainsKey(cargoName))
            {
                Console.WriteLine(
                    "Podanego produktu nie ma na liście, czy chcesz go dodać wraz z informacją w jakiej temperatuze powinien być przechowywany?\n{y/n}");
                string decision = Console.ReadLine();
                if (decision != "y")
                {
                    throw new Exception("Brak produktu na liscie");
                }

                Console.WriteLine($"Podaj temperaturę przechowywania dla {cargoName}:");
                double temperature = Console.Read();
                productsList.Add(cargoName, temperature);
            }

            if (productsList[cargoName] > ContainerTemperature)
            {
                //dopisac zmiane temp w kontenerze?
                Console.WriteLine("Temperatura w kontenerze jest za niska. Nie załadowano produktu");
            }
            else
            {
                LoadWeight += cargoWeight;
                CargoProductName = cargoName;
                Console.WriteLine("Container loaded successfully");
            }
        }else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
        }
    }
   
    public override void Unload()
    {
        LoadWeight = 0;
        CargoProductName = null;
        Console.WriteLine("Container emptied successfully");
    }
    public override string ToString()
    {
        return $"CoolingContainer: {base.ToString()}, ContainerTemperature={ContainerTemperature}";
    }
}