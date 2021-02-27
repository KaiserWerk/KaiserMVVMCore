using System;

namespace KaiserMVVMCore.Exceptions
{
    [Serializable]
    public class PropertyNameUnknownException : Exception
    {
        public PropertyNameUnknownException() : base("Unknown or empty property name; probably overwritten")
        { }
    }
}
