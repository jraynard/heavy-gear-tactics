#region File Description
//-----------------------------------------------------------------------------
// HexTile.cs
// Justin Raynard
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Drawing;
#endregion

namespace HeavyGearMapEditor
{
    /// <summary>
    /// Class used to represent a hex tile within the game
    /// </summary>

    public enum HexType
    {
        Clear,
        Rough,
        Sand,
        Woodland,
        Jungle,
        Swamp,
        Water
    }
    
    [Serializable]
    public class HexTile
    {
        #region Variables
        private int elevation = 0;
        private HexType hexType = HexType.Clear;

        private bool highlight = false;

        private Point mapPosition;
        private Point screenPosition;
        private Point center;
        private Point[] points = new Point[6];

        #endregion

        #region Properties
        public bool Highlight
        {
            get
            {
                return highlight;
            }
            set
            {
                highlight = value;
            }
        }
        public Point Center
        {
            get
            {
                return center;
            }
        }
        public Point MapPosition
        {
            get
            {
                return mapPosition;
            }
        }
        public Point ScreenPosition
        {
            get
            {
                return screenPosition;
            }
        }
        public Point[] Points
        {
            get
            {
                return points;
            }
        }
        public int Elevation
        {
            get
            {
                return this.elevation;
            }
            set
            {
                this.elevation = value;
            }
        }

        public HexType HexType
        {
            get
            {
                return this.hexType;
            }
            set
            {
                this.hexType = value;
            }
        }
        #endregion

        #region Constructor

        public HexTile(Point mapPosition, int elevation, HexType hexType)
        {
            this.elevation = elevation;
            this.hexType = hexType;
            this.mapPosition = mapPosition;

            Init();
        }

        public HexTile(Point mapPosition)
        {
            this.mapPosition = mapPosition;
            Init();
        }

        #endregion

        #region Methods

        private void Init()
        {
            ConvertMapToPixel(ref mapPosition, out screenPosition);

            points = new Point[] {
                new Point(screenPosition.X + 37 / 2, screenPosition.Y), //Top Left
                new Point(screenPosition.X + 111 / 2, screenPosition.Y),//Top Right
                new Point(screenPosition.X + 74, screenPosition.Y + 32),//Right
                new Point(screenPosition.X + 111 / 2, screenPosition.Y + 64),//Bottom Right
                new Point(screenPosition.X + 37 / 2, screenPosition.Y + 64),//Bottom Left
                new Point(screenPosition.X, screenPosition.Y + 32)};//Left


            center.X = screenPosition.X + 74;
            center.Y = screenPosition.Y + 64;
        }

        public static void ConvertMapToPixel(ref Point mapPosition, out Point pixelPosition)
        {
            int xPos, yPos;

            xPos = mapPosition.X * 111;
            if (mapPosition.Y % 2 > 0)
            {
                xPos += 111 / 2;
            }

            yPos = mapPosition.Y * 32;

            pixelPosition = new Point(xPos, yPos);
        }

        #endregion

    }
}

