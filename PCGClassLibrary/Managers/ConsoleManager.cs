namespace PCGClassLibrary.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using PCGClassLibrary.GameObjects;
    using PCGClassLibrary.InnerGameExceptions;
    using PCGClassLibrary.Interfaces;

    public class ConsoleManager
    {
        public ConsoleManager()
        {
            Console.WindowWidth = Console.LargestWindowWidth;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.Title = "POLICE CHASING GAME";
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void PrintIntroText(ISoundManager gameSounds, string welcomeLine, IList<string> descriptionLines)
        {
            if (gameSounds == null)
            {
                throw new ArgumentNullException("gameSounds");
            }

            this.PrintWelcomeLine(welcomeLine);
            gameSounds.PlaySound("typewriter.wav");
            this.PrintGameDescription(descriptionLines);
        }

        public void ReadCarInfo(ICar policeCar)
        {
            if (policeCar == null)
            {
                throw new ArgumentNullException("policeCar");
            }

            const byte INPUT_MESSAGES_START_LINE = 13;
            Console.SetCursorPosition(0, INPUT_MESSAGES_START_LINE - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Input info about car and player");

            bool invalidUserInput;
            do
            {
                invalidUserInput = false;
                Console.SetCursorPosition(0, INPUT_MESSAGES_START_LINE);
                Console.ForegroundColor = ConsoleColor.Green;
                try
                {
                    Console.Write("Player name: ");
                    policeCar.DriverName = Console.ReadLine();
                    Console.Write("Car brand: ");
                    policeCar.Brand = Console.ReadLine();
                    Console.Write("Car model: ");
                    policeCar.Model = Console.ReadLine();
                }
                catch (InnerGameException userInputException)
                {
                    invalidUserInput = true;
                    Console.WriteLine("{0}", userInputException.Message);
                    Thread.Sleep(3000);

                    var deleteLine = new string('_', Console.WindowWidth);
                    var deletePosition = new Position()
                    {
                        XCoordinate = 1,
                        YCoordinate = INPUT_MESSAGES_START_LINE
                    };

                    this.Delete(deletePosition, deleteLine);
                }
            } while (invalidUserInput);
        }

        public void PrintUnlockAndStartMessages(ISoundManager gameSounds)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            var listOfMessages = new List<string>()
                {
                    "U key to unlock the car",
                    "S key to start the engine"
                };

            while (true)
            {
                bool wrongKeyPressed = false;
                for (int i = 0; i < listOfMessages.Count; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.WriteLine("Press {0}", listOfMessages[i]);

                    var expectedKey = listOfMessages[i].Substring(0, 1);
                    var userChoice = Console.ReadKey();
                    if (userChoice.Key.ToString() == expectedKey)
                    {
                        if (expectedKey == "U")
                        {
                            gameSounds.PlaySound("car_unlock.wav");
                        }
                        else
                        {
                            gameSounds.PlaySound("start_engine.wav");
                        }

                        continue;
                    }

                    this.PrintWrongKeyMessage();
                    wrongKeyPressed = true;
                    break;
                }

                if (wrongKeyPressed)
                {
                    continue;
                }

                Console.SetCursorPosition(0, 2);
                Console.Write("Press any key to begin the game");
                var someKey = Console.ReadKey();
                Console.Clear();
                break;
            }
        }

        public void PrintFigure(IPosition printPosition, IList<string> figure, ConsoleColor foregroundColor)
        {
            if (printPosition == null || figure == null)
            {
                var parameterName = string.Empty;
                if (printPosition == null)
                {
                    parameterName = "printPosition";
                }
                else
                {
                    parameterName = "figure";
                }

                throw new ArgumentNullException(parameterName);
            }

            if (printPosition.XCoordinate < 0 || printPosition.YCoordinate < 0)
            {
                throw new ArgumentOutOfRangeException("\"printPosition\". ConsoleManager cannot print figure at position with negative coordinates.");
            }

            Console.ForegroundColor = foregroundColor;
            for (int i = 0; i < figure.Count; i++)
            {
                Console.SetCursorPosition(printPosition.XCoordinate, printPosition.YCoordinate + i);
                Console.WriteLine(figure[i]);
            }
        }

        public void Delete(IPosition deletePosition, string deleteLine)
        {
            if (deletePosition == null)
            {
                throw new ArgumentNullException("deletePosition");
            }

            if (deletePosition.XCoordinate < 0 || deletePosition.YCoordinate < 0)
            {
                throw new ArgumentOutOfRangeException("\"deletePosition\". ConsoleManager cannot delete object at position with negative coordinates.");
            }

            const byte LINES_TO_DELETE = 4;
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < LINES_TO_DELETE; i++)
            {
                Console.SetCursorPosition(deletePosition.XCoordinate, deletePosition.YCoordinate + i);
                Console.WriteLine(deleteLine);
            }
        }

        public void PrintGameOverMessage(int playerScore, ICar policeCar)
        {
            if (playerScore < 0)
            {
                throw new ArgumentOutOfRangeException("\"playerScore\" cannot have negative value.");
            }

            if (policeCar == null)
            {
                throw new ArgumentNullException("policeCar");
            }

            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("GAME OVER!");

            var playerPerformance = string.Empty;
            if (10 < playerScore && playerScore < 16)
            {
                playerPerformance = "Good job! ";
            }
            else if (15 < playerScore)
            {
                playerPerformance = "Excellent job! ";
            }

            Console.WriteLine("{0}Officer {1} arrested {2} criminals with {3}.", playerPerformance, policeCar.DriverName, playerScore, policeCar.ToString());
            this.DoNothingForFiveSeconds();
        }

        public bool CheckIfPlayerWantsNewGame()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = true;
            Console.WriteLine("\nPress P key if you want to play again or other key to close...");

            var playerChoice = false;
            var playNewGameKey = Console.ReadKey();
            if (playNewGameKey.Key == ConsoleKey.P)
            {
                playerChoice = true;
            }

            return playerChoice;
        }

        private void PrintGameDescription(IList<string> descriptionLines)
        {
            var cursorPositions = this.SetCursorPositions(descriptionLines);
            for (int i = 0; i < descriptionLines.Count; i++)
            {
                Console.SetCursorPosition(cursorPositions[i].XCoordinate, cursorPositions[i].YCoordinate);
                this.PrintDescriptionLine(descriptionLines[i]);
            }

            Console.WriteLine("\n\n\n");
        }

        private void PrintWrongKeyMessage()
        {
            Console.WriteLine("Wrong key. Try again!");
            Thread.Sleep(1500);
            Console.Clear();
        }

        private void PrintWelcomeLine(string welcomeLine)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var marginLeft = (Console.WindowWidth / 2) - (welcomeLine.Length / 2);
            Console.SetCursorPosition(marginLeft, 1);
            Console.WriteLine("{0}\n\n", welcomeLine.ToUpper());
            Thread.Sleep(1000);
        }

        private void DoNothingForFiveSeconds()
        {
            var fiveSeconds = new TimeSpan(0, 0, 5);
            var timeToStopTheLoop = DateTime.Now + fiveSeconds;
            Console.ForegroundColor = ConsoleColor.Black;
            while (DateTime.Now <= timeToStopTheLoop)
            {
                if (Console.KeyAvailable)
                {
                    var someKey = Console.ReadKey();
                }
            }
        }

        private void PrintDescriptionLine(string descriptionLine)
        {
            for (int i = 0; i < descriptionLine.Length; i++)
            {
                Thread.Sleep(77);
                Console.Write("{0}", descriptionLine[i]);
            }
        }

        private IList<IPosition> SetCursorPositions(IList<string> textLines)
        {
            var result = new List<IPosition>();
            for (int i = 0; i < textLines.Count; i++)
            {
                var x = (Console.WindowWidth / 2) - (textLines[i].Length / 2);
                result.Add(new Position() { XCoordinate = x, YCoordinate = 5 + i });
            }

            return result;
        }
    }
}