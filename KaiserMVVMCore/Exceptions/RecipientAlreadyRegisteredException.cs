using System;

namespace KaiserMVVMCore.Exceptions
{
    public class RecipientAlreadyRegisteredException : Exception
    {
        public RecipientAlreadyRegisteredException() : base("This recipient class is already registered")
        { }
    }
}
