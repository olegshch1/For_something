using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFlood
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChooseMultiplayer : ContentPage
    {
        public ChooseMultiplayer()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Hot-seat game")
            {
                //game = new Game(Convert.ToInt32(Math.Round(SizeSlider.Value)));
                //game = new Game();
                await Navigation.PushModalAsync(new TwoPlayerBoard());
            }

            if (button.Text == "Local multiplayer")
            {
                //game = new TwoPlayerGame(Convert.ToInt32(Math.Round(SizeSlider.Value)));
                //game = new TwoPlayerGame();
                //await Navigation.PushModalAsync(new XamarinFlood.Host(game));
                await Navigation.PushModalAsync(new Host());
            }
        }
    }
}