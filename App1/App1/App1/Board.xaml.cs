using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Board : ContentPage
    {
        public Board()
        {
            InitializeComponent();
        }

        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var parent = button.Parent.Parent;
            Content.BackgroundColor = button.BackgroundColor;            
        }

        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}