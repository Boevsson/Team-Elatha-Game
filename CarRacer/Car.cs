using System;

namespace CarRacer
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
            this.color = color;
            this.X = x;
            this.Y = y;
        }
        public Car() { }

    } // end class Car
} // end namespace CarRacer
