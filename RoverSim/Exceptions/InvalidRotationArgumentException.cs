using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RoverSim.Exceptions
{
    [Serializable]
    public class InvalidRotationArgumentException : Exception
    {
        public InvalidRotationArgumentException()
        {
        }

        public InvalidRotationArgumentException(string message) : base(message)
        {
        }

        public InvalidRotationArgumentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRotationArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
