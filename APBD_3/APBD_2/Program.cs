using System;
using System.Collections.Generic;

namespace APBD_3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize containers
            LiquidContainer liquidContainer = new LiquidContainer(2.0, 10.0, 1.5, 500.0);
            GasContainer gasContainer = new GasContainer(2.0, 10.0, 1.5, 500.0);
            CoolingContainer coolingContainer = new CoolingContainer(2.0, 10.0, 1.5, 500.0, -18.0);

            // Initialize the container ship
            List<Container> containers = new List<Container> { liquidContainer, gasContainer, coolingContainer };
            ContainerShip ship = new ContainerShip(containers, 20.0, 10, 10000.0);

            // Load cargo into the liquid container
            try
            {
                liquidContainer.ContainerLoading("Water", 200.0);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Load cargo into the gas container
            try
            {
                gasContainer.ContainerLoading("Natural Gas", 300.0);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Load cargo into the cooling container
            try
            {
                coolingContainer.ContainerLoading("Ice Cream", 100.0);
            }
            catch (OverfillException ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Display information about the ship and its cargo
            Console.WriteLine(ship.ToString());

            // Remove a container from the ship
            ship.RemoveContainer(liquidContainer._serialNumber);

            // Display updated information about the ship and its cargo
            Console.WriteLine(ship.ToString());

            // Replace a container on the ship
            CoolingContainer newCoolingContainer = new CoolingContainer(2.0, 10.0, 1.5, 500.0, -18.0);
            ship.ReplacingContainer(coolingContainer._serialNumber, newCoolingContainer);

            // Display final information about the ship and its cargo
            Console.WriteLine(ship.ToString());
        }
    }
}
