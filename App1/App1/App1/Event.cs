using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public class Event
    {
        public event EventHandler<EventArgs> OneHandler = (sender, args) => { };
        public event EventHandler<EventArgs> TwoHandler = (sender, args) => { };
        public event EventHandler<EventArgs> ThreeHandler = (sender, args) => { };
        public event EventHandler<EventArgs> FourHandler = (sender, args) => { };
        public event EventHandler<EventArgs> FiveHandler = (sender, args) => { };
        public event EventHandler<EventArgs> SixHandler = (sender, args) => { };
        public event EventHandler<EventArgs> SpaceHandler = (sender, args) => { };

        /// <summary>
        /// Начало считывания команд
        /// </summary>
        public void Run()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.NumPad1:
                        OneHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D1:
                        OneHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.NumPad2:
                        TwoHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D2:
                        TwoHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.NumPad3:
                        ThreeHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D3:
                        ThreeHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.NumPad4:
                        FourHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D4:
                        FourHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.NumPad5:
                        FiveHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D5:
                        FiveHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.NumPad6:
                        SixHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.D6:
                        SixHandler(this, EventArgs.Empty);
                        break;

                    case ConsoleKey.Spacebar:
                        SpaceHandler(this, EventArgs.Empty);
                        break;
                    case ConsoleKey.Enter:
                        SpaceHandler(this, EventArgs.Empty);
                        break;
                }
            }
        }
    }
}
