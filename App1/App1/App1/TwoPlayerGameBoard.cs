using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xamarin.Forms;
using App1;
namespace XamarinFlood
{
    class TwoPlayerGameBoard : AbsoluteLayout
    {
        // Alternative sizes make the tiles a tad small.
        public List<List<Tile>> tiles = new List<List<Tile>>();
        bool isGameInProgress;              // on first tap
        bool isGameInitialized;             // on first double-tap
        bool isGameEnded;

        // Events to notify page.
        public event EventHandler GameStarted;
        public event EventHandler<bool> GameEnded;
        public App1.IGame game;
        public int Size { get; set; } = 10;
        public TwoPlayerGameBoard()
        {
            game = new App1.TwoPlayerGame();
            for (int row = 0; row < Size; row++)
            {
                tiles.Add(new List<App1.Tile>());
                for (int col = 0; col < Size; col++)
                {
                    App1.Tile tile = new App1.Tile(row, col);
                    tile.TileStatusChanged += OnTileStatusChanged;
                    this.Children.Add(tile);
                    tiles[row].Add(tile);
                }
            }
            Check();

            SizeChanged += (sender, args) =>
            {
                double tileWidth = this.Width / Size;
                double tileHeight = this.Height / Size;

                foreach (List<Tile> list in tiles)
                {
                    foreach (Tile tile in list)
                    {
                        Rectangle bounds = new Rectangle(tile.Col * tileWidth,
                                                         tile.Row * tileHeight,
                                                         tileWidth, tileHeight);
                        AbsoluteLayout.SetLayoutBounds(tile, bounds);
                    }
                }
            };

            NewGameInitialize();
        }

        public void Check()
        {
            foreach (List<Tile> list in tiles)
            {
                foreach (Tile tile in list)
                {
                    switch (game.Map[tile.Row][tile.Col])
                    {
                        case (1):
                            tile.label.TextColor = Color.Red;
                            tile.label.BackgroundColor = Color.Red;
                            tile.BackgroundColor = Color.Red;
                            tile.BorderColor = Color.Red;
                            break;
                        case (2):
                            tile.label.TextColor = Color.Pink;
                            tile.label.BackgroundColor = Color.Pink;
                            tile.BackgroundColor = Color.Pink;
                            tile.BorderColor = Color.Pink;
                            break;
                        case (3):
                            tile.label.TextColor = Color.Green;
                            tile.label.BackgroundColor = Color.Green;
                            tile.BackgroundColor = Color.Green;
                            tile.BorderColor = Color.Green;
                            break;
                        case (4):
                            tile.label.TextColor = Color.Blue;
                            tile.label.BackgroundColor = Color.Blue;
                            tile.BackgroundColor = Color.Blue;
                            tile.BorderColor = Color.Blue;
                            break;
                        case (5):
                            tile.label.TextColor = Color.Yellow;
                            tile.label.BackgroundColor = Color.Yellow;
                            tile.BackgroundColor = Color.Yellow;
                            tile.BorderColor = Color.Yellow;
                            break;
                        case (6):
                            tile.label.TextColor = Color.Black;
                            tile.label.BackgroundColor = Color.Black;
                            tile.BackgroundColor = Color.Black;
                            tile.BorderColor = Color.Black;
                            break;
                    }

                }
            }
        }

        public void NewGameInitialize()
        {
            // Clear all the tiles.
            foreach (List<Tile> list in tiles)
            {
                foreach (Tile tile in list)
                    tile.Initialize();
            }

            isGameInProgress = false;
            isGameInitialized = false;
            isGameEnded = false;
        }

        void OnTileStatusChanged(object sender, TileStatus tileStatus)
        {
            if (isGameEnded)
                return;

            // With a first tile tapped, the game is now in progress.
            if (!isGameInProgress)
            {
                isGameInProgress = true;

                // Fire the GameStarted event.
                if (GameStarted != null)
                {
                    GameStarted(this, EventArgs.Empty);
                }
            }

            // Get the tile whose status has changed.
            Tile changedTile = (Tile)sender;

            // If it's exposed, some actions are required.
            if (tileStatus == TileStatus.Exposed)
            {
                if (!isGameInitialized)
                {
                    isGameInitialized = true;
                }

                if (changedTile.IsBug)
                {
                    isGameInProgress = false;
                    isGameEnded = true;

                    // Fire the GameEnded event!
                    if (GameEnded != null)
                    {
                        GameEnded(this, false);
                    }
                    return;
                }
            }

            // Check for a win.
            bool hasWon = true;
            // If there's a win, celebrate!
            if (hasWon)
            {
                isGameInProgress = false;
                isGameEnded = true;

                // Fire the GameEnded event!
                if (GameEnded != null)
                {
                    GameEnded(this, true);
                }
                return;
            }
        }
    }
}

