#pragma warning disable 4014        // for non-await'ed async call

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
            board.NewGameInitialize();
        }

        void OnMainContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;

            bool isLandscape = width > height;

            if (isLandscape)
            {
                mainGrid.RowDefinitions[0].Height = 0;
                mainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

                mainGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            }
            else // portrait
            {
                mainGrid.RowDefinitions[0].Height = new GridLength(3, GridUnitType.Star);
                mainGrid.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);

                mainGrid.ColumnDefinitions[0].Width = 0;
                mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            }
        }
        void OnBoardContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;
            double dimension = Math.Min(width, height);
            double horzPadding = (width - dimension) / 2;
            double vertPadding = (height - dimension) / 2;
            contentView.Padding = new Thickness(horzPadding, vertPadding);
        }


        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Content.BackgroundColor = button.BackgroundColor;
            if (button.BackgroundColor == Color.Red)  board.game.Move(1); 
            if (button.BackgroundColor == Color.Pink)  board.game.Move(2); 
            if (button.BackgroundColor == Color.Green)  board.game.Move(3); 
            if (button.BackgroundColor == Color.Blue)  board.game.Move(4); 
            if (button.BackgroundColor == Color.Yellow)  board.game.Move(5); 
            if (button.BackgroundColor == Color.Black)  board.game.Move(6);
            board.Check();
        }
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}