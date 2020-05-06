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
            if (button.Text == "one player")
            {
                //game = new Game(Convert.ToInt32(Math.Round(SizeSlider.Value)));
                //game = new Game();
                await Navigation.PushModalAsync(new Board());
            }

            if (button.Text == "two players")
            {
                //game = new TwoPlayerGame(Convert.ToInt32(Math.Round(SizeSlider.Value)));
                //game = new TwoPlayerGame();
                //await Navigation.PushModalAsync(new XamarinFlood.Host(game));
                await Navigation.PushModalAsync(new XamarinFlood.TwoPlayerBoard());
            }           

        }

        //void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        //{
        //    var slider = (Slider)sender;
        //    slider.Value = Convert.ToInt32(Math.Round(SizeSlider.Value));
        //    header.Text = e.NewValue.ToString();
        //}
    }
}
