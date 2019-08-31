using System;

namespace Wollies.Domain.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException(string message) : base(message)
        {
        }
    }
}
