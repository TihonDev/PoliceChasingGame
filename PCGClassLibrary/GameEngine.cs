namespace PCGClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Media;
    using System.Threading;
    using PCGClassLibrary.GameObjects;
    using PCGClassLibrary.Interfaces;
    using PCGClassLibrary.Managers;

    public class GameEngine
    {
        private static GameEngine gameEngineInstance;
        private int sleepTime;
        private int scorePoints;
        private bool playNewGame = true;
        private bool moveTheCar = true;
        private ConsoleManager consoleManager = new ConsoleManager();
        private SoundManager gameSoundsManager = new SoundManager(new SoundPlayer());

        private GameEngine()
        {
        }

        public static GameEngine GetInstance()
        {
            if (gameEngineInstance == null)
            {
                gameEngineInstance = new GameEngine();
            }

            return gameEngineInstance;
        }

        public void Play()
        {
            const int CAR_WIDTH = 9;
            const int CAR_HEIGHT = 4;
            var maxValueOfX = Console.WindowWidth - CAR_WIDTH;
            var maxValueOfY = Console.WindowHeight - CAR_HEIGHT;

            var welcomeLineText = "WELCOME TO \"POLICE CHASING\" GAME";
            var gameDescriptionLines = new List<string>()
            {
                "You are a police officer in Sofia.",
                "Your mission is to pursue and arrest the most dangerous criminals in the city,",
                "like Mitio - Ochilata and Ceco - Boreco."
            };

            this.consoleManager.PrintIntroText(this.gameSoundsManager, welcomeLineText, gameDescriptionLines);
            Thread.Sleep(2000);

            var criminalGuy = new Criminal(maxValueOfX, maxValueOfY);
            var policeCar = new Car(criminalGuy, maxValueOfX, maxValueOfY);
            this.consoleManager.ReadCarInfo(policeCar);
            this.RunTheGame(policeCar, criminalGuy);
        }

        private void RunTheGame(Car policeCar, Criminal criminalGuy)
        {
            policeCar.CaughtCriminalEvent += this.CaughtCriminal;
            policeCar.CarCrashedEvent += this.CarCrashed;
            var directionsManager = new DirectionsManager();

            do
            {
                this.moveTheCar = true;
                this.scorePoints = 0;
                this.sleepTime = 80;
                directionsManager.ClearDirectionsHistory();
                policeCar.Direction = directionsManager.GetDirection("RightArrow");

                Console.Clear();
                Console.CursorVisible = false;
                this.consoleManager.PrintUnlockAndStartMessages(this.gameSoundsManager);
                this.gameSoundsManager.PlaySound("main_sound_theme.wav");
                policeCar.SetNewPosition(1, 1);
                criminalGuy.UpdatePosition(new Random());

                while (this.moveTheCar)
                {
                    if (Console.KeyAvailable)
                    {
                        var userChoice = Console.ReadKey();
                        policeCar.Direction = directionsManager.GetDirection(userChoice.Key.ToString());
                    }

                    this.consoleManager.Delete(policeCar, "_________");
                    policeCar.Move();
                    this.consoleManager.PrintFigure(policeCar, policeCar.Direction.CarForm, ConsoleColor.Cyan);
                    this.consoleManager.PrintFigure(criminalGuy, criminalGuy.BodyFigure, ConsoleColor.Red);
                    Thread.Sleep(this.sleepTime);
                }
            } while (this.playNewGame == true);
        }

        private void CaughtCriminal(ICar policeCar, ICriminal caughtCriminal)
        {
            this.scorePoints++;
            policeCar.SpeedUp(ref this.sleepTime);
            this.consoleManager.Delete(caughtCriminal, "_________");
            caughtCriminal.UpdatePosition(new Random());
        }

        private void CarCrashed(ICar crashedCar)
        {
            this.gameSoundsManager.PlaySound("crash.wav");
            this.consoleManager.PrintFigure(crashedCar, crashedCar.Direction.CarForm, ConsoleColor.Cyan);
            this.consoleManager.PrintGameOverMessage(this.scorePoints, crashedCar);

            this.moveTheCar = false;
            if (this.consoleManager.CheckIfPlayerWantsNewGame() == false)
            {
                this.playNewGame = false;
            }
        }
    }
}
