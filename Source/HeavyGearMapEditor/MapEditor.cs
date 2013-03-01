using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace HeavyGearMapEditor
{
    public partial class MapEditor : UserControl
    {
        #region Constants

        private const int TileWidth = 74;
        private const int TileHeight = 64;
        private const int TileSide = 37;

        private static Point TileScale = new Point(111, 32);
        private static int TileOffset = (int)Math.Round(111 / 2.0f);

        private static Pen normalPen = new Pen(Color.Black, 2.0f);
        private static Pen highlightPen = new Pen(Color.Blue, 2.0f);

        private static Font font = new Font("Arial", 12);

        #endregion

        #region Variables

        private Map map;
        private bool elevationMode;
        private bool hexTypeMode;
        private HexType hexTypeBrush;
        private bool playerStartMode;
        private int playerToPlace;

        private bool viewHexType = true;
        private bool viewPlayerStarts = true;
        private bool viewElevation = true;
        private bool viewHexGrid = true;

        #endregion

        public bool ViewHexType
        {
            get
            {
                return viewHexType;
            }
            set
            {
                viewHexType = value;
            }
        }
        public bool ViewPlayerStarts
        {
            get
            {
                return viewPlayerStarts;
            }
            set
            {
                viewPlayerStarts = value;
            }
        }

        public bool ViewElevation
        {
            get
            {
                return viewElevation;
            }
            set
            {
                viewElevation = value;
            }
        }

        public bool ViewHexGrid
        {
            get
            {
                return viewHexGrid;
            }
            set
            {
                viewHexGrid = value;
            }
        }

        public Image Background
        {
            get
            {
                return pictureBox1.Image;
            }
        }

        public Map Map
        {
            get
            {
                return map;
            }
        }

        public void SetName(string name)
        {
            map.Name = name;
        }

        public void SetDescription(string desc)
        {
            map.Description = desc;
        }

        public void SetThreatValue(int threatValue)
        {
            map.ThreatValue = threatValue;
        }

        public MapEditor()
        {
            InitializeComponent();
            pictureBox1.Image = null;
        }

        public void StartHexTypeEdit(HexType newHexType)
        {
            hexTypeBrush = newHexType;
            hexTypeMode = true;
            elevationMode = false;
            playerStartMode = false;
        }

        public void StartElevationEdit()
        {
            hexTypeMode = false;
            elevationMode = true;
            playerStartMode = false;
        }

        public void StartPlayerStartEdit(int player)
        {
            playerToPlace = player;
            hexTypeMode = false;
            elevationMode = false;
            playerStartMode = true;
        }

        public void EndEdit()
        {
            hexTypeMode = false;
            elevationMode = false;
            playerStartMode = false;
        }

        #region Save/Load/New Map

        public void NewMap(Image image)
        {
            map = new Map(image.Width, image.Height);
            pictureBox1.Image = image;
        }

        public void LoadMap(Image image, XmlDocument xmlDoc)
        {
            map = new Map(xmlDoc);
            pictureBox1.Image = image;
        }

        public XmlDocument SaveMap()
        {
            return map.Save();
        }

        #endregion

        #region Draw
        /// <summary>
        /// Draws the hexagon overlay over the background image
        /// </summary>
        /// <param name="g"></param>
        public void Draw(Graphics g)
        {

            for (int x = 0; x < map.Width; x++)
            {
                for (int y = 0; y < map.Height; y++)
                {
                    if (map.GetTile(x, y) != null)
                    {
                        //Get the current tile from the grid
                        HexTile currentTile = map.GetTile(x, y);

                        Point screenPosition = currentTile.ScreenPosition;

                        Point[] points = currentTile.Points;

                        if (viewHexGrid)
                        {
                            //Renders either the highlighted or the normal tile
                            if (currentTile.Highlight)
                                g.DrawPolygon(highlightPen, points);
                            else
                                g.DrawPolygon(normalPen, points);
                        }

                        PointF textPosition = new PointF(currentTile.Center.X, currentTile.Center.Y);
                        PointF elevPoint = new PointF(textPosition.X - 10, textPosition.Y);
                        PointF startPoint = new PointF(textPosition.X - 40, textPosition.Y + 20);
                        PointF hexTypePoint = new PointF(textPosition.X - 40, textPosition.Y - 20);

                        if (viewElevation)
                            g.DrawString(currentTile.Elevation.ToString(), font, Brushes.Black, elevPoint);

                        if (viewPlayerStarts)
                        {
                            //draw the start positions
                            if (map.Player1Start.X == x && map.Player1Start.Y == y)
                            {
                                g.DrawString("Player 1 Start", font, Brushes.Black, startPoint);
                            }
                            else if (map.Player2Start.X == x && map.Player2Start.Y == y)
                            {
                                g.DrawString("Player 2 Start", font, Brushes.Black, startPoint);
                            }
                            else if (map.Player3Start.X == x && map.Player3Start.Y == y)
                            {
                                g.DrawString("Player 3 Start", font, Brushes.Black, startPoint);
                            }
                            else if (map.Player4Start.X == x && map.Player4Start.Y == y)
                            {
                                g.DrawString("Player 4 Start", font, Brushes.Black, startPoint);
                            }
                        }

                        if (viewHexType)
                        {
                            string hexType = "";
                            switch (currentTile.HexType)
                            {
                                case HexType.Clear:
                                    hexType = "Clear";
                                    break;
                                case HexType.Jungle:
                                    hexType = "Jungle";
                                    break;
                                case HexType.Rough:
                                    hexType = "Rough";
                                    break;
                                case HexType.Sand:
                                    hexType = "Sand";
                                    break;
                                case HexType.Swamp:
                                    hexType = "Swamp";
                                    break;
                                case HexType.Water:
                                    hexType = "Water";
                                    break;
                                case HexType.Woodland:
                                    hexType = "Woodland";
                                    break;
                            }

                            g.DrawString(hexType, font, Brushes.Black, hexTypePoint);
                        }
                    }
                }
            }
        }
        #endregion

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
                Draw(e.Graphics);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (elevationMode || playerStartMode || hexTypeMode)
            {
                Point mousePos = e.Location;
                Point mapPos;
                //mousePos.X += mainPanel.AutoScrollPosition.X;
                //mousePos.Y -= mainPanel.AutoScrollPosition.Y;
                ConvertPixelToMap(ref mousePos, out mapPos);
                HexTile tile = map.GetTile(mapPos.X, mapPos.Y);

                if (elevationMode)
                {
                    if (e.Button == MouseButtons.Left || e.Delta > 0)
                    {
                        if (tile.Elevation < 5)
                        {
                            tile.Elevation++;
                            pictureBox1.Refresh();
                        }
                    }
                    else if (e.Button == MouseButtons.Right || e.Delta < 0)
                    {
                        if (tile.Elevation > 0)
                        {
                            tile.Elevation--;
                            pictureBox1.Refresh();
                        }
                    }
                }

                if (hexTypeMode)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        tile.HexType = hexTypeBrush;
                        pictureBox1.Refresh();
                    }
                }

                if (playerStartMode)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        switch (playerToPlace)
                        {
                            case 1:
                                map.Player1Start = mapPos;
                                break;
                            case 2:
                                map.Player2Start = mapPos;
                                break;
                            case 3:
                                map.Player3Start = mapPos;
                                break;
                            case 4:
                                map.Player4Start = mapPos;
                                break;
                        }
                    }
                    pictureBox1.Refresh();
                }
            }
        }

        public void ConvertPixelToMap(ref Point pixelPosition, out Point mapPosition)
        {
            int xPos, yPos;

            yPos = (int)(pixelPosition.Y / TileScale.Y);

            if ((int)pixelPosition.X / TileOffset % 2 == 0)
            {
                //aprrox. even column
                if ((int)pixelPosition.Y % TileHeight >= TileScale.Y)
                {
                    //bottom part of the tile, so subtract one
                    yPos--;
                }
            }
            else
            {
                if (((int)pixelPosition.Y - TileScale.Y) % TileHeight >= TileScale.Y)
                {
                    //bottom part of the tile, so subtract one
                    yPos--;
                }
            }


            xPos = (int)pixelPosition.X / TileScale.X;

            //constrain to the limits of the grid
            if (yPos < 0)
                yPos = 0;

            if (xPos < 0)
                xPos = 0;

            if (yPos > (map.Height - 1))
                yPos = map.Height - 1;

            if (xPos > (map.Width - 1))
                xPos = map.Width - 1;

            mapPosition = new Point(xPos, yPos);
        }
    }
}
