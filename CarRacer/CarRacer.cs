using System;

namespace CarRacer

{
    class CarRacer
    {
        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = 45;
            Console.BufferWidth = Console.WindowWidth = 70;
            Game game1 = new Game();
            game1.PlayGame();
        }
    } // end class CarRacer
} // end namespace CarRacer