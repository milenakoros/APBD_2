namespace APBD_3;

public abstract class Container
{
    private double _loadWeight;
    private double _containerHeight;
    private double _containerWeight;
    private double _containerDepth;
    private double _maxLoadWeight;
    private String _serialNumber;
    private static int counter = 0;

    protected Container(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight, char kind )
    {
        _containerHeight = containerHeight;
        _containerWeight = containerWeight;
        _containerDepth = containerDepth;
        _maxLoadWeight = maxLoadWeight;
        _serialNumber = CreateSerialNumber(kind);
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

    private static String CreateSerialNumber(char kind)
    {
        return "KON-" + kind + "-" + counter++;
    } 
}

public class LiquidContainer : Container
{
    public LiquidContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'L')
    {
        
    }
    
    public void loadContainer (){}
}
public class GasContainer : Container
{
    public GasContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'G')
    {
        
    }
    
    public void loadContainer (){}
}

public class CoolingContainer : Container
{
    public CoolingContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight) : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'C')
    {
        
    }
}


public interface IHazardNotifier
{
    public void unsafeCommuniaction(string message)
    {
        //Console.Error(message);
    }
}

public class OverfillException : Exception
{
}
