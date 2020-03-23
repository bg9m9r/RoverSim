using System;
using System.ComponentModel;
using System.Linq;
using RoverSim.Exceptions;
using RoverSim.Models;

namespace RoverSim.Controllers
{
    /// <summary>
    /// A class for controlling the rover.
    /// </summary>
    public static class RoverController
    {
        /// <summary>
        /// Moves the rover one unit forward given its current orientation.
        /// </summary>
        /// <returns>The resulting coordinate after moving.</returns>
        private static Coordinate Move(Coordinate currentCoordinate, Orientation currentOrientation)
        {
            switch (currentOrientation)
            {
                case Orientation.North:
                    currentCoordinate.Y++;
                    break;
                case Orientation.South:
                    currentCoordinate.Y--;
                    break;
                case Orientation.East:
                    currentCoordinate.X++;
                    break;
                case Orientation.West:
                    currentCoordinate.X--;
                    break;
            }

            return currentCoordinate;
        }

        /// <summary>
        /// Parses and executes the movement plan from user input.
        /// </summary>
        /// <param name="movementPlan"></param>
        /// <param name="rover"></param>
        /// <param name="plateau"></param>
        /// <returns>Returns both orientation and coordinate of the result.</returns>
        /// <exception cref="InvalidMovementException"></exception>
        public static Rover ExecuteMovementPlan(string movementPlan, Rover rover, Plateau plateau)
        {
            if (rover == null) throw new ArgumentNullException(nameof(rover));
            if (plateau == null) throw new ArgumentNullException(nameof(plateau));
            if (string.IsNullOrEmpty(movementPlan))
                throw new ArgumentException("Value cannot be null or empty.", nameof(movementPlan));

            foreach (var movement in movementPlan.ToUpperInvariant().Select((value, index) => new {value, index}))
            {
                switch (movement.value)
                {
                    case 'L':
                    case 'R':
                        rover.Orientation = Rotate(movement.value, rover.Orientation);
                        break;
                    case 'M':
                        rover.Coordinate = Move(rover.Coordinate, rover.Orientation);
                        if (rover.Coordinate.X > plateau.TopRight.X 
                            || rover.Coordinate.Y > plateau.TopRight.Y
                            || rover.Coordinate.Y < 0
                            || rover.Coordinate.X < 0)
                        {
                            throw new InvalidMovementException($"Movement at position {movement.index} in the plan goes outside of the plateau bounds.");
                        }
                        break;
                }
            }
            return rover;
        }

        

        /// <summary>
        /// Parses for the orientation character from user input.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static Orientation ParseOrientation(string position)
        {
            if (string.IsNullOrEmpty(position))
                throw new ArgumentException("Value cannot be null or empty.", nameof(position));
            
            var splitPosition = position.Split(' ');

            Orientation orientation;

            switch (splitPosition[2])
            {
                case "W":
                    orientation = Orientation.West;
                    break;
                case "N":
                    orientation = Orientation.North;
                    break;
                case "E":
                    orientation = Orientation.East;
                    break;
                case "S":
                    orientation = Orientation.South;
                    break;
                default:
                    throw new ArgumentException("Position string must be 2 numbers and a character representing a direction.");

            }

            return orientation;
        }

        /// <summary>
        /// Parses a coordinate string, the two coordinates X and Y separated by a single space.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public static Coordinate ParseCoordinate(string coord)
        {
            if (string.IsNullOrEmpty(coord))
                throw new ArgumentException("Value cannot be null or empty.", nameof(coord));

            var splitCoord = coord.Split(' ');

            if (splitCoord.Length != 2 && splitCoord.Length != 3) throw new ArgumentException("Coordinate must have two numbers with a space in between.");

            return new Coordinate(int.Parse(splitCoord[0]),int.Parse(splitCoord[1]));
        }


        /// <summary>
        /// Rotates the rover left or right 90 degrees.
        /// </summary>
        /// <returns>The resulting orientation after rotating.</returns>
        public static Orientation Rotate(char direction, Orientation orientation)
        {
            if (!Enum.IsDefined(typeof(Orientation), orientation))
                throw new InvalidEnumArgumentException(nameof(orientation), (int) orientation, typeof(Orientation));

            Rotation rotation;

            switch (direction)
            {
                case 'L':
                    rotation = Rotation.Left;
                    break;
                case 'R':
                    rotation = Rotation.Right;
                    break;
                default:
                    throw new InvalidRotationArgumentException($"{direction} is not a valid direction.");
                    
            }
            var orientationValue = (int)orientation + (int)rotation;

            Enum.TryParse(orientationValue.ToString(), out Orientation newOrientation);

            // handle orientation beyond the enum values
            if (orientationValue > 3)
            {
                newOrientation = Orientation.North;
            } 
            else if (orientationValue < 0)
            {
                newOrientation = Orientation.West;
            }

            return newOrientation;
        }
    }
}
