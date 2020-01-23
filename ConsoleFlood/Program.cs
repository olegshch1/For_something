using System;

namespace ConsoleFlood
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Use numbers 1 - 6 for changing values");
            Console.WriteLine("Use SpaceBar for new game");
            Console.WriteLine("Enter the Map size");
            Console.CursorVisible = false;

            //запуск eventloop и игры
            var eventLoop = new ConsoleFlood.Event();
            var game = new ConsoleFlood.Game();           
            game.Start();

            eventLoop.OneHandler += game.One;
            eventLoop.TwoHandler += game.Two;
            eventLoop.ThreeHandler += game.Three;
            eventLoop.FourHandler += game.Four;
            eventLoop.FiveHandler += game.Five;
            eventLoop.SixHandler += game.Six;
            eventLoop.SpaceHandler += game.Space;

            eventLoop.OneHandler += game.Print;
            eventLoop.TwoHandler += game.Print;
            eventLoop.ThreeHandler += game.Print;
            eventLoop.FourHandler += game.Print;
            eventLoop.FiveHandler += game.Print;
            eventLoop.SixHandler += game.Print;
            eventLoop.SpaceHandler += game.Print;

            eventLoop.Run();
        }
    }
}
