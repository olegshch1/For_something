using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1
{
    public interface IGame
    {
        //Game Map
        int[][] Map { get; set; }

        //Counting moves
        int Count { get; set; }

        int Size { get; set; }
        //Markers
        bool[][] Used { get; set; }

        bool Flag { get; set; }

        void Move(int number);
    }
}
