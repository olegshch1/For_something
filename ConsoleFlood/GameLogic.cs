using System;

namespace ConsoleFlood
{
    public class Game
    {
        //Game Map
        public int[][] Map { get; set; }

        //Counting moves
        public int Count { get; set; }

        //Markers
        public bool[][] Used { get; set; }

        /// <summary>
        /// считывание карты
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public Game() => NewGame();

        public void NewGame()
        {
            Console.Clear();
            Console.WriteLine("Use numbers 1 - 6 for changing values");
            Console.WriteLine("Use SpaceBar for new game");
            Console.WriteLine("Enter the Map size");
            int size = int.Parse(Console.ReadLine());
            Map = new int[size][];
            var random = new Random();
            for (int i = 0; i < size; i++)
            {
                Map[i] = new int[size];
                for (int j = 0; j < size; j++)
                {
                    Map[i][j] = random.Next(1, 7);
                }
            }

            Used = new bool[size][];
            for (int k = 0; k < size; k++)
            {
                Used[k] = new bool[size];
                for (int p = 0; p < size; p++)
                {
                    Used[k][p] = false;
                }
            }
        }

        /// <summary>
        /// Change colors
        /// </summary>
        public void One(object sender, EventArgs args) => Move(1);
        public void Two(object sender, EventArgs args) => Move(2);
        public void Three(object sender, EventArgs args) => Move(3);
        public void Four(object sender, EventArgs args) => Move(4);
        public void Five(object sender, EventArgs args) => Move(5);
        public void Six(object sender, EventArgs args) => Move(6);
        public void Space(object sender, EventArgs args) => NewGame();

        private void Move(int number) 
        {
            for(int i = 0; i < Map.Length; i++)
            {
                for(int j = 0; j < Map.Length; j++)
                {
                    Used[i][j] = false;
                }
            }
            Count++;
            Algorithm(0, 0, number, Map[0][0]);           
        } 

        /// <summary>
        /// Recurve algorithm
        /// </summary>
        /// <param name="i">line position</param>
        /// <param name="j">column position</param>
        /// <param name="num">new number</param>
        /// <param name="current">current number</param>
        private void Algorithm(int i, int j, int num, int current)
        {
            Map[i][j] = num;
            Used[i][j] = true;
            if (j + 1 < Map[i].Length && Map[i][j + 1] == current && !Used[i][j + 1]) 
            {
                Algorithm(i, j + 1, num, current);
            }
            if (j - 1 >= 0 && Map[i][j - 1] == current && !Used[i][j - 1]) 
            {
                Algorithm(i, j - 1, num, current);
            }
            if (i + 1 < Map[i].Length && Map[i + 1][j] == current && !Used[i + 1][j]) 
            {
                Algorithm(i + 1, j, num, current);
            }               
            if (i - 1 >= 0 && Map[i - 1][j] == current && !Used[i - 1][j])
            {
                Algorithm(i - 1, j, num, current);
            } 
        }

        /// <summary>
        /// печать карты
        /// </summary>
        public void Print(object sender, EventArgs args)
        {
            Console.Clear();
            int h = 0;
            for(int i = 0; i < Map.Length; i++)
            {
                for(int j = 0; j < Map.Length; j++)
                {
                    if (Used[i][j])
                    {
                        h++;
                    }
                }
            }
            if (h==Map.Length*Map.Length)
            {
                Console.WriteLine($"W I N N E R for {Count - 1} turns");
                Console.WriteLine("Press SpaceBar for new game");
            }
            
            else
            {
                for (var i = 0; i < Map.Length; ++i)
                {
                    for (var j = 0; j < Map[i].Length; ++j)
                    {
                        Console.Write($"{Map[i][j]} ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
                for (var i = 0; i < Map.Length; ++i)
                {
                    for (var j = 0; j < Map[i].Length; ++j)
                    {
                        if(Used[i][j]) Console.Write($"T ");
                        else Console.Write($"_ ");                       
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(h);
                Console.WriteLine($"Turn {Count}");
            }
        }

        /// <summary>
        /// first-time printing
        /// </summary>
        public void Start()
        {
            Console.Clear();
            for (var i = 0; i < Map.Length; ++i)
            {
                for (var j = 0; j < Map[i].Length; ++j)
                {
                    Console.Write($"{Map[i][j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            for (var i = 0; i < Map.Length; ++i)
            {
                for (var j = 0; j < Map[i].Length; ++j)
                {
                    if (Used[i][j]) Console.Write($"T ");
                    else Console.Write($"_ ");
                }
                Console.WriteLine();
            }
        }
    }
}
