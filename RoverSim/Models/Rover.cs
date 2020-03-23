using System;
using RoverSim.Controllers;

namespace RoverSim.Models
{
    public enum Orientation
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public enum Rotation
    {
        Left = -1,
        Right = 1
    }

    public class Rover
    {
        private string Position => $"{Coordinate.X} {Coordinate.Y} {Orientation.ToString().Substring(0,1)}";
        public Coordinate Coordinate { get; set; }
        public Orientation Orientation { get; set; }

        public Rover(string position, Plateau plateau)
        {
            Coordinate = RoverController.ParseCoordinate(position);
            if (Coordinate.X > plateau.TopRight.X 
                || Coordinate.Y > plateau.TopRight.Y
                || Coordinate.Y < 0
                || Coordinate.X < 0)
                throw new ArgumentOutOfRangeException(nameof(position), "Rover out of range of the plateau graph.");

            Orientation = RoverController.ParseOrientation(position);
        }

        public override string ToString()
        {
            return Position;
        }

        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            return obj is Rover p && Equals(Coordinate, p.Coordinate) && Orientation == p.Orientation;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Coordinate != null ? Coordinate.GetHashCode() : 0) * 397) ^ (int) Orientation;
            }
        }
    }

}
