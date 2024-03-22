using System.Text;

namespace APBD_3;

public class ContainerShip
{
    private List<Container> _containers;
    private double _speed;
    private int _maxContainerNum;
    private double _maxWeight;

    public ContainerShip(List<Container> containers, double speed, int maxContainerNum, double maxWeight)
    {
        _containers = containers;
        _speed = speed;
        _maxContainerNum = maxContainerNum;
        _maxWeight = maxWeight;
    }

    public void AddContainer(Container container)
    {
        _containers.Add(container);
    }
    public void AddContainers(List<Container> containers)
    {
        _containers.AddRange(containers);
    }

    public void RemoveContainer(string serialNumber)
    {
        if (_containers.Exists(container => container._serialNumber == serialNumber))
        {
            _containers.RemoveAll(container => container._serialNumber == serialNumber);
            Console.WriteLine($"Removed container with the number: {serialNumber}");
        }
        else
        {
            Console.WriteLine($"There is no container on the container ship with the number: {serialNumber}");
        }
    }

    public void ReplacingContainer(string oldSerialNumber, Container newContainer)
    {
        if (_containers.Exists(container => container._serialNumber == oldSerialNumber))
        {
            _containers.RemoveAll(container => container._serialNumber == oldSerialNumber);
            _containers.Add(newContainer);
        }
        else
        {
            Console.WriteLine($"Container with the number {oldSerialNumber} does not exists.\nContainers are not replaced");
        }

    }
    public override string ToString()
    {
        StringBuilder shipInfo = new StringBuilder();
        shipInfo.AppendLine($"Container Ship Information:");
        shipInfo.AppendLine($"Speed: {_speed} knots");
        shipInfo.AppendLine($"Maximum Number of Containers: {_maxContainerNum}");
        shipInfo.AppendLine($"Maximum Total Weight: {_maxWeight} kg");
        shipInfo.AppendLine($"Containers on Board:");

        foreach (var container in _containers)
        {
            shipInfo.AppendLine(container.ToString());
        }

        return shipInfo.ToString();
    }

}