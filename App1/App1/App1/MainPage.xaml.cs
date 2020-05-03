using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            
        }
        IGame game;
        async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if(button.Text=="one player") game = new Game(Convert.ToInt32(Math.Round(SizeSlider.Value)));
            if(button.Text=="two players") game = new TwoPlayerGame(Convert.ToInt32(Math.Round(SizeSlider.Value)));

            //game.PrintingMap();


            Event eventLoop = new Event();

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

            await Navigation.PushModalAsync(new Board());

        }

        void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            slider.Value = Convert.ToInt32(Math.Round(SizeSlider.Value));
            header.Text = e.NewValue.ToString();
        }
    }
}
