namespace APBD_3;


public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }
    public GasContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight, double pressure) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'G')
    {
        LoadWeight = 0;
        Pressure = pressure;
    }

    public override void Load(string cargoName, double cargoWeight)
    {
        if (IsEmpty() || (cargoName == CargoProductName && cargoWeight < MaxLoadWeight - LoadWeight))
        {
            if (cargoWeight > MaxLoadWeight)
            {
                throw new OverfillException(SerialNumber);
            }

            LoadWeight += cargoWeight;
            CargoProductName = cargoName;
        }
        else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
        }
    }
    public override void Unload()
    {
        LoadWeight = 0.05*LoadWeight;
        CargoProductName = null;
        Console.WriteLine("Container emptied successfully");
    }
    public void NotifyHazard(string message)
    {
        Console.WriteLine($"Hazard detected in LiquidContainer {SerialNumber}: {message}");
    }

    public new bool IsEmpty()
    {
        return LoadWeight <= 0.05 * MaxLoadWeight;
    }

    public override string ToString()
    {//do poprawy
        return $"GasContainer: {base.ToString()}, Pressure={Pressure}";
    }
}
