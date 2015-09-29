using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacer
{
    class Game
    {
        public static int trackOffsetRight = 25;

        public void PlayGame()
        {
            ResetBuffer();
            ShowMenu();
            //ConsoleView();
            PrintStringAtPosition(0, 5);
            PrintCarAtPosition(30, 10, "*", ConsoleColor.Green);
        } // end public void PlayGame()

        public void ShowMenu()
        {
            // Clear the console and assign foreground color. Welcome the user and offer numeric choice for
            // new game, highscore table, about, exit (environment.exit(0))
            // read user input as string and switch (default case calls ShowMenu() again)
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;

            int position = 0;

            for (int i = 0; i < 5; i++)
            {
                PrintLogo(position, position + i);
                Thread.Sleep(1000);
                Console.Clear();
            }

            centerText("=================");
            centerText("=== CAR RACER ===");
            centerText("=================");
            Console.WriteLine();
            centerText("Welcome to Car Racer!");
            Console.WriteLine();
            centerText("MENU");
            centerText("====");
            centerText("1. New Game");
            centerText("2. Highscore");
            centerText("3. About");
            centerText("4. Exit");


            Console.WriteLine();
            Console.Write(new string(' ', (Console.WindowWidth - "Enter menu number: ".Length) / 2));
            Console.Write("Enter menu number: ");
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    ChooseDiff();
                    break;
                case "2":
                    ViewHighScores();
                    break;
                case "3":
                    AboutGame();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Thread.Sleep(1000);
                    ShowMenu();
                    break;
            }

        } // end void ShowMenu()

        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }

        static void PrintLogo(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            centerText("=================");
            centerText("=== CAR RACER ===");
            centerText("=================");
            Console.WriteLine(@"
                                                        #@@@@,     
                                                        @;`;@@     
                             ,@#';,.                      @@@@     
                         `@@@@@@@@@@@@@@@@@@@@' :@@@@@@@@@@@@#     
                       @@@@@@@@@@@@@@@@@@@; .#@@@@@@@@@@    ,@@@   
                    ` '@@@@@@@@@@@@@+,  :@@@@@@@@@@@@@, #@@@@; @@  
             ;@@@@@@@@@@',`     .:'@@@@@@@@@@@@@@@@@@ #@@@#@@@@`@` 
          +@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @@#     `@@.  
        @.@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#,@+        @@  
      #` @@@@@     '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @@          @# 
    `' `@@@@' @@@@#  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @.          @@ 
   ' ,@@@@@+.@@@+@@@  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@:@           #@ 
  #@@@@@@@@ @@     @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@@;@           +@ 
 +@@@@@@@@@@@       @,`@@@@@@@@@@@@@@@@@@@@@@@@@@@@.@           @@ 
 #@@@@@@@@;@@       @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@ @+          @@ 
  @@@@@@@@'@@       @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@'@@         +@, 
  @@@@@@@@@@@       @#:@@@@@@@@@@@@@#+@@@@@@@@@@@@@@ @@       ;@@  
  @` ,@@@@@;@       @ @@@@@@;`                       .@@+   `@@@   
    `;#@@@@ @@`   `@# @;                               @@@@@@@@    
             @@@@@@# `                                   '@#:      
               +@'                                                 ");
            // prints single char at certain position
            // useful for lane separators ( '|' ) and for collecting bonuses (lives?)
        } // end void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)

        public void ChooseDiff()
        {
            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;

            PrintLogo(0, 0);
            Thread.Sleep(1000);
            Console.Clear();

            centerText("=================");
            centerText("=== CAR RACER ===");
            centerText("=================");
            Console.WriteLine();
            centerText("Choose difficulty");
            Console.WriteLine();
            centerText("====");
            centerText("1. Driver");
            centerText("2. Racer");
            centerText("3. F1");
            centerText("4. Go back");

            Console.WriteLine();
            Console.Write(new string(' ', (Console.WindowWidth - "Enter menu number: ".Length) / 2));
            Console.Write("Enter menu number: ");
            string userChoice = Console.ReadLine();

            switch (userChoice)
            {
                case "1":
                    InitializeGame(200, 7);
                    break;
                case "2":
                    InitializeGame(175, 6);
                    break;
                case "3":
                    InitializeGame(150, 5);
                    break;
                case "4":
                    ShowMenu();
                    break;
                default:
                    Console.WriteLine("Invalid input!");
                    Thread.Sleep(1000);
                    ChooseDiff();
                    break;
            }
        }

        #region MENU_OPTIONS

        void InitializeGame(int speed, int newCarInterval)
        {
            // get user nickname (for highscore)

            // variables
            List<Car> carsList = new List<Car>();
            List<Coin> collectibles = new List<Coin>();

            double score = 0;
            int lives = 1;

            // initialize player car
            Car myCar = new Car(34, 35, ConsoleColor.Red);

            Random random = new Random();

            int newCollectibleInterval = 0;

            while (true)
            {
                bool hitted = false;

                if (newCollectibleInterval > 29)
                {
                    Coin bonus = new Coin();
                    int bonusLane = random.Next(0, 5);
                    bonus.X = trackOffsetRight + 2 + 4 * bonusLane; // 21-> where the first lane starts; 2-> half the width of the lane; 4-> the width of one lane
                    bonus.Y = 1;
                    collectibles.Add(bonus);
                    newCollectibleInterval = 0;
                    newCarInterval = -2;
                }

                if (newCarInterval > 5)
                {
                    Car addCar = SpawnCar(random.Next(1, 6));
                    carsList.Add(addCar);
                    newCarInterval = 0;
                }

                for (int i = 1; i < 45; i += 2)
                {
                    char symbol = '|';
                    string lines = string.Format("{2}{1}{0}{1}{0}{1}{0}{1}{0}{1}{0}{1}", new string(' ', 3), symbol, new string(' ', trackOffsetRight));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(lines);

                }

                PrintCarAtPosition(myCar.X, myCar.Y, "*", myCar.Color);



                foreach (var car in carsList)
                {
                    PrintCarAtPosition(car.X, car.Y, "*", car.Color);
                    car.Y++;
                }
                for (int i = 0; i < carsList.Count; i++)
                {
                    if (carsList[i].Y > Console.WindowHeight - 5)
                    {
                        carsList.Remove(carsList[i]);
                    }
                }

                foreach (var bonusCoin in collectibles)
                {
                    PrintAtPosition(bonusCoin.X, bonusCoin.Y, bonusCoin.Symbol, bonusCoin.Color);
                    bonusCoin.Y++;
                }
                for (int i = 0; i < collectibles.Count; i++)
                {
                    if (collectibles[i].Y > Console.WindowHeight - 1)
                    {
                        collectibles.Remove(collectibles[i]);
                    }
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();

                    switch (pressedKey.Key)
                    {                    
                        case ConsoleKey.LeftArrow:
                            {
                                if (myCar.X > trackOffsetRight + 1)
                                {
                                    myCar.X -= 4;
                                }
                                break;
                            }                           
                        case ConsoleKey.UpArrow:
                            {
                                if (myCar.Y > 10)
                                {
                                    myCar.Y -= 1;
                                }
                            }   
                            break;
                        case ConsoleKey.RightArrow:
                            {
                                if (myCar.X < trackOffsetRight + 16)
                                {
                                    myCar.X += 4;
                                }
                            }
                            break;
                        case ConsoleKey.DownArrow:
                            {
                                if (myCar.Y < 40)
                                {
                                    myCar.Y += 1;
                                }
                            }
                            break;
                    }

                    //while (Console.KeyAvailable)
                    //{
                    //    pressedKey = Console.ReadKey();
                    //}
                }

                Thread.Sleep(speed);
                Console.Clear();
                newCarInterval++;
                newCollectibleInterval++;

                // logic behind spawning cars and buffs
                // spawn a car
                // spawn +1 live buffs

                // move player car (ConsoleKeyInfo
                // ConsoleKey.LeftArrow, RighthArrow, UpArrow, DownArrow)

                

                // move other cars, buffs
                // create new list of cars, foreach element in carList create new car object 

                // collision detection
                // lives--, possible GameOver(score, username)

                // clear all after each thread.sleep

                // draw player car ( collision => clear other cars from screen, reduce speed, visually show hit)

                // draw other cars, buffs

                // draw score and lives

                // control speed and score
            }

        } // end void InitializeGame()

        void AboutGame()
        {
            // some info about the game - name, represents group project for AdvancedC#, softuni
            // authors
            // 1) ...
            // 2) ...
            // etc
        } // end void AboutMe()

        void ViewHighScores()
        {
            // implement some high-score system, preferably reading from .txt
            // splitting usernames and scores (regex?), dictionary
        } // end void ViewHighScores()

        #endregion

        #region INGAME_METHODS
        //Set an ingame box Method
        static void ConsoleView()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            int height = Console.BufferHeight;
            int width = Console.BufferWidth;
            for (int i = 0; i < width; i++)
            {
                Console.Write("_");
            }
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("-|");
                Console.SetCursorPosition(width - 2, i);
                Console.Write("|-");
                Console.SetCursorPosition(0, i);
            }
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            char symbol = '\u00AF';
            for (int i = 0; i < width; i++)
            {
                Console.Write(symbol);
            }
        }
        //Clear the box Method 
        static void ClearBox()
        {
            for (int i = 1; i < Console.WindowHeight - 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(2, i);
                Console.Write(new string(' ', Console.WindowWidth - 4));
            }
        }

        void GameOver(double score, string player)
        {   // Endgame screen?
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format(@"
              ___           ___           ___           ___     
             /  /\         /  /\         /__/\         /  /\    
            /  /:/_       /  /::\       |  |::\       /  /:/_   
           /  /:/ /\     /  /:/\:\      |  |:|:\     /  /:/ /\  
          /  /:/_/::\   /  /:/~/::\   __|__|:|\:\   /  /:/ /:/_ 
         /__/:/__\/\:\ /__/:/ /:/\:\ /__/::::| \:\ /__/:/ /:/ /\
         \  \:\ /~~/:/ \  \:\/:/__\/ \  \:\~~\__\/ \  \:\/:/ /:/
          \  \:\  /:/   \  \::/       \  \:\        \  \::/ /:/ 
           \  \:\/:/     \  \:\        \  \:\        \  \:\/:/  
            \  \::/       \  \:\        \  \:\        \  \::/   
             \__\/         \__\/         \__\/         \__\/    
              ___                        ___           ___     
             /  /\          ___         /  /\         /  /\    
            /  /::\        /__/\       /  /:/_       /  /::\   
           /  /:/\:\       \  \:\     /  /:/ /\     /  /:/\:\  
          /  /:/  \:\       \  \:\   /  /:/ /:/_   /  /:/~/:/  
         /__/:/ \__\:\  ___  \__\:\ /__/:/ /:/ /\ /__/:/ /:/___
         \  \:\ /  /:/ /__/\ |  |:| \  \:\/:/ /:/ \  \:\/:::::/
          \  \:\  /:/  \  \:\|  |:|  \  \::/ /:/   \  \::/~~~~ 
           \  \:\/:/    \  \:\__|:|   \  \:\/:/     \  \:\     
            \  \::/      \__\::::/     \  \::/       \  \:\    
             \__\/           ~~~~       \__\/         \__\/    

        "));
            Console.WriteLine("Congratulations, {0}! Your score is {1:F0}", player, score);
            Console.WriteLine("Press ENTER to return to main menu.");
            Console.ReadLine();
            ShowMenu();
        } // end void GameOver()

        Car SpawnCar(int i)
        {
            int laneWidth = 4;

            List<ConsoleColor> colorPalette = new List<ConsoleColor>() { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.White };
            Random random = new Random();

            Car spawnedCar = new Car();
            spawnedCar.Y = 1;
            spawnedCar.Color = colorPalette[random.Next(colorPalette.Count)];

            //int lane = random.Next(1, 6);
            switch (i)
            {
                case 1: spawnedCar.X = trackOffsetRight + laneWidth * 0 + 1; break;
                case 2: spawnedCar.X = trackOffsetRight + laneWidth * 1 + 1; break;
                case 3: spawnedCar.X = trackOffsetRight + laneWidth * 2 + 1; break;
                case 4: spawnedCar.X = trackOffsetRight + laneWidth * 3 + 1; break;
                case 5: spawnedCar.X = trackOffsetRight + laneWidth * 4 + 1; break;

            }

            return spawnedCar;
        } // end void SpawnCar()

        #endregion

        #region HELPER_METHODS

        void ResetBuffer()
        {
            // Title for the console, curser visibility options,
            // console window buffers and size, other cosmetics
        } // end void ResetBuffer()

        void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)
        {
            int digit = 0;
            while (digit < 4)
            {
                if (digit % 2 == 0)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.ForegroundColor = color;
                    Console.WriteLine(string.Format(" {0} ", thing));
                }
                else
                {
                    Console.SetCursorPosition(x, y++);
                    Console.ForegroundColor = color;
                    Console.WriteLine(string.Format("{0}{0}{0}", thing));
                }
                digit++;
            }
            // prints a car
            // new lines in the string Car.Vehicle reset the CursorPosition :(
            // manual car drawing until we find a solution :/
        } // end void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)
        static void PrintPoints(int points)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 0);
            Console.Write("Points : {0}", points);
        }

        static void PrintLives(int lives)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 0);
            Console.Write("Level : {0}", lives);
            Console.ForegroundColor = ConsoleColor.White;

        }
        void PrintStringAtPosition(int points, int lives)
        {
            PrintPoints(points);
            PrintLives(lives);
            // prints a string at certain position
            // useful for scoreboard
        } // end void PrintStringAtPosition(int x, int y, string text, ConsoleColor color)

        void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            // prints single char at certain position
            // useful for lane separators ( '|' ) and for collecting bonuses (lives?)
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        } // end void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)

        #endregion

    } // end class Game
} // end namespace CarRacer