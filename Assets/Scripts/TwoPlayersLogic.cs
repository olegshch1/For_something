using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace App1
{
    public class TwoPlayerGame : IGame
    {
        //Game Map
        public int[][] Map { get; set; }

        //Counting moves
        public int Count { get; set; }

        //Markers
        public bool[][] Used { get; set; }

        public bool Flag { get; set; }
        public int Size { get; set; }
        public int size;
        /// <summary>
        /// считывание карты
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public TwoPlayerGame(int size)
        {
            this.size = size;
            this.Size = size;
            NewGame();
        }
        public TwoPlayerGame()
        {
            this.size = 10;
            this.Size = size;
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
            if (Count % 2 == 0)
            {
                Algorithm(0, 0, number, Map[0][0]);
            }
            else
            {
                OppositeAlgorithm(Map.Length - 1, Map.Length - 1, number, Map[Map.Length - 1][Map.Length - 1]);
            }
            Check();
            TwoPlayersCheck();
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
            if (i == Map.Length - 1 && j == Map.Length - 1) 
                Flag = true;
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



        private void TwoPlayersCheckAlgorithm(int i, int j, int current)
        {
            if (i == Map.Length - 1 && j == Map.Length - 1)
                Flag = true;            
            Used[i][j] = true;
            if (j + 1 < Map[i].Length && Map[i][j + 1] == current && !Used[i][j + 1])
            {
                TwoPlayersCheckAlgorithm(i, j + 1, current);
            }
            if (j - 1 >= 0 && Map[i][j - 1] == current && !Used[i][j - 1])
            {
                TwoPlayersCheckAlgorithm(i, j - 1, current);
            }
            if (i + 1 < Map[i].Length && Map[i + 1][j] == current && !Used[i + 1][j])
            {
                TwoPlayersCheckAlgorithm(i + 1, j, current);
            }
            if (i - 1 >= 0 && Map[i - 1][j] == current && !Used[i - 1][j])
            {
                TwoPlayersCheckAlgorithm(i - 1, j, current);
            }
        }

        private void TwoPlayersCheck()
        {
            for (int i = 0; i < Map.Length; i++)
            {
                for (int j = 0; j < Map.Length; j++)
                {
                    Used[i][j] = false;
                }
            }
            TwoPlayersCheckAlgorithm(0, 0, Map[0][0]);
        }

        private void OppositeAlgorithm(int i, int j, int num, int current)
        {
            if (i == 0 && j == 0) 
                Flag = true;
            Map[i][j] = num;
            Used[i][j] = true;
            if (j + 1 < Map[i].Length && Map[i][j + 1] == current && !Used[i][j + 1])
            {
                OppositeAlgorithm(i, j + 1, num, current);
            }
            if (j - 1 >= 0 && Map[i][j - 1] == current && !Used[i][j - 1])
            {
                OppositeAlgorithm(i, j - 1, num, current);
            }
            if (i + 1 < Map[i].Length && Map[i + 1][j] == current && !Used[i + 1][j])
            {
                OppositeAlgorithm(i + 1, j, num, current);
            }
            if (i - 1 >= 0 && Map[i - 1][j] == current && !Used[i - 1][j])
            {
                OppositeAlgorithm(i - 1, j, num, current);
            }
        }
    }
}
