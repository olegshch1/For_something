using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    class GameBoard : AbsoluteLayout
    {
        // Alternative sizes make the tiles a tad small.
        List<List<Tile>> tiles = new List<List<Tile>>();
        bool isGameInProgress;              // on first tap
        bool isGameInitialized;             // on first double-tap
        bool isGameEnded;

        // Events to notify page.
        public event EventHandler GameStarted;
        public event EventHandler<bool> GameEnded;
        IGame game;
        public int Size { get; set; } = 5;
        public GameBoard()
        {
            for (int row = 0; row < Size; row++)
            {
                tiles.Add(new List<Tile>());
                for (int col = 0; col < Size; col++)
                {
                    Tile tile = new Tile(row, col);
                    if ((col + row) % 2 == 0)
                    {
                        tile.label.TextColor = Color.Black;
                        tile.label.BackgroundColor = Color.Black;
                        tile.BackgroundColor = Color.Black;
                        tile.BorderColor = Color.Black;
                    }
                    tile.TileStatusChanged += OnTileStatusChanged;
                    this.Children.Add(tile);
                    tiles[row].Add(tile);
                }
            }

            SizeChanged += (sender, args) =>
            {
                double tileWidth = this.Width / Size;
                double tileHeight = this.Height / Size;

                foreach (List<Tile> list in tiles) {
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

