using System;

namespace KaiserMVVM.Exceptions
{
    [Serializable]
    public class PropertyNameUnknownException : Exception
    {
        public PropertyNameUnknownException() : base("Unknown or empty property name; probably overwritten")
        { }
    }
}
