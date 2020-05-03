using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {

            ////запуск eventloop и игры        
            //game.PrintingMap();

            //eventLoop.OneHandler += game.One;
            //eventLoop.TwoHandler += game.Two;
            //eventLoop.ThreeHandler += game.Three;
            //eventLoop.FourHandler += game.Four;
            //eventLoop.FiveHandler += game.Five;
            //eventLoop.SixHandler += game.Six;
            //eventLoop.SpaceHandler += game.Space;

            //eventLoop.OneHandler += game.Print;
            //eventLoop.TwoHandler += game.Print;
            //eventLoop.ThreeHandler += game.Print;
            //eventLoop.FourHandler += game.Print;
            //eventLoop.FiveHandler += game.Print;
            //eventLoop.SixHandler += game.Print;
            //eventLoop.SpaceHandler += game.Print;

            //eventLoop.Run();

            InitializeComponent();
            MainPage = new MainPage();            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
