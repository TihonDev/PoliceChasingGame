namespace PCGClassLibrary.InnerGameExceptions
{
    public class InvalidCarBrandException : InnerGameException
    {
        public InvalidCarBrandException(string exceptionMessage = "Brand name cannot be so short or empty!")
            : base(exceptionMessage)
        {
        }
    }
}
