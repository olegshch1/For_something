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

        /// <summary>
        /// Event printing
        /// </summary>
        void Print(object sender, EventArgs args);

        void Move(int number);

        /// <summary>
        /// Change colors
        /// </summary>
        //void One(object sender, EventArgs args);
        //void Two(object sender, EventArgs args);
        //void Three(object sender, EventArgs args);
        //void Four(object sender, EventArgs args);
        //void Five(object sender, EventArgs args);
        //void Six(object sender, EventArgs args);
        //void Space(object sender, EventArgs args);
    }
}
