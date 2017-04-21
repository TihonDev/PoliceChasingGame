namespace PCGClassLibrary.InnerGameExceptions
{
    public class InvalidCarModelException : InnerGameException
    {
        public InvalidCarModelException(string exceptionMessage = "Please input model name!")
            : base(exceptionMessage)
        {
        }
    }
}
