using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace CarRacer
{
    class Game
    {
        protected string highScoreFilePath = @"Scores.txt";

        public void PlayGame()
        {
            ResetBuffer();
            ShowMenu();
            //ConsoleView();
            //PrintStringAtPosition(0, 5);
            //PrintCarAtPosition(30, 10, "*", ConsoleColor.Green);
        } // end public void PlayGame()

        void ShowMenu()
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

        void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }

        void PrintLogo(int x, int y)
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

        void ChooseDiff()
        {
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
                    InitializeGame();
                    break;
                case "2":
                    InitializeGame();
                    break;
                case "3":
                    InitializeGame();
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

        void InitializeGame()
        {
            Console.Write("Enter your nickname...");
            string player = Console.ReadLine();

            // variables
            List<Car> carsList = new List<Car>();
            Random rnd = new Random();
            double score = 0;
            int speed = 100;
            int lives = 1;

            // initialize player car

            while (true)
            {
                bool hitted = false;

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

        #endregion

        #region INGAME_METHODS

        void ConsoleView()
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
        } // end void ConsoleView()

        void ClearBox()
        {
            for (int i = 1; i < Console.WindowHeight - 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(2, i);
                Console.Write(new string(' ', Console.WindowWidth - 4));
            }
        } // end void ClearBox() 

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
            return new Car();
            // create an array of type ConsoleColor, add some colors (reserve one for your own car)
            // randomly pick an array[index] and create a new Car object manually 
            // Car spawnedCar = new Car(); spawnedCar.Y = 2; spawnedCar.Color = colors[index], switch (i)
            // switch (i) to place the spawned car in a lane of it's own!
            // return spawnedCar
        } // end void SpawnCar()

        #endregion

        #region HELPER_METHODS

        void ResetBuffer()
        {
            Console.Title = "Car Racer v1.0";
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;
        } // end void ResetBuffer()

        void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)
        {
            int digit = 0;
            //Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = color;
            while (digit < 4)
            {
                if (digit % 2 == 0)
                {
                    Console.SetCursorPosition(x, y++);
                    Console.WriteLine("  " + thing);
                }
                else
                {
                    Console.SetCursorPosition(x, y++);
                    Console.WriteLine(string.Format("{0} {0} {0}", thing));
                }
                digit++;
            }
            // prints a car
            // new lines in the string Car.Vehicle reset the CursorPosition :(
            // manual car drawing until we find a solution :/
        } // end void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)

        void PrintPoints(int points)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 0);
            Console.Write("Points : {0}", points);
        } // end void PrintPoints(int points)s

        void PrintLives(int lives)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 0);
            Console.Write("Level : {0}", lives);
            Console.ForegroundColor = ConsoleColor.White;
        } // end void PrintLives(int lives)

        void PrintStringAtPosition(int points, int lives)
        {
            PrintPoints(points);
            PrintLives(lives);
        } // end void PrintStringAtPosition(int x, int y, string text, ConsoleColor color)

        void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            // prints single char at certain position
            // useful for lane separators ( '|' ) and for collecting bonuses (lives?)
        } // end void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)

        #endregion

        #region HIGHSCORE_SYSTEM

        void SaveScore(int score, string player)
        {
            Dictionary<int, List<string>> scores = new Dictionary<int, List<string>>();
            List<string> subList = new List<string>();

            if (File.Exists(highScoreFilePath))
            {
                string readText = File.ReadAllText(highScoreFilePath);
                Regex regex = new Regex(@"(\w+) (\d+)");
                MatchCollection matches = regex.Matches(readText);

                foreach (Match match in matches)
                {
                    subList = new List<string>();

                    if (scores.ContainsKey(int.Parse(match.Groups[2].ToString())))
                    {
                        subList = scores[int.Parse(match.Groups[2].ToString())];
                    }

                    subList.Add(match.Groups[1].ToString());
                    scores[int.Parse(match.Groups[2].ToString())] = subList;
                }
            }

            subList = new List<string>();

            if (scores.ContainsKey(score))
            {
                subList = scores[score];
            }

            subList.Add(player);
            scores[score] = subList;

            StringBuilder highScores = new StringBuilder();
            int playerPlace = 1;

            foreach (var item in scores.OrderByDescending(x => x.Key))
            {
                foreach (var players in item.Value)
                {
                    highScores.Append(string.Format(playerPlace + ". " + players + " " + item.Key + Environment.NewLine));
                    playerPlace++;
                }
            }
            File.WriteAllText(highScoreFilePath, highScores.ToString());
        }

        void ViewHighScores()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            if (File.Exists(highScoreFilePath))
            {
                string[] scores = File.ReadAllLines(highScoreFilePath);

                Console.WriteLine();
                Console.WriteLine("Highscores");
                Console.WriteLine();

                for (int i = 0; i < 10 && i < scores.Length; i++)
                {
                    Console.WriteLine(scores[i]);
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to go back to menu");

                ConsoleKeyInfo keyPressed = Console.ReadKey();

                ShowMenu();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Highscores");
                Console.WriteLine();
                Console.WriteLine("There are no highscores yet");
                Console.WriteLine();
                Console.WriteLine("Press any key to go back to menu");

                ConsoleKeyInfo keyPressed = Console.ReadKey();

                ShowMenu();
            }

        }

        void CheckForHighscore(int score, string player)
        {
            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;
            if (File.Exists(highScoreFilePath))
            {
                string[] highestScore = File.ReadAllLines(highScoreFilePath);
                Regex regex = new Regex(@"(\w+) (\d+)");
                Match match = regex.Match(highestScore[0]);

                if (score >= int.Parse(match.Groups[2].ToString()))
                {
                    string greating = string.Format("  Congratulations " + player + "!  ");
                    int spacesCount = (greating.Length - string.Format("Highscore: " + score).Length) / 2;
                    string highScore = string.Format(new string(' ', spacesCount) + "Highscore: " + score + new string(' ', spacesCount));
                    Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2 + 1);
                    Console.Write(greating);
                    Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2);
                    Console.Write(new string(' ', greating.Length - 1));
                    Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2 - 1);
                    Console.Write(highScore);
                }
                else
                {
                    return;
                }
            }
            else
            {
                string greating = string.Format("     Congratulations " + player + "!     ");
                int spacesCount = (greating.Length - string.Format("Highscore: " + score).Length) / 2;
                string highScore = string.Format(new string(' ', spacesCount) + "Highscore: " + score + new string(' ', spacesCount));
                Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2 + 1);
                Console.Write(greating);
                Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2);
                Console.Write(new string(' ', greating.Length - 1));
                Console.SetCursorPosition((Console.WindowWidth - greating.Length) / 2, Console.WindowHeight / 2 - 1);
                Console.Write(highScore);
            }
        }

        #endregion

    } // end class Game
} // end namespace CarRacer
