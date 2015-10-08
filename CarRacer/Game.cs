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
        // global variables
        protected static int trackOffsetRight = 25;
        protected string highScoreFilePath = @"Scores.txt";
        protected double score = 0;
        protected int lives = 3;
        protected string player = string.Empty;

        public void PlayGame()
        {
            ResetBuffer();
            CascadeLogo();
            ShowMenu();
            //ConsoleView();
            //PrintStringAtPosition(0, 5);
            //PrintCarAtPosition(30, 10, "*", ConsoleColor.Green);
        } // end public void PlayGame()

        private void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

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
            //Console.SetCursorPosition(45, 18);
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

        } // end private void ShowMenu()

        #region MENU_OPTIONS

        private void ChooseDiff()
        {
            Console.Clear();
            PlaySound("Menu");
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
                    InitializeGame(50, 7, "Driver");
                    break;
                case "2":
                    InitializeGame(100, 6, "Racer");
                    break;
                case "3":
                    InitializeGame(150, 5, "F1");
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
        } // end private void ChooseDiff()

        private void PlaySound(string sound)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.SoundLocation = @"..\..\..\Sounds\" + sound + ".wav";
            player.Play();
        }

        private void InitializeGame(int speed, int spawnCarInterval, string sound)
        {
            Console.WriteLine();
            Console.Write(new string(' ', (Console.WindowWidth - "Enter your nickname: ".Length) / 2));
            Console.Write("Enter your nickname: ");
            player = Console.ReadLine();

            score = 0;
            lives = 3;

        RestartRace:
            PlaySound(sound);

            // variables
            List<Car> carsList = new List<Car>();
            List<Coin> collectibles = new List<Coin>();

            // initialize player car
            Car myCar = new Car(34, 35, ConsoleColor.Red);

            Random random = new Random();

            int newCarInterval = 0;
            int newFastCarInterval = 0;
            int newCollectibleInterval = 0;

            while (true)
            {

                if (newCollectibleInterval > 29)
                {
                    Coin bonus = new Coin();
                    int bonusLane = random.Next(0, 5);
                    bonus.X = trackOffsetRight + 2 + 4 * bonusLane;
                    // 21-> where the first lane starts; 2-> half the width of the lane; 4-> the width of one lane
                    bonus.Y = 1;
                    collectibles.Add(bonus);
                    newCollectibleInterval = 0;
                    newCarInterval = -2;
                }

                if (newCarInterval > spawnCarInterval)
                {
                    Car addCar;

                    if (newFastCarInterval > 17)
                    {
                        List<int> freelanes = new List<int>();
                        for (int lane = 1; lane <= 5; lane++)
                        {
                            bool isFree = true;
                            for (int car = 0; car < carsList.Count; car++)
                            {
                                if (carsList[car].X == trackOffsetRight + 1 + (lane - 1) * 4 && carsList[car].Y < 25)
                                {
                                    isFree = false;
                                }
                            }
                            if (isFree)
                            {
                                freelanes.Add(lane);
                            }
                        }
                        int randomFreeLane = freelanes[random.Next(0, freelanes.Count)];
                        addCar = SpawnCar(randomFreeLane);
                        addCar.Speed = 2;
                        newFastCarInterval = 0;
                    }
                    else
                    {
                        addCar = SpawnCar(random.Next(1, 6));
                        addCar.Speed = 1;
                        newCarInterval = 0;
                    }

                    carsList.Add(addCar);
                }

                for (int i = 1; i < 45; i += 2)
                {
                    char symbol = '|';
                    string lines = string.Format("{2}{1}{0}{1}{0}{1}{0}{1}{0}{1}{0}{1}", new string(' ', 3), symbol, new string(' ', trackOffsetRight));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(lines);

                }

                Console.SetCursorPosition(trackOffsetRight + 5 * 5, 2);
                Console.WriteLine(player);
                Console.SetCursorPosition(trackOffsetRight + 5 * 5, 4);
                Console.WriteLine("Score: {0:F2}", score);
                Console.SetCursorPosition(trackOffsetRight + 5 * 5, 6);
                Console.WriteLine("Lives: {0}", lives);
                Console.SetCursorPosition(trackOffsetRight + 5 * 5, 8);
                Console.WriteLine("Speed: {0}", speed);
                Console.SetCursorPosition(2, 1);
                Console.WriteLine("Press Esc to pause");
                // Position = track offset + 4 lanes, 5 chars each + aditional buffer 5

                PrintCar(myCar);
                //PrintCarAtPosition(myCar.X, myCar.Y, "*", myCar.Color);


                foreach (var car in carsList)
                {
                    car.Y += car.Speed;
                    PrintCar(car);
                    //PrintCarAtPosition(car.X, car.Y, "*", car.Color);

                    if (car.X == myCar.X && ((myCar.Y >= car.Y && myCar.Y <= car.Y + 4) || (myCar.Y + 4 >= car.Y && myCar.Y + 4 <= car.Y + 4)))
                    {
                        PrintCarAtPosition(myCar.X, myCar.Y, "X", ConsoleColor.DarkRed);

                        PlaySound("Crash");
                        lives--;

                        Thread.Sleep(2000);

                        if (lives <= 0)
                        {
                            GameOver(score, player);
                        }

                        carsList.Clear();
                        Console.Clear();
                        goto RestartRace;
                    }
                }
                for (int i = 0; i < carsList.Count; i++)
                {
                    if (carsList[i].Y >= Console.WindowHeight - 5)
                    {
                        carsList.Remove(carsList[i]);
                        score += 5;
                    }
                }

                foreach (var bonusCoin in collectibles)
                {
                    bonusCoin.Y++;
                    PrintAtPosition(bonusCoin.X, bonusCoin.Y, bonusCoin.Symbol, bonusCoin.Color);
                    if (bonusCoin.X >= myCar.X && bonusCoin.X <= (myCar.X + 2) && bonusCoin.Y >= myCar.Y && bonusCoin.Y <= (myCar.Y + 3))
                    {
                        PrintCarAtPosition(myCar.X, myCar.Y, "X", ConsoleColor.Yellow);
                        score += 10;
                        bonusCoin.Y = Console.WindowHeight;
                        Console.Beep(659, 125);
                    }

                }
                for (int i = 0; i < collectibles.Count; i++)
                {
                    if (collectibles[i].Y >= Console.WindowHeight - 1)
                    {
                        collectibles.Remove(collectibles[i]);
                    }
                }

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey();
                    while (Console.KeyAvailable)
                    {
                        Console.ReadKey();
                    }

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

                        case ConsoleKey.Escape:
                            {
                                IngameMenu();
                            }
                            break;
                    }

                    //while (Console.KeyAvailable)
                    //{
                    //    pressedKey = Console.ReadKey();
                    //}
                }
                score += (0.2) * speed / 240;
                Thread.Sleep(250 - speed);
                Console.Clear();
                newCarInterval++;
                newFastCarInterval++;
                newCollectibleInterval++;

                // todo: collision detection

            }

        } // end private void InitializeGame(int speed, int newCarInterval)

        private void PrintAtPosition(string toPrint, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine(toPrint);
        }
        private void IngameMenu()
        {
            int pointerX = 1;
            int pointerY = 4;

            int continueRow = 4;
            int endThisGameRow = 5;
            int exitGameRow = 6;

            int menuTopRow = continueRow;
            int menuBotRow = exitGameRow;

            //Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(2, 2);

            PrintAtPosition("Pause", 6, 2);
            PrintAtPosition("=====", 6, 3);
            PrintAtPosition("1. Continue", 2, continueRow);
            PrintAtPosition("2. End race", 2, endThisGameRow);
            PrintAtPosition("3. Exit game", 2, exitGameRow);


            PrintAtPosition(">", pointerX, pointerY);
            ConsoleKeyInfo info = Console.ReadKey();
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key == ConsoleKey.Escape)
                {
                    pointerY = continueRow;
                    break;
                }
                if (info.Key == ConsoleKey.DownArrow && pointerY < menuBotRow)
                {
                    PrintAtPosition(" ", pointerX, pointerY);
                    pointerY++;
                }
                else if (info.Key == ConsoleKey.UpArrow && pointerY > menuTopRow)
                {
                    PrintAtPosition(" ", pointerX, pointerY);
                    pointerY--;
                }

                PrintAtPosition(">", pointerX, pointerY);
                info = Console.ReadKey();
            }

            if (pointerY == exitGameRow)
            {
                Environment.Exit(0);
            }
            else if (pointerY == continueRow)
            {
                PrintAtPosition("Resuming game in:", 2, exitGameRow + 2);
                for (int i = 1; i <= 3; i++)
                {
                    PrintAtPosition(i.ToString(), "Resuming game in:".Length / 2 + 2, exitGameRow + 4);
                    Thread.Sleep(1000);
                }
                return;
            }
            else if (pointerY == endThisGameRow)
            {
                GameOver(score, player);
            }


            Console.ReadLine();
        }


        private void AboutGame()
        {
            Console.Clear();
            Console.WriteLine(" CAR RACER - a TEAM \"ELATHA\" project for AdvancedC# course in SoftUni, Sept.21015");
            Console.WriteLine();
            Console.WriteLine("Authors (in alphabetical order):");
            Console.WriteLine("1) Aleksandar.Tanev");
            Console.WriteLine("2) bulgaria_mitko");
            Console.WriteLine("3) pgboev");
            Console.WriteLine("4) PreslavPetkov");
            Console.WriteLine("5) Rextor92");
            Console.WriteLine("6) Tsvyatko");
            Console.WriteLine("7) yanchev_i");
            Console.WriteLine();
            Console.WriteLine("Press ESC to return to Main menu.");
            
            while (true)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                if (pressedKey.Key == ConsoleKey.Escape)
                {
                    ShowMenu();
                }
            }
        } // end private void AboutGame()

        #region HIGHSCORE_SYSTEM

        private void SaveScore(int score, string player)
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
        } // end private void SaveScore(int score, string player)

        private void ViewHighScores()
        {

            PlaySound("Credits_HighScore");
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            if (File.Exists(highScoreFilePath))
            {
                string[] scores = File.ReadAllLines(highScoreFilePath);

                centerText("=================");
                centerText("=== CAR RACER ===");
                centerText("=================");

                Console.WriteLine();
                centerText("Highscores");
                Console.WriteLine();
                centerText("====");

                for (int i = 0; i < 10 && i < scores.Length; i++)
                {
                    centerText(scores[i]);
                }
                Console.WriteLine();
                centerText("Press any key to go back to menu");

                ConsoleKeyInfo keyPressed = Console.ReadKey();

                ShowMenu();
            }
            else
            {
                centerText("=================");
                centerText("=== CAR RACER ===");
                centerText("=================");
                Console.WriteLine();
                centerText("There are no highscores yet");
                Console.WriteLine();
                centerText("====");
                Console.WriteLine();
                centerText("Press any key to go back to menu");

                ConsoleKeyInfo keyPressed = Console.ReadKey();

                ShowMenu();
            }

        } // end private void ViewHighScores()

        private void CheckForHighscore(int score, string player)
        {

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
        } // end private void CheckForHighscore(int score, string player)

        #endregion

        #endregion

        #region INGAME_METHODS

        //Set an ingame box Method
        private void ConsoleView()
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
        } // end private void ConsoleView()

        private void ClearBox()
        {
            for (int i = 1; i < Console.WindowHeight - 2; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(2, i);
                Console.Write(new string(' ', Console.WindowWidth - 4));
            }
        } // end private void ClearBox()

        private void GameOver(double score, string player)
        {   // Endgame screen?
            SaveScore((int)score, player);

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
        } // end private void GameOver(int score, string player)

        private Car SpawnCar(int i)
        {
            int laneWidth = 4;

            List<ConsoleColor> colorPalette = new List<ConsoleColor>() { ConsoleColor.Blue, ConsoleColor.Cyan,
                                                    ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.White };
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
        } // end private Car SpawnCar(int i)

        private void CascadeLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            string logo = @"                                                                                                 =================
                                                       #@@@@,                                    === CAR RACER ===
                                                       @;`;@@                                    =================
                            ,@#';,.                      @@@@    
                        `@@@@@@@@@@@@@@@@@@@@' :@@@@@@@@@@@@#                                  Welcome to Car Racer!
                      @@@@@@@@@@@@@@@@@@@; .#@@@@@@@@@@    ,@@@          
                   ` '@@@@@@@@@@@@@+,  :@@@@@@@@@@@@@, #@@@@; @@                                        MENU
            ;@@@@@@@@@@',`     .:'@@@@@@@@@@@@@@@@@@ #@@@#@@@@`@`                                       ====
         +@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @@#     `@@.                                    1. New Game  
       @.@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#,@+        @@                                    2. Highscore
     #` @@@@@     '@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @@          @#                                     3. About
   `' `@@@@' @@@@#  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ @.          @@                                     4. Exit 
  ' ,@@@@@+.@@@+@@@  @@@@@@@@@@@@@@@@@@@@@@@@@@@@@:@           #@                                   
 #@@@@@@@@ @@     @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@@;@           +@                               Enter menu number:
