using System;

namespace KaiserMVVMCore.Exceptions
{
    public class RecipientAlreadyRegisteredException : Exception
    {
        public RecipientAlreadyRegisteredException(string message = "This recipient class is already registered") : base(message)
        { }
    }
}
