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
    public partial class TwoPlayerBoard : ContentPage
    {
        public TwoPlayerBoard()
        {
            InitializeComponent();
            
        }

        void OnMainContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;

            bool isLandscape = width > height;

            //if (isLandscape)
            //{
            //    mainGrid.RowDefinitions[0].Height = 0;
            //    //mainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);

            //    mainGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
            //    //mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            //}
            //else // portrait
            //{
            twomainGrid.RowDefinitions[0].Height = new GridLength(3, GridUnitType.Star);
            twomainGrid.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);

            twomainGrid.ColumnDefinitions[0].Width = 0;
            twomainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);

            //}
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
            if (button.BackgroundColor == Color.Red) twoboard.game.Move(1);
            if (button.BackgroundColor == Color.Pink) twoboard.game.Move(2);
            if (button.BackgroundColor == Color.Green) twoboard.game.Move(3);
            if (button.BackgroundColor == Color.Blue) twoboard.game.Move(4);
            if (button.BackgroundColor == Color.Yellow) twoboard.game.Move(5);
            if (button.BackgroundColor == Color.Black) twoboard.game.Move(6);
            twoboard.Check();
            int count = Convert.ToInt32(counter.Text);
            count++;
            counter.Text = count.ToString();
            if (turn.Text == "down-right player's turn") turn.Text = "up-left player's turn";
            else turn.Text = "down-right player's turn";
            if (twoboard.game.Flag) Navigation.PushModalAsync(new XamarinFlood.FinishPage());
        }
    }
}