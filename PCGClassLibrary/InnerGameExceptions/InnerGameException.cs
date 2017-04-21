namespace PCGClassLibrary.InnerGameExceptions
{
    using System;

    public class InnerGameException : Exception
    {
        public InnerGameException(string message)
            : base(message)
        {
        }
    }
}
