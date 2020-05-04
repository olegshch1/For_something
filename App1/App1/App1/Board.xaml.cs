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
        IGame game;
        public Board(IGame game)
        {
            this.game = game;
            Grid grid = new Grid();
            for (int i = 0; i < game.Size; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int i = 0; i < game.Size; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < game.Size; i++)
            {
                for (int j = 0; j < game.Size; j++)
                {
                    BoxView cell = new BoxView();
                    grid.Children.Add(cell, i, j);
                }
            }
            InitializeComponent();
            
            //Event eventLoop = new Event();

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
        }

        private void ColorButton_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            Content.BackgroundColor = button.BackgroundColor;
            if (button.BackgroundColor == Color.Red)  game.Move(1); 
            if (button.BackgroundColor == Color.Pink)  game.Move(2); 
            if (button.BackgroundColor == Color.Green)  game.Move(3); 
            if (button.BackgroundColor == Color.Blue)  game.Move(4); 
            if (button.BackgroundColor == Color.Yellow)  game.Move(5); 
            if (button.BackgroundColor == Color.Pink)  game.Move(6); 
        }
        async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}