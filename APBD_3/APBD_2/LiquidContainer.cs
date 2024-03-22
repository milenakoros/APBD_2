namespace APBD_3;

public class LiquidContainer : Container
{
    private bool IsDangerous { get; set; }

    public LiquidContainer(double containerHeight, double containerWeight, double containerDepth, double maxLoadWeight, bool isDangerous) 
        : base( containerHeight, containerWeight, containerDepth, maxLoadWeight, 'L')
    {
        LoadWeight = 0;
        IsDangerous = isDangerous;
    }

    public override void Load(string cargoName, double cargoWeight)
    {
        if (IsEmpty() || (cargoName == CargoProductName && cargoWeight < MaxLoadWeight - LoadWeight))
        {
            if (IsDangerous)
            {
                if (cargoWeight / 2 > MaxLoadWeight)
                {
                    throw new OverfillException(SerialNumber);
                }
            }
            else
            {
                if (cargoWeight * 9 / 10 > MaxLoadWeight)
                {
                    throw new OverfillException(SerialNumber);
                }
            }
            CargoProductName = cargoName;
            LoadWeight = cargoWeight;
            Console.WriteLine("Container loaded successfully");
        }
        else
        {
            Console.WriteLine("You cannot add another thing because container is full or there is another product");
            throw new OverfillException(SerialNumber);
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
        return $"LiquidContainer: {base.ToString()}, Is Load Dangerous = {IsDangerous}";
    }
}