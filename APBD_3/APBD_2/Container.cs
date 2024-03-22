namespace APBD_3;

public abstract class Container
{
    private double _cargoWeight;
    private double _containerHeight;
    private double _containerWeight;
    private double _containerDepth;
    private double _maxLoadWeight;
    public string _serialNumber { get; set; }
    public string _cargoName { get; set; }
    private static int counter = 0;

    protected Container(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight, char kind )
    {
        _containerHeight = containerHeight;
        _containerWeight = containerWeight;
        _containerDepth = containerDepth;
        _maxLoadWeight = maxLoadWeight;
        _serialNumber = CreateSerialNumber(kind);
    }

    public double CargoWeight
    {
        get { return _cargoWeight;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Weight cannot be less than zero");
            }

            _cargoWeight = value;
        }
    }
    public double ContainerHeight
    {
        get { return _containerHeight;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Height cannot be less than zero");
            }

            _containerHeight = value;
        }
    }
    public double ContainerWeight
    {
        get { return _containerWeight;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Container weight cannot be less than zero");
            }
            _containerWeight = value;
        }
    }
    public double ContainerDepth
    {
        get => _containerDepth;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Container weight cannot be less than zero");
            }
            _containerDepth = value;
        }    }
    public double MaxLoadWeight
    {
        get => _maxLoadWeight;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Container weight cannot be less than zero");
            }
            _maxLoadWeight = value;
        }    }

    public abstract void ContainerLoading(string cargoName, double cargoWeight);

    private static String CreateSerialNumber(char kind)
    {
        return $"KON-{kind}-{counter++}";
    }
    public override string ToString()
    {
        return $"Container: SerialNumber={_serialNumber}, Height={_containerHeight}, Weight={_containerWeight}, Depth={_containerDepth}, MaxLoadWeight={_maxLoadWeight}";
    }
}

public class LiquidContainer : Container
{
    public bool isDangerous { get; set; }

    public LiquidContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'L')
    {
        
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (cargoWeight > MaxLoadWeight)
        {
            throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
        }
        Console.WriteLine("Is liquid dangerous? Then you can fill only the half of the container.\ny - yes\nanything else - no");
        string? decision = Console.ReadLine();
        if (decision == "y")
        {
            if (cargoWeight/2 > MaxLoadWeight)
            {
                //inny blad napisac?
                throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
            }
            
        }
        if (cargoWeight*9/10 > MaxLoadWeight)
        {
            throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
        }
        _cargoName = cargoName;
        CargoWeight = cargoWeight;
        Console.WriteLine("Container loaded successfully");
    }

    public override string ToString()
    {
        return $"LiquidContainer: {base.ToString()}, LoadWeight={CargoWeight}";
    }
}
public class GasContainer : Container, IHazardNotifier
{
    public GasContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'G')
    {
        
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (cargoWeight > MaxLoadWeight)
        {
            throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
        }

        CargoWeight = cargoWeight;
        _cargoName = cargoName;
    }
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard detected in LiquidContainer {_serialNumber}: {message}");
    }

    public override string ToString()
    {
        return $"GasContainer: {base.ToString()}, LoadWeight={CargoWeight}";
    }
}

public class CoolingContainer : Container
{
    private static Dictionary<string, double> productsList= new Dictionary<string, double>()
    {
        { "bananas", 13.3 }, { "chocolate", 18 }, { "fish", 2 }, { "meat", -15 }, { "ice cream", -18 },
        {"frozen pizza", -30}, {"cheese", 7.2}, {"sausages", 5},{"butter",20.5},{"eggs",19}
    };
    public double _containerTemperature { get; set; }

    public CoolingContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight,
        double containerTemperature)
        : base(containerHeight, containerWeight, containerDepth, maxLoadWeight, 'C')
    {
        _containerTemperature = containerTemperature;
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (cargoWeight > MaxLoadWeight)
        {
            throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
        }

        if (!productsList.ContainsKey(cargoName))
        {
            Console.WriteLine("Podanego produktu nie ma na liście, czy chcesz go dodać wraz z informacją w jakiej temperatuze powinien być przechowywany?\n{y/n}");
            string decision = Console.ReadLine();
            if (decision != "y")
            {
                throw new Exception("Brak produktu na liscie");
            }
            Console.WriteLine($"Podaj temperaturę przechowywania dla {cargoName}:");
            double temperature = Console.Read();
            productsList.Add(cargoName,temperature);
        }
        
        if (productsList[cargoName] > _containerTemperature)
        {
            //dopisac zmiane temp w kontenerze?
            Console.WriteLine("Temperatura w kontenerze jest za niska. Nie załadowano produktu");
        }
        else
        {
            CargoWeight = cargoWeight;
            _cargoName = cargoName;
            Console.WriteLine("Container loaded successfully");
        }
    }
    public override string ToString()
    {
        return $"CoolingContainer: {base.ToString()}, LoadWeight={CargoWeight}";
    }
}

public interface IHazardNotifier
{
    void NotifyHazard(string message);
}


public class OverfillException : Exception
{
    public OverfillException(string serialNumber, double loadWeight, double maxLoadWeight)
        : base($"Attempted to load a container {serialNumber} with a weight of {loadWeight} kg, which exceeds its maximum load capacity of {maxLoadWeight} kg.")
    {
    }
}
