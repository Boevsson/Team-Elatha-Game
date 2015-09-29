using System;

namespace CarRacer
{
    class Coin
    {
        // variables
        private char symbol = '$';
        private int x;
        private int y;
        private ConsoleColor color = ConsoleColor.Yellow;

        // getters & setters
        public ConsoleColor Color
        {
            get
            {
                return this.color;
            }
        }

        public char Symbol
        {
            get
            {
                return this.symbol;
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
        public Coin(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public Coin() { }
    }
}
