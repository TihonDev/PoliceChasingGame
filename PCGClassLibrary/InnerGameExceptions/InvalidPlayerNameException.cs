namespace PCGClassLibrary.InnerGameExceptions
{
    public class InvalidPlayerNameException : InnerGameException
    {
        public InvalidPlayerNameException(string exceptionMessage = "Player name cannot be so short or empty!")
            : base(exceptionMessage)
        {
        }
    }
}
