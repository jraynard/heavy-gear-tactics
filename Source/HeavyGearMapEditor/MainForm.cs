using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace HeavyGearMapEditor
{
    public partial class MainForm : Form
    {
        SaveFileDialog sfd = new SaveFileDialog();
        OpenFileDialog ofd = new OpenFileDialog();
        ContentBuilder contentBuilder;

        public MainForm()
        {
            //contentBuilder = new ContentBuilder();
            InitializeComponent();
        }

        private void Save()
        {
            string xmlFileName = sfd.FileName;
            string backgroundFileName = xmlFileName.Replace(".xml", ".png");
            string prevFileName = xmlFileName.Replace(".xml", "Preview.png");
            Image background = mapEditor.Background;
            Image thumbnail = background.GetThumbnailImage(100, 100, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            XmlDocument doc = mapEditor.SaveMap();

            doc.Save(xmlFileName);
            background.Save(backgroundFileName);
            thumbnail.Save(prevFileName);

            string name = Path.GetFileNameWithoutExtension(backgroundFileName);
            string path = Path.GetDirectoryName(backgroundFileName);
            // Tell the ContentBuilder what to build.
            contentBuilder = new ContentBuilder(path);
            contentBuilder.Clear();
            contentBuilder.Add(backgroundFileName, name, null, "TextureProcessor");
            contentBuilder.Add(prevFileName, name + "Preview", null, "TextureProcessor");

            // Build this new model data.
            string buildError = contentBuilder.Build();

            if (!string.IsNullOrEmpty(buildError))
            {
                // If the build failed, display an error message.
                MessageBox.Show(buildError, "Error");
            }
        }

        /// <summary>
        /// Required, but not used
        /// </summary>
        /// <returns>true</returns>
        public bool ThumbnailCallback()
        {
            return true;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            ofd.Filter = "PNG|*.png|Bitmap|*.bmp|JPEG|*.jpg";
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.Title = "Choose background image";
            DialogResult result = ofd.ShowDialog(this);

            //MemoryStream stream = ofd.OpenFile();
            //Image background = Image.FromStream(stream);
            if (result == DialogResult.OK)
            {
                Image background;

                using (FileStream f = File.Open(ofd.FileName, FileMode.Open))
                {
                    background = Image.FromStream(f);
                    f.Close();
                }

                mapEditor.NewMap(background);
                editToolStripMenuItem.Enabled = true;
                viewToolStripMenuItem.Enabled = true;
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "XML|*.xml";
            ofd.AddExtension = true;
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Multiselect = false;
            ofd.Title = "Choose XML file";
            DialogResult result = ofd.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                string xmlFileName = ofd.FileName;
                string backgroundFileName = xmlFileName.Replace(".xml", ".png");

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFileName);

                Image background;

                using (FileStream f = File.Open(backgroundFileName, FileMode.Open))
                {
                    background = Image.FromStream(f);
                    f.Close();
                }

                mapEditor.LoadMap(background, xmlDoc);

                sfd.FileName = ofd.FileName;

                editToolStripMenuItem.Enabled = true;
                viewToolStripMenuItem.Enabled = true; 
                saveAsToolStripMenuItem.Enabled = true;
                saveToolStripMenuItem.Enabled = true;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.FileName == string.Empty)
            {
                sfd.Filter = "XML|*.xml";
                sfd.AddExtension = true;
                sfd.Title = "Save map";
                DialogResult result = sfd.ShowDialog(this);

                if (result == DialogResult.OK)
                {
                    Save();
                }
            }
            else
            {
                Save();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "XML|*.xml";
            sfd.AddExtension = true;
            sfd.Title = "Save map";
            DialogResult result = sfd.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                Save();
            }
        }

        private void elevationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!elevationsToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartElevationEdit();
                clearToolStripMenuItem.Checked = false;
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!clearToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Clear);
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void jungleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!jungleToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Jungle);
                clearToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void roughToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!roughToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Rough);
                jungleToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void sandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!sandToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Sand);
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void swampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!swampToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Swamp);
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void waterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!waterToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Water);
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void woodlandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!woodlandToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartHexTypeEdit(HexType.Woodland);
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void player1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!player1ToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartPlayerStartEdit(1);
                clearToolStripMenuItem.Checked = false;
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
            }
        }

        private void player2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!player2ToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartPlayerStartEdit(2);
                player1ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
            }

        }

        private void player3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!player3ToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartPlayerStartEdit(3);
                player2ToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                player4ToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
            }
        }

        private void player4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!player4ToolStripMenuItem.Checked)
                mapEditor.EndEdit();
            else
            {
                mapEditor.StartPlayerStartEdit(4);
                player2ToolStripMenuItem.Checked = false;
                player3ToolStripMenuItem.Checked = false;
                player1ToolStripMenuItem.Checked = false;
                clearToolStripMenuItem.Checked = false;
                jungleToolStripMenuItem.Checked = false;
                roughToolStripMenuItem.Checked = false;
                sandToolStripMenuItem.Checked = false;
                swampToolStripMenuItem.Checked = false;
                waterToolStripMenuItem.Checked = false;
                woodlandToolStripMenuItem.Checked = false;
                elevationsToolStripMenuItem.Checked = false;
            }
        }

        private void elevationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.ViewElevation = !mapEditor.ViewElevation;
            mapEditor.Refresh();
        }

        private void startPositionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.ViewPlayerStarts = !mapEditor.ViewPlayerStarts;
            mapEditor.Refresh();
        }

        private void hexGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.ViewHexGrid = !mapEditor.ViewHexGrid;
            mapEditor.Refresh();
        }

        private void viewHexTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapEditor.ViewHexType = !mapEditor.ViewHexType;
            mapEditor.Refresh();
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextDialog nameDialog = new TextDialog(mapEditor, TextDialogType.Name);
            nameDialog.Show();
        }

        private void descriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextDialog descDialog = new TextDialog(mapEditor, TextDialogType.Description);
            descDialog.Show();
        }

        private void threatValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextDialog tvDialog = new TextDialog(mapEditor, TextDialogType.ThreatValue);
            tvDialog.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}