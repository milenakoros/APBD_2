namespace APBD_3;

public abstract class Container
{
    private double _weight;
    private double _height;
    private double _containerWeight;
    private double _depth;
    private double _maxWeight;
    private String _serialNumber;
    private static int counter = 0;

    protected Container(double weight, double height, double containerWeight, double depth, double maxWeight, char kind )
    {
        _weight = weight;
        _height = height;
        _containerWeight = containerWeight;
        _depth = depth;
        _maxWeight = maxWeight;
        _serialNumber = CreateSerialNumber(kind);
    }

    public double Weight
    {
        get { return _weight;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Weight cannot be less than zero");
            }

            _weight = value;
        }
    }
    public double Height
    {
        get { return _height;}
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Height cannot be less than zero");
            }

            _height = value;
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

    private static String CreateSerialNumber(char kind)
    {
        return "KON-" + kind + "-" + counter++;
    } 
}

public class GasContainer : Container
{
    public GasContainer(double weight, double height, double containerWeight, double depth, double maxWeight) : base(weight, height, containerWeight, depth, maxWeight, 'G')
    {
        
    }
}

public class CoolingContainer : Container
{
    public CoolingContainer(double weight, double height, double containerWeight, double depth, double maxWeight) : base(weight, height, containerWeight, depth, maxWeight, 'C')
    {
        
    }
}


public class IHazardNotifier : Exception {
    public IHazardNotifier(string message) : base(message)
    {
    }
}