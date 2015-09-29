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
        public string highScoreFilePath = @"Scores.txt";

        public void PlayGame()
        {
            ResetBuffer();
            ShowMenu();
            //  PrintStringAtPosition(0, 5);
            //  PrintCarAtPosition(30, 10, "*", ConsoleColor.Green);
        } // end public void PlayGame()

        void ShowMenu()
        {
            // Clear the console and assign foreground color. Welcome the user and offer numeric choice for
            // new game, highscore table, about, exit (environment.exit(0))
            // read user input as string and switch (default case calls ShowMenu() again)
        } // end void ShowMenu()

        #region MENU_OPTIONS

        void InitializeGame()
        {
            // get user nickname (for highscore)

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
                    Console.WriteLine("  " + thing);
                }
                else
                {
                    Console.SetCursorPosition(x, y++);
                    Console.ForegroundColor = color;
                    Console.WriteLine(string.Format("{0} {0} {0}", thing));
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

        #endregion

    } // end class Game
} // end namespace CarRacer