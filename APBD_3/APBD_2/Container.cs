namespace APBD_3;

public abstract class Container
{
    private double _loadWeight;
    private double _containerHeight;
    private double _containerWeight;
    private double _containerDepth;
    private double _maxLoadWeight;
    public string SerialNumber { get; set; }
    public string? CargoProductName { get; set; }
    private static int _counter = 1;

    protected Container(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight, char kind )
    {
        _containerHeight = containerHeight;
        _containerWeight = containerWeight;
        _containerDepth = containerDepth;
        _maxLoadWeight = maxLoadWeight;
        _loadWeight = 0;
        SerialNumber = CreateSerialNumber(kind);
    }

    public double LoadWeight
    {
        get { return _loadWeight;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Weight cannot be less than zero");
            }
            _loadWeight = value;
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

    public abstract void Load(string cargoName, double cargoWeight);
    public abstract void Unload();

    public bool IsEmpty()
    {
        return _loadWeight == 0;
    }


    private static String CreateSerialNumber(char kind)
    {
        return $"KON-{kind}-{_counter++}";
    }
    public override string ToString()
    {
        return $"SerialNumber={SerialNumber}, ProductName={CargoProductName}, ProductWeight={_loadWeight}, " +
               $"ContainerHeight={_containerHeight}, ContainerWeight={_containerWeight}, ContainerDepth={_containerDepth}, MaxLoadWeight={_maxLoadWeight}";
    }
}


public interface IHazardNotifier
{
    void NotifyHazard(string message);
}

public class OverfillException : Exception
{
    public OverfillException(string serialNumber)
        : base($"The load exceeds the permitted free space in the container with serial number: {serialNumber}. The cargo has not been loaded")
    {
    }
}
