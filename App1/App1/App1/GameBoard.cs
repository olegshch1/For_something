using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    class GameBoard : AbsoluteLayout
    {
        // Alternative sizes make the tiles a tad small.
        const int COLS = 9;         // 16
        const int ROWS = 9;         // 16
        const int BUGS = 10;        // 40

        Tile[,] tiles = new Tile[ROWS, COLS];
        int flaggedTileCount;
        bool isGameInProgress;              // on first tap
        bool isGameInitialized;             // on first double-tap
        bool isGameEnded;

        // Events to notify page.
        public event EventHandler GameStarted;
        public event EventHandler<bool> GameEnded;

        public GameBoard()
        {
            for (int row = 0; row < ROWS; row++)
                for (int col = 0; col < COLS; col++)
                {
                    Tile tile = new Tile(row, col);
                    tile.TileStatusChanged += OnTileStatusChanged;
                    this.Children.Add(tile);
                    tiles[row, col] = tile;
                }

            SizeChanged += (sender, args) =>
            {
                double tileWidth = this.Width / COLS;
                double tileHeight = this.Height / ROWS;

                foreach (Tile tile in tiles)
                {
                    Rectangle bounds = new Rectangle(tile.Col * tileWidth,
                                                     tile.Row * tileHeight,
                                                     tileWidth, tileHeight);
                    AbsoluteLayout.SetLayoutBounds(tile, bounds);
                }
            };

            NewGameInitialize();
        }

        public void NewGameInitialize()
        {
            // Clear all the tiles.
            foreach (Tile tile in tiles)
                tile.Initialize();

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

            foreach (Tile til in tiles)
            {
                if (til.IsBug && til.Status != TileStatus.Flagged)
                    hasWon = false;

                if (!til.IsBug && til.Status != TileStatus.Exposed)
                    hasWon = false;
            }

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

