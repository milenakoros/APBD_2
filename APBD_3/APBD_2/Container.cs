namespace APBD_3;

public abstract class Container
{
    private double _cargoWeight;
    private double _containerHeight;
    private double _containerWeight;
    private double _containerDepth;
    private double _maxLoadWeight;
    public bool _isEmpty{ get; set; }
    public string _serialNumber { get; set; }
    public string? _cargoProductName { get; set; }
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
    public abstract void CargoRemoval();


    private static String CreateSerialNumber(char kind)
    {
        return $"KON-{kind}-{counter++}";
    }
    public override string ToString()
    {
        return $"Container: SerialNumber={_serialNumber}, ProductName={_cargoProductName}, ProductWeight={_cargoWeight}, " +
               $"ContainerHeight={_containerHeight}, ContainerWeight={_containerWeight}, ContainerDepth={_containerDepth}, MaxLoadWeight={_maxLoadWeight}";
    }
}

public class LiquidContainer : Container
{
    public bool isDangerous { get; set; }

    public LiquidContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'L')
    {
        _isEmpty = true;
        CargoWeight = 0;
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (_isEmpty || (cargoName == _cargoProductName && cargoWeight < MaxLoadWeight - CargoWeight))
        {
            if (cargoWeight > MaxLoadWeight - CargoWeight)
            {
                throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
            }

            Console.WriteLine(
                "Is liquid dangerous? Then you can fill only the half of the container.\ny - yes\nanything else - no");
            string? decision = Console.ReadLine();
            if (decision == "y")
            {
                isDangerous = true;
                if (cargoWeight / 2 > MaxLoadWeight)
                {
                    //inny blad napisac?
                    throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
                }
            }
            else
            {
                isDangerous = false;
            }
            if (cargoWeight * 9 / 10 > MaxLoadWeight)
            {
                throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
            }
            _cargoProductName = cargoName;
            CargoWeight = cargoWeight;
            Console.WriteLine("Container loaded successfully");
        }
        else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
        }
    }

    public override void CargoRemoval()
    {
        CargoWeight = 0;
        _cargoProductName = null;
        Console.WriteLine("Container emptied successfully");
        _isEmpty = true;
    }


    public override string ToString()
    {
        return $"LiquidContainer: {base.ToString()}, Is Cargo Dangerous = {isDangerous}";
    }
}
public class GasContainer : Container, IHazardNotifier
{
    public GasContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'G')
    {
        _isEmpty = true;
        CargoWeight = 0;
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (_isEmpty || (cargoName == _cargoProductName && cargoWeight < MaxLoadWeight - CargoWeight))
        {
            if (cargoWeight > MaxLoadWeight)
            {
                throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
            }

            CargoWeight += cargoWeight;
            _cargoProductName = cargoName;
            _isEmpty = false;
        }
        else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
        }
    }
    public override void CargoRemoval()
    {
        CargoWeight = 0.05*CargoWeight;
        _cargoProductName = null;
        Console.WriteLine("Container emptied successfully");
        _isEmpty = true;
    }
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard detected in LiquidContainer {_serialNumber}: {message}");
    }

    public override string ToString()
    {//do poprawy
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
        _isEmpty = true;
        CargoWeight = 0;
    }

    public override void ContainerLoading(string cargoName, double cargoWeight)
    {
        if (_isEmpty || (cargoName == _cargoProductName && cargoWeight < MaxLoadWeight - CargoWeight))
        {
            if (cargoWeight > MaxLoadWeight)
            {
                throw new OverfillException(_serialNumber, MaxLoadWeight, cargoWeight);
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

            if (productsList[cargoName] > _containerTemperature)
            {
                //dopisac zmiane temp w kontenerze?
                Console.WriteLine("Temperatura w kontenerze jest za niska. Nie załadowano produktu");
            }
            else
            {
                CargoWeight += cargoWeight;
                _cargoProductName = cargoName;
                Console.WriteLine("Container loaded successfully");
                _isEmpty = false;
            }
        }else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
        }
    }
    public override string ToString()
    {
        return $"CoolingContainer: {base.ToString()}, ContainerTemperature={_containerTemperature}";
    }
    public override void CargoRemoval()
    {
        CargoWeight = 0;
        _cargoProductName = null;
        Console.WriteLine("Container emptied successfully");
        _isEmpty = true;
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
