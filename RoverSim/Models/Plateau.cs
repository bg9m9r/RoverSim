using System.Collections.Generic;

namespace RoverSim.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Plateau
    {
        public Coordinate TopRight { get; }

        /// <summary>
        /// Constructor for assigning a top right coordinate.
        /// </summary>
        /// <param name="topRight"></param>
        public Plateau(Coordinate topRight)
        {
            TopRight = topRight;
        }
    }
}
