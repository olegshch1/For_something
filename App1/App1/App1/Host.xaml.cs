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
    public partial class Host : ContentPage
    {
        App1.IGame game;
        public Host()
        {
            InitializeComponent();
            this.game = game;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            if (button.Text == "to be host")
            {
            }

            if (button.Text == "search for a game")
            {
            }
        }
    }
}