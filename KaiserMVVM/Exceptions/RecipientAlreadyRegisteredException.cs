using System;

namespace KaiserMVVM.Exceptions
{
    public class RecipientAlreadyRegisteredException : Exception
    {
        public RecipientAlreadyRegisteredException() : base("This recipient class is already registered")
        { }
    }
}
