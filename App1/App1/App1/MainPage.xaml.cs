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
            //if(button.Text=="one player") game = new Game();
            //if(button.Text=="two players") game = new TwoPlayerGame();
            await Navigation.PushModalAsync(new Board());

        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            slider.Value = Math.Round(slider.Value);
            header.Text = String.Format("{0:f1}", e.NewValue);
        }
    }
}
