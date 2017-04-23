namespace PCGConsoleClient
{
    using PCGClassLibrary;

    public static class StartupClass
    {
        public static void Main()
        {
            var policeChasingGame = GameEngine.GetInstance();
            policeChasingGame.Play();
        }
    }
}
