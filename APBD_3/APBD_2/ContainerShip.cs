using System.Text;

namespace APBD_3;

public class ContainerShip
{
    public List<Container> Containers { get; private set; } = new List<Container>();
    public string ContainerShipName { get; set; }
    public double Speed { get; private set; }
    public int MaxContainerNum { get; private set; }
    public double MaxWeight { get; private set; }

    public ContainerShip(string containerShipName, double speed, int maxContainerNum, double maxWeight)
    {
        ContainerShipName = containerShipName;
        Speed = speed;
        MaxContainerNum = maxContainerNum;
        MaxWeight = maxWeight;
    }

    public double WeightWithTheContainers()
    {
        double weight = 0;
        foreach (var container in Containers)
        {
            weight += container.ContainerWeight + container.LoadWeight;
        }

        return weight;
    }

    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerNum)
        {
            Console.WriteLine("Cannot add more containers. Maximum number of containers reached.");
        }
        else if (Containers.Any(c => c.SerialNumber == container.SerialNumber))
        {
            Console.WriteLine("Container with this serial number already exists.");
        }
        else if (container.ContainerWeight + container.LoadWeight + WeightWithTheContainers() > MaxWeight)
        {
            Console.WriteLine($"The weight of the containers exceeds the available free weight on the ship. " +
                              $"The container with the number: {container.SerialNumber} was not loaded.");
        }
        else
        {
            Containers.Add(container);
        }
    }
    public void AddContainers(List<Container> containers)
    {
        foreach (var container in Containers)
        {
            AddContainer(container);
        }
    }
    public void RemoveContainer(string serialNumber)
    {
        if (Containers.Exists(container => container.SerialNumber == serialNumber))
        {
            Containers.RemoveAll(container => container.SerialNumber == serialNumber);
            Console.WriteLine($"Removed container with the number: {serialNumber}");
        }
        else
        {
            Console.WriteLine($"There is no container on the ship with the number: {serialNumber}");
        }
    }
    public void ReplacingContainer(string oldSerialNumber, Container newContainer)
    {
        var oldContainerIndex = Containers.FindIndex(container => container.SerialNumber == oldSerialNumber);
        if (oldContainerIndex != -1)
        {
            Containers[oldContainerIndex] = newContainer;
            Console.WriteLine($"Container with the number {oldSerialNumber} has been replaced.");
        }
        else
        {
            Console.WriteLine($"Container with the number {oldSerialNumber} does not exist.\nContainers are not replaced");
        }
    }
    public void TransferContainer(ContainerShip targetShip, string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        if (container == null)
        {
            throw new Exception("Container not found on the source ship.");
        }
        Containers.Remove(container);
        targetShip.AddContainer(container);
    }
    public override string ToString()
    {
        StringBuilder shipInfo = new StringBuilder();
        shipInfo.AppendLine("Container Ship Information:");
        shipInfo.AppendLine($"Name: {ContainerShipName}, Speed: {Speed} knots, Maximum Number of Containers: {MaxContainerNum}, Maximum Total Weight: {MaxWeight} kg");
        shipInfo.AppendLine("Containers on Board:");

        if (Containers.Count==0)
        {
            shipInfo.AppendLine("None");
        }
        else
        {
            foreach (var container in Containers)
            {
                shipInfo.AppendLine(container.ToString());
            }
        }
        return shipInfo.ToString();
    }
}