using System;

namespace App1
{
    public class Game : IGame
    {
        //Game Map
        public int[][] Map { get; set; }

        //Counting moves
        public int Count { get; set; }

        //Markers
        public bool[][] Used { get; set; }

        public bool Flag { get; set; }

        public int Size { get; set; } = 10;
        int size;
        /// <summary>
        /// считывание карты
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public Game()
        {
            size = Size;
            NewGame();
        }

        public void NewGame()
        {
            Flag = false;
            Count = 0;
            //Console.Clear();
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

            Check();

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
        //public void One(object sender, EventArgs args) => Move(1);
        //public void Two(object sender, EventArgs args) => Move(2);
        //public void Three(object sender, EventArgs args) => Move(3);
        //public void Four(object sender, EventArgs args) => Move(4);
        //public void Five(object sender, EventArgs args) => Move(5);
        //public void Six(object sender, EventArgs args) => Move(6);
        //public void Space(object sender, EventArgs args) => NewGame();

        /// <summary>
        /// Refilling map
        /// </summary>
        /// <param name="number"></param>
        public void Move(int number)
        {
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map.Length; j++)
                {
                    Used[i][j] = false;
                }
            }
            Count++;
            Algorithm(0, 0, number, Map[0][0]);
            Check();
        }

        /// <summary>
        /// Check if won
        /// </summary>
        private void Check()
        {
            int h = 0;
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map.Length; j++)
                {
                    if (Map[i][j] == Map[0][0])
                    {
                        h++;
                    }
                }
            }
            if (h == Map.Length * Map.Length)
            {
                Flag = true;
            }
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
        /// Event printing
        /// </summary>
        public void Print(object sender, EventArgs args) => PrintingMap();

        /// <summary>
        /// Print method
        /// </summary>
        public void PrintingMap()
        {
            //Console.Clear();
            //if (Flag)
            //{
            //    Console.WriteLine($"W I N N E R for {Count} turns");
            //    Console.WriteLine("Press SpaceBar or Enter for new game");
            //}

            //else
            //{
            //    for (var i = 0; i < Map.Length; ++i)
            //    {
            //        for (var j = 0; j < Map[i].Length; ++j)
            //        {
            //            Console.Write($"{Map[i][j]} ");
            //        }
            //        Console.WriteLine();
            //    }
            //    Console.WriteLine();
            //    //for (var i = 0; i < Map.Length; ++i)
            //    //{
            //    //    for (var j = 0; j < Map[i].Length; ++j)
            //    //    {
            //    //        if(Used[i][j]) Console.Write($"T ");
            //    //        else Console.Write($"_ ");                       
            //    //    }
            //    //    Console.WriteLine();
            //    //}
            //    Console.WriteLine($"Turn {Count}");
            //}
        }
    }
}
