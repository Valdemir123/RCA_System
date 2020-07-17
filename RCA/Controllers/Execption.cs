using System;
using System.Runtime.Serialization;

namespace RCA.Controllers
{
    [Serializable]
    internal class Execption : Exception
    {
        public Execption()
        {
        }

        public Execption(string message) : base(message)
        {
        }

        public Execption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected Execption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}