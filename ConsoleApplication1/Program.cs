using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    class Test
    {
        class Car
        {
            // variables
            private string vehicle = " * \n***\n * \n***";
            private int x;
            private int y;
            private ConsoleColor color;

            // getters & setters
            public ConsoleColor Color
            {
                get
                {
                    return color;
                }

                set
                {
                    color = value;
                }
            }

            public string Vehicle
            {
                get
                {
                    Console.ForegroundColor = Color;
                    return vehicle;
                }

                private set
                {
                    vehicle = value;
                }
            }

            public int Y
            {
                get
                {
                    return y;
                }

                set
                {
                    y = value;
                }
            }

            public int X
            {
                get
                {
                    return x;
                }

                set
                {
                    x = value;
                }
            }

            // constructors
            public Car(int x, int y, ConsoleColor color)
            {
                this.Color = color;
                this.X = x;
                this.Y = y;
            }
            public Car() { }

        } // end class Car

        static void Main(string[] args)
        {
            List<Car> cars = new List<Car>();

            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;

            Random random = new Random();

            int x = random.Next(5, 65);
            int y = random.Next(5, 45);
            string vehicle = " * \n***\n * \n***";

            //Console.WriteLine(vehicle);

            List<ConsoleColor> colorPalette = new List<ConsoleColor>() { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Yellow };

            ConsoleColor color = colorPalette[random.Next(colorPalette.Count)];

            int interval = 4;
            //Car newCar = SpawnCar(random.Next(1, 6));
            //cars.Add(newCar);

            while (true)
            {
                if (interval > 5)
                {
                    Car addCar = SpawnCar(random.Next(1, 6));
                    cars.Add(addCar);
                    interval = 0;
                }

                for (int y1 = 1; y1 < 45; y1 += 2)
                {
                    char symbol = '|';
                    ConsoleColor roadMarksColor = ConsoleColor.Gray;
                    for (int x1 = 5; x1 < 27; x1 += 4)
                    {
                        PrintAtPosition(x1, y1, symbol, roadMarksColor);
                    }
                }

                foreach (var car in cars)
                {
                    PrintCarAtPosition(car.X, car.Y, car.Color);
                    car.Y++;
                    if (car.Y > 40)
                    {
                        cars.Remove(car);
                        break;
                    }
                }

                Thread.Sleep(200);
                Console.Clear();
                interval++;
            }

        }

        static Car SpawnCar(int i)
        {
            int magicNumber = 6;
            int laneWidth = 4;

            List<ConsoleColor> colorPalette = new List<ConsoleColor>() { ConsoleColor.Blue, ConsoleColor.Cyan, ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.White, ConsoleColor.Yellow };
            Random random = new Random();

            Car spawnedCar = new Car();
            spawnedCar.Y = 2;
            spawnedCar.Color = colorPalette[random.Next(colorPalette.Count)];

            //int lane = random.Next(1, 6);
            switch (i)
            {
                case 1: spawnedCar.X = magicNumber; break;
                case 2: spawnedCar.X = magicNumber + laneWidth; break;
                case 3: spawnedCar.X = magicNumber + laneWidth * 2; break;
                case 4: spawnedCar.X = magicNumber + laneWidth * 3; break;
                case 5: spawnedCar.X = magicNumber + laneWidth * 4; break;

            }

            return spawnedCar;
        } // end void SpawnCar()

        static void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
            // prints single char at certain position
            // useful for lane separators ( '|' ) and for collecting bonuses (lives?)
        } // end void PrintAtPosition(int x, int y, char symbol, ConsoleColor color)

        static void PrintCarAtPosition(int x, int y, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(" * ");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("***");
            Console.SetCursorPosition(x, y + 2);
            Console.Write(" * ");
            Console.SetCursorPosition(x, y + 3);
            Console.Write("***");

            //int digit = 0;
            //while (digit < 4)
            //{
            //    if (digit % 2 == 0)
            //    {
            //        Console.SetCursorPosition(x, y++);
            //        Console.ForegroundColor = color;
            //        Console.WriteLine("  " + thing);
            //    }
            //    else
            //    {
            //        Console.SetCursorPosition(x, y++);
            //        Console.ForegroundColor = color;
            //        Console.WriteLine(string.Format("{0} {0} {0}", thing));
            //    }
            //    digit++;
            // prints a car
            // new lines in the string Car.Vehicle reset the CursorPosition :(
            // manual car drawing until we find a solution :/
        } // end void PrintCarAtPosition(int x, int y, string thing, ConsoleColor color)
    }
}