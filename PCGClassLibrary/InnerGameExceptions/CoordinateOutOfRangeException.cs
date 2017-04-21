namespace PCGClassLibrary.InnerGameExceptions
{
    using System;

    public class CoordinateOutOfRangeException : Exception
    {
        public CoordinateOutOfRangeException(string message)
            : base(message)
        {
        }
    }
}
