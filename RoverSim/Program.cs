using System;
using RoverSim.Controllers;
using RoverSim.Models;

namespace RoverSim
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.Write("Enter Graph Upper Right Coordinate: ");
            var coordinate = Console.ReadLine();

            var plateau = new Plateau(RoverController.ParseCoordinate(coordinate));

            for(var roverCount = 1; roverCount < 3; roverCount++)
            {
                Console.Write($"Rover {roverCount} Starting Position: ");
                var startPosition = Console.ReadLine();

                var rover = new Rover(startPosition, plateau);
                
                Console.Write($"Rover {roverCount} Movement Plan: ");
                var movementPlan = Console.ReadLine();

                rover = RoverController.ExecuteMovementPlan(movementPlan, rover, plateau);

                Console.WriteLine($"Rover {roverCount} Output: {rover}");
            }

            Quit();

        }

        private static void Quit()
        {
            Console.Write("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
