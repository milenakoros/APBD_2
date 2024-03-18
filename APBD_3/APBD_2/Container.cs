namespace APBD_3;

public class Container
{
    private double _weight;
    private double _height;
    private double _containerWeight;
    private double _depth;
    private double _maxWeight;
    private String _serialNumber;
    private static int counter = 0;

    public Container(double _containerWeight, double _maxWeight)
    {
        this._containerWeight = _containerWeight;
        this._maxWeight = _maxWeight;
        this._serialNumber = CreateSerialNumber();
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

    private static String CreateSerialNumber()
    {
        return "KON-X-" + counter++;
    } 
}