using System;

namespace ServerAdministration.WindowOs
{

    [Serializable]
    public class NotEnoughDiskSpaceException : Exception
    {
        public NotEnoughDiskSpaceException() { }
        public NotEnoughDiskSpaceException(string message) : base(message) { }
        public NotEnoughDiskSpaceException(string message, Exception inner) : base(message, inner) { }
        protected NotEnoughDiskSpaceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
