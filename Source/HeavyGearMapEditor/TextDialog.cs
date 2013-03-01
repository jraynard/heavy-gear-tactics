using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HeavyGearMapEditor
{
    public enum TextDialogType
    {
        Name,
        Description,
        ThreatValue
    }

    public partial class TextDialog : Form
    {
        MapEditor editor;
        TextDialogType type;

        public TextDialog(MapEditor editor, TextDialogType type)
        {
            this.editor = editor;
            this.type = type;
            InitializeComponent();
            if (type == TextDialogType.Name)
            {
                label1.Text = "Enter map name : ";
                textbox1.Text = editor.Map.Name;
            }
            else if (type == TextDialogType.Description)
            {
                label1.Text = "Enter map description : ";
                textbox1.Text = editor.Map.Description;
            }
            else
            {
                label1.Text = "Enter map threat value : ";
                textbox1.Text = editor.Map.ThreatValue.ToString();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.textbox1.Text != string.Empty)
            {
                if (type == TextDialogType.Name)
                    editor.SetName(this.textbox1.Text);
                else if (type == TextDialogType.Description)
                    editor.SetDescription(this.textbox1.Text);
                else
                {
                    int threatValue = 0;
                    try
                    {
                        threatValue = Convert.ToInt16(textbox1.Text);
                    }
                    catch (Exception ex)
                    {
                        this.Close();
                    }
                    editor.SetThreatValue(threatValue);
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}