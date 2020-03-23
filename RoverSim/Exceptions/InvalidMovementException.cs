using System;

namespace RoverSim.Exceptions
{
    [Serializable]
    public class InvalidMovementException : Exception
    {
        public InvalidMovementException(string message) : base(message)
        {
        }

        public InvalidMovementException()
        {
        }

        public InvalidMovementException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidMovementException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
        {
        }
    }
}
