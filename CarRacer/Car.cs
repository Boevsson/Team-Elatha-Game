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
        private int speed;

        // getters & setters
        public int Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

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

        public Car (Car car1)
        {
            this.x = car1.x;
            this.y = car1.y;
            this.color = car1.color;
            this.vehicle = car1.vehicle;
        }

    } // end class Car
} // end namespace CarRacer
