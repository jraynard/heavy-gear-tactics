#region File Description
//-----------------------------------------------------------------------------
// Map.cs
// Justin Raynard
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
#endregion

namespace HeavyGearMapEditor
{
    public enum MissionType
    {
        CaptureAndHold,
        Assault,
        Skirmish
    }
    public enum ObjectiveType
    {
        Location,
        Unit
    }
    /// <summary>
    /// Represents a hex grid where units will move
    /// </summary>
    public class Map
    {
        #region Variables

        private HexTile[][] grid;
        private int width;
        private int height;

        private string name;
        private string description;

        private Point player1Start;
        private Point player2Start;
        private Point player3Start;
        private Point player4Start;

        private int threatValue;
        
        #endregion

        #region Properties

        public int ThreatValue
        {
            get
            {
                return threatValue;
            }
            set
            {
                threatValue = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }

        public Point Player1Start
        {
            get
            {
                return player1Start;
            }
            set
            {
                player1Start = value;
            }
        }

        public Point Player2Start
        {
            get
            {
                return player2Start;
            }
            set
            {
                player2Start = value;
            }
        }

        public Point Player3Start
        {
            get
            {
                return player3Start;
            }
            set
            {
                player3Start = value;
            }
        }

        public Point Player4Start
        {
            get
            {
                return player4Start;
            }
            set
            {
                player4Start = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for a hex grid
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="gridFileName">The name of the xml file containing the grid of HexTiles to be used</param>
        public Map(XmlDocument doc)
        {
            Load(doc);
        }

        public Map(int backgroundWidth, int backgroundHeight)
        {
            New(backgroundWidth, backgroundHeight);
        }

        #endregion

        #region Load
        public void Load(XmlDocument doc)
        {
            XmlNodeList nameNode = doc.GetElementsByTagName("Name");
            name = nameNode[0].InnerXml;

            XmlNodeList descriptionNode = doc.GetElementsByTagName("Description");
            description = descriptionNode[0].InnerXml;

            XmlNodeList threatValueNode = doc.GetElementsByTagName("ThreatValue");
            threatValue = Convert.ToInt32(threatValueNode[0].InnerXml);

            XmlNodeList rows = doc.GetElementsByTagName("Row");
            XmlNodeList player1StartNode = doc.GetElementsByTagName("Player1Start");
            XmlNodeList player2StartNode = doc.GetElementsByTagName("Player2Start");
            XmlNodeList player3StartNode = doc.GetElementsByTagName("Player3Start");
            XmlNodeList player4StartNode = doc.GetElementsByTagName("Player4Start");

            player1Start.X = Convert.ToInt32(player1StartNode[0].ChildNodes[0].InnerXml);
            player1Start.Y = Convert.ToInt32(player1StartNode[0].ChildNodes[1].InnerXml);

            player2Start.X = Convert.ToInt32(player2StartNode[0].ChildNodes[0].InnerXml);
            player2Start.Y = Convert.ToInt32(player2StartNode[0].ChildNodes[1].InnerXml);

            player3Start.X = Convert.ToInt32(player3StartNode[0].ChildNodes[0].InnerXml);
            player3Start.Y = Convert.ToInt32(player3StartNode[0].ChildNodes[1].InnerXml);

            player4Start.X = Convert.ToInt32(player4StartNode[0].ChildNodes[0].InnerXml);
            player4Start.Y = Convert.ToInt32(player4StartNode[0].ChildNodes[1].InnerXml);

            width = rows.Count;
            height = rows[0].ChildNodes.Count;

            grid = new HexTile[width][];
            for (int i = 0; i < width; i++)
            {
                grid[i] = new HexTile[height];
                XmlNode row = rows[i];
                for (int j = 0; j < height; j++)
                {
                    XmlNode tileNode = row.ChildNodes[j];
                    grid[i][j] = new HexTile(new Point(i, j),
                                            Convert.ToInt32(tileNode.ChildNodes[0].InnerXml),
                                            (HexType)Convert.ToInt32(tileNode.ChildNodes[1].InnerXml));
                }
            }
        }
        #endregion

        #region Save
        /// <summary>
        /// Saves the map as an xml file, used with editor
        /// </summary>
        /// <param name="fileName"></param>
        public XmlDocument Save()
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();
            sb.Append("<Grid>");

            sb.Append("\t<Name>" + name + "</Name>");

            sb.Append("\t<Description>" + description + "</Description>");

            sb.Append("\t<ThreatValue>" + threatValue + "</ThreatValue>");

            for (int i = 0; i < width; i++)
            {
                sb.Append("\t<Row>");
                for (int j = 0; j < height; j++)
                {
                    HexTile tile = GetTile(i, j);
                    sb.Append("\t\t<HexTile>");
                    sb.Append("\t\t\t<Elevation>" + tile.Elevation + "</Elevation>");
                    sb.Append("\t\t\t<HexType>" + (int)tile.HexType + "</HexType>");
                    sb.Append("\t\t</HexTile>");
                }
                sb.Append("\t</Row>");
            }

            sb.Append("\t<Player1Start>");
            sb.Append("\t\t<X>" + (int)player1Start.X + "</X>");
            sb.Append("\t\t<Y>" + (int)player1Start.Y + "</Y>");
            sb.Append("\t</Player1Start>");

            sb.Append("\t<Player2Start>");
            sb.Append("\t\t<X>" + (int)player2Start.X + "</X>");
            sb.Append("\t\t<Y>" + (int)player2Start.Y + "</Y>");
            sb.Append("\t</Player2Start>");

            sb.Append("\t<Player3Start>");
            sb.Append("\t\t<X>" + (int)player3Start.X + "</X>");
            sb.Append("\t\t<Y>" + (int)player3Start.Y + "</Y>");
            sb.Append("\t</Player3Start>");

            sb.Append("\t<Player4Start>");
            sb.Append("\t\t<X>" + (int)player4Start.X + "</X>");
            sb.Append("\t\t<Y>" + (int)player4Start.Y + "</Y>");
            sb.Append("\t</Player4Start>");

            sb.Append("</Grid>");

            xmlDoc.LoadXml(sb.ToString());

            return xmlDoc;
        }
        #endregion

        #region New
        /// <summary>
        /// Creates a new tile grid defaulting to a 148x128 hex size
        /// </summary>
        public void New(int backgroundWidth, int backgroundHeight)
        {
            width = backgroundWidth / 111;
            height = backgroundHeight / 32 - 1;

            grid = new HexTile[width][];
            for (int i = 0; i < width; i++)
            {
                grid[i] = new HexTile[height];
                for (int j = 0; j < height; j++)
                {
                    grid[i][j] = new HexTile(new Point(i, j));
                }
            }
        }
        #endregion

        #region Methods
        public void SetTile(int x, int y, HexTile tile)
        {
            if (x < 0 || x > width - 1 || y < 0 || y > height - 1)
                throw new ArgumentException("Invalid x, y");
            grid[x][y] = tile;
        }
        public HexTile GetTile(int x, int y)
        {
            if (x < 0 || x > width - 1 || y < 0 || y > height - 1)
                return null;
            else
                return grid[x][y];
        }
        #endregion
    }
}