#define FIX_UWP_DOUBLE_TAPS   // Double-taps don't work well on UWP as of 2.3.0
#define FIX_UWP_NULL_CONTENT  // Set Content of Frame to null doesn't work in UWP as of 2.3.0

using System;
using System.Reflection;
using Xamarin.Forms;

namespace App1
{
    enum TileStatus
    {
        Hidden,
        Flagged,
        Exposed
    }

    class Tile : Frame
    {
        TileStatus tileStatus = TileStatus.Hidden;
        public Label label;
        bool doNotFireEvent;

        public event EventHandler<TileStatus> TileStatusChanged;


        public Tile(int row, int col)
        {
            this.Row = row;
            this.Col = col;

            this.BackgroundColor = Color.Black;
            this.BorderColor = Color.Yellow;
            this.Padding = 0;

            label = new Label
            {
                Text = " ",
                TextColor = Color.Yellow,
                BackgroundColor = Color.Yellow,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };


        }

        public int Row { private set; get; }

        public int Col { private set; get; }

        public bool IsBug { set; get; }

        //public int SurroundingBugCount { set; get; }

        public TileStatus Status
        {
            set
            {
                if (tileStatus != value)
                {
                    tileStatus = value;

                    switch (tileStatus)
                    {
                        case TileStatus.Hidden:                            
                            break;

                        case TileStatus.Flagged:
                            break;

                        case TileStatus.Exposed:
                            if (this.IsBug) { }
                            else
                            {
                                this.Content = label;
                                label.Text = " ";
                            }
                            break;
                    }

                    if (!doNotFireEvent && TileStatusChanged != null)
                    {
                        TileStatusChanged(this, tileStatus);
                    }
                }
            }
            get
            {
                return tileStatus;
            }
        }

        // Does not fire TileStatusChanged events.
        public void Initialize()
        {
            doNotFireEvent = true;
            this.Status = TileStatus.Hidden;
            this.IsBug = false;
            doNotFireEvent = false;
        }        

        void OnDoubleTap(object sender, object args)
        {
            this.Status = TileStatus.Exposed;
        }
    }
}