+@@@@@@@@@@@       @,`@@@@@@@@@@@@@@@@@@@@@@@@@@@@.@           @@                                
#@@@@@@@@;@@       @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@ @+          @@                                  
 @@@@@@@@'@@       @@ @@@@@@@@@@@@@@@@@@@@@@@@@@@@'@@         +@,                                  
 @@@@@@@@@@@       @#:@@@@@@@@@@@@@#+@@@@@@@@@@@@@@ @@       ;@@  
 @` ,@@@@@;@       @ @@@@@@;`                       .@@+   `@@@                                 
   `;#@@@@ @@`   `@# @;                               @@@@@@@@    
            @@@@@@# `                                   '@#:      
              +@'                                                 ";

            PlaySound("Intro");
            Print(logo);
        } // end private void CascadeLogo()

        static void Print(string str)
        {
            string[] splittedString = str.Split('\n');
            for (int cycle = 0; cycle <= Console.WindowWidth; cycle++)
            {
                for (int i = 0; i < splittedString.Length; i++)
                {
                    if (cycle < splittedString[i].Length)
                    {
                        string substring = splittedString[i].Substring(cycle);
                        if (substring.Length > Console.WindowWidth - 1)
                        {
                            substring = substring.Substring(0, Console.WindowWidth - 1);
                        }
                        Console.WriteLine(substring);
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }

                if (cycle == 0)
                {
                    Thread.Sleep(1200);
                }
                else
                {
                    Thread.Sleep(25);
                }

                if (cycle < Console.WindowWidth)
                {
                    Console.Clear();
                }
            }
        }

        private void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        } // end private void centerText(String text)

        // FOR DELETE!
        private void PrintLogo(int x, int y, string logo)
        {
            Console.SetCursorPosition(x, y);




        } // end private void PrintLogo(int x, int y)

        #endregion

        #region HELPER_METHODS

        private void ResetBuffer()
        {
            Console.Title = "Car Racer v1.0";
            Console.CursorVisible = false;
            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;
        } // end private void ResetBuffer()

        private void PrintCar(Car car)
        {
            int counterY = 0;
            int counterX = 0;
            for (int i = 0; i < car.Vehicle.Length; i++)
            {
                Console.SetCursorPosition(car.X + counterX, car.Y + counterY);
                Console.Write(car.Vehicle[i]);
                counterX++;
                if (car.Vehicle[i] == '\n')
                {
                    counterY++;
                    counterX = 0;

                }
            }
        } // end private void PrintCar(Car car)


        private void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)
        {
            int digit = 0;
            //Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = color;
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
        } // end private void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)

        private void PrintPoints(int points)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 0);
            Console.Write("Points : {0}", points);
        } // end private void PrintPoints(int points)s

        private void PrintLives(int lives)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, 0);
            Console.Write("Level : {0}", lives);
            Console.ForegroundColor = ConsoleColor.White;
        } // end private void PrintLives(int lives)

        private void PrintStringAtPosition(int points, int lives)
        {
            PrintPoints(points);
            PrintLives(lives);
        } // end private void PrintStringAtPosition(int x, int y, string text, ConsoleColor color)

        private void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            // prints single char at certain position
            // useful for lane separators ( '|' ) and for collecting bonuses (lives?)
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        } // end private void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)

        #endregion

    } // end class Game
} // end namespace CarRacer
