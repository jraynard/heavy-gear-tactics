namespace HeavyGearMapEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.elevationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hexTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jungleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.roughToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.swampToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.waterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.woodlandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.player4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descriptionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threatValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.missionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.destroyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skirmishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewElevationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewStartPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHexTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHexGridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapEditor = new HeavyGearMapEditor.MapEditor();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.missionToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(640, 24);
            this.mainMenuStrip.TabIndex = 0;
            this.mainMenuStrip.Text = "Main Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Enabled = false;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Enabled = false;
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveAsToolStripMenuItem.Text = "Save as...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.elevationsToolStripMenuItem,
            this.hexTypeToolStripMenuItem,
            this.placeStartToolStripMenuItem,
            this.nameToolStripMenuItem,
            this.descriptionToolStripMenuItem,
            this.threatValueToolStripMenuItem});
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // elevationsToolStripMenuItem
            // 
            this.elevationsToolStripMenuItem.CheckOnClick = true;
            this.elevationsToolStripMenuItem.Name = "elevationsToolStripMenuItem";
            this.elevationsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.elevationsToolStripMenuItem.Text = "Elevation";
            this.elevationsToolStripMenuItem.Click += new System.EventHandler(this.elevationsToolStripMenuItem_Click);
            // 
            // hexTypeToolStripMenuItem
            // 
            this.hexTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearToolStripMenuItem,
            this.jungleToolStripMenuItem,
            this.roughToolStripMenuItem,
            this.sandToolStripMenuItem,
            this.swampToolStripMenuItem,
            this.waterToolStripMenuItem,
            this.woodlandToolStripMenuItem});
            this.hexTypeToolStripMenuItem.Name = "hexTypeToolStripMenuItem";
            this.hexTypeToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.hexTypeToolStripMenuItem.Text = "Hex Type";
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.CheckOnClick = true;
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // jungleToolStripMenuItem
            // 
            this.jungleToolStripMenuItem.CheckOnClick = true;
            this.jungleToolStripMenuItem.Name = "jungleToolStripMenuItem";
            this.jungleToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.jungleToolStripMenuItem.Text = "Jungle";
            this.jungleToolStripMenuItem.Click += new System.EventHandler(this.jungleToolStripMenuItem_Click);
            // 
            // roughToolStripMenuItem
            // 
            this.roughToolStripMenuItem.CheckOnClick = true;
            this.roughToolStripMenuItem.Name = "roughToolStripMenuItem";
            this.roughToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.roughToolStripMenuItem.Text = "Rough";
            this.roughToolStripMenuItem.Click += new System.EventHandler(this.roughToolStripMenuItem_Click);
            // 
            // sandToolStripMenuItem
            // 
            this.sandToolStripMenuItem.CheckOnClick = true;
            this.sandToolStripMenuItem.Name = "sandToolStripMenuItem";
            this.sandToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.sandToolStripMenuItem.Text = "Sand";
            this.sandToolStripMenuItem.Click += new System.EventHandler(this.sandToolStripMenuItem_Click);
            // 
            // swampToolStripMenuItem
            // 
            this.swampToolStripMenuItem.CheckOnClick = true;
            this.swampToolStripMenuItem.Name = "swampToolStripMenuItem";
            this.swampToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.swampToolStripMenuItem.Text = "Swamp";
            this.swampToolStripMenuItem.Click += new System.EventHandler(this.swampToolStripMenuItem_Click);
            // 
            // waterToolStripMenuItem
            // 
            this.waterToolStripMenuItem.CheckOnClick = true;
            this.waterToolStripMenuItem.Name = "waterToolStripMenuItem";
            this.waterToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.waterToolStripMenuItem.Text = "Water";
            this.waterToolStripMenuItem.Click += new System.EventHandler(this.waterToolStripMenuItem_Click);
            // 
            // woodlandToolStripMenuItem
            // 
            this.woodlandToolStripMenuItem.CheckOnClick = true;
            this.woodlandToolStripMenuItem.Name = "woodlandToolStripMenuItem";
            this.woodlandToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.woodlandToolStripMenuItem.Text = "Woodland";
            this.woodlandToolStripMenuItem.Click += new System.EventHandler(this.woodlandToolStripMenuItem_Click);
            // 
            // placeStartToolStripMenuItem
            // 
            this.placeStartToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.player1ToolStripMenuItem,
            this.player2ToolStripMenuItem,
            this.player3ToolStripMenuItem,
            this.player4ToolStripMenuItem});
            this.placeStartToolStripMenuItem.Name = "placeStartToolStripMenuItem";
            this.placeStartToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.placeStartToolStripMenuItem.Text = "Place Start";
            // 
            // player1ToolStripMenuItem
            // 
            this.player1ToolStripMenuItem.CheckOnClick = true;
            this.player1ToolStripMenuItem.Name = "player1ToolStripMenuItem";
            this.player1ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.player1ToolStripMenuItem.Text = "Player 1";
            this.player1ToolStripMenuItem.Click += new System.EventHandler(this.player1ToolStripMenuItem_Click);
            // 
            // player2ToolStripMenuItem
            // 
            this.player2ToolStripMenuItem.CheckOnClick = true;
            this.player2ToolStripMenuItem.Name = "player2ToolStripMenuItem";
            this.player2ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.player2ToolStripMenuItem.Text = "Player 2";
            this.player2ToolStripMenuItem.Click += new System.EventHandler(this.player2ToolStripMenuItem_Click);
            // 
            // player3ToolStripMenuItem
            // 
            this.player3ToolStripMenuItem.CheckOnClick = true;
            this.player3ToolStripMenuItem.Name = "player3ToolStripMenuItem";
            this.player3ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.player3ToolStripMenuItem.Text = "Player 3";
            this.player3ToolStripMenuItem.Click += new System.EventHandler(this.player3ToolStripMenuItem_Click);
            // 
            // player4ToolStripMenuItem
            // 
            this.player4ToolStripMenuItem.CheckOnClick = true;
            this.player4ToolStripMenuItem.Name = "player4ToolStripMenuItem";
            this.player4ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.player4ToolStripMenuItem.Text = "Player 4";
            this.player4ToolStripMenuItem.Click += new System.EventHandler(this.player4ToolStripMenuItem_Click);
            // 
            // nameToolStripMenuItem
            // 
            this.nameToolStripMenuItem.Name = "nameToolStripMenuItem";
            this.nameToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.nameToolStripMenuItem.Text = "Name";
            this.nameToolStripMenuItem.Click += new System.EventHandler(this.nameToolStripMenuItem_Click);
            // 
            // descriptionToolStripMenuItem
            // 
            this.descriptionToolStripMenuItem.Name = "descriptionToolStripMenuItem";
            this.descriptionToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.descriptionToolStripMenuItem.Text = "Description";
            this.descriptionToolStripMenuItem.Click += new System.EventHandler(this.descriptionToolStripMenuItem_Click);
            // 
            // threatValueToolStripMenuItem
            // 
            this.threatValueToolStripMenuItem.Name = "threatValueToolStripMenuItem";
            this.threatValueToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.threatValueToolStripMenuItem.Text = "Threat Value";
            this.threatValueToolStripMenuItem.Click += new System.EventHandler(this.threatValueToolStripMenuItem_Click);
            // 
            // missionToolStripMenuItem
            // 
            this.missionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.objectiveToolStripMenuItem});
            this.missionToolStripMenuItem.Enabled = false;
            this.missionToolStripMenuItem.Name = "missionToolStripMenuItem";
            this.missionToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.missionToolStripMenuItem.Text = "Mission";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.captureToolStripMenuItem,
            this.destroyToolStripMenuItem,
            this.skirmishToolStripMenuItem});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(146, 22);
            this.toolStripMenuItem2.Text = "Mission Type";
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.captureToolStripMenuItem.Text = "Capture and Hold";
            // 
            // destroyToolStripMenuItem
            // 
            this.destroyToolStripMenuItem.Name = "destroyToolStripMenuItem";
            this.destroyToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.destroyToolStripMenuItem.Text = "Assault";
            // 
            // skirmishToolStripMenuItem
            // 
            this.skirmishToolStripMenuItem.Name = "skirmishToolStripMenuItem";
            this.skirmishToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.skirmishToolStripMenuItem.Text = "Skirmish";
            // 
            // objectiveToolStripMenuItem
            // 
            this.objectiveToolStripMenuItem.Name = "objectiveToolStripMenuItem";
            this.objectiveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.objectiveToolStripMenuItem.Text = "Objective";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewElevationToolStripMenuItem,
            this.viewStartPositionsToolStripMenuItem,
            this.viewHexTypesToolStripMenuItem,
            this.viewHexGridToolStripMenuItem});
            this.viewToolStripMenuItem.Enabled = false;
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // viewElevationToolStripMenuItem
            // 
            this.viewElevationToolStripMenuItem.Checked = true;
            this.viewElevationToolStripMenuItem.CheckOnClick = true;
            this.viewElevationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewElevationToolStripMenuItem.Name = "viewElevationToolStripMenuItem";
            this.viewElevationToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.viewElevationToolStripMenuItem.Text = "Elevation";
            this.viewElevationToolStripMenuItem.Click += new System.EventHandler(this.elevationToolStripMenuItem_Click);
            // 
            // viewStartPositionsToolStripMenuItem
            // 
            this.viewStartPositionsToolStripMenuItem.Checked = true;
            this.viewStartPositionsToolStripMenuItem.CheckOnClick = true;
            this.viewStartPositionsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewStartPositionsToolStripMenuItem.Name = "viewStartPositionsToolStripMenuItem";
            this.viewStartPositionsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.viewStartPositionsToolStripMenuItem.Text = "Start Positions";
            this.viewStartPositionsToolStripMenuItem.Click += new System.EventHandler(this.startPositionsToolStripMenuItem_Click);
            // 
            // viewHexTypesToolStripMenuItem
            // 
            this.viewHexTypesToolStripMenuItem.Checked = true;
            this.viewHexTypesToolStripMenuItem.CheckOnClick = true;
            this.viewHexTypesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewHexTypesToolStripMenuItem.Name = "viewHexTypesToolStripMenuItem";
            this.viewHexTypesToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.viewHexTypesToolStripMenuItem.Text = "Hex Types";
            this.viewHexTypesToolStripMenuItem.Click += new System.EventHandler(this.viewHexTypesToolStripMenuItem_Click);
            // 
            // viewHexGridToolStripMenuItem
            // 
            this.viewHexGridToolStripMenuItem.Checked = true;
            this.viewHexGridToolStripMenuItem.CheckOnClick = true;
            this.viewHexGridToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.viewHexGridToolStripMenuItem.Name = "viewHexGridToolStripMenuItem";
            this.viewHexGridToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.viewHexGridToolStripMenuItem.Text = "Hex Grid";
            this.viewHexGridToolStripMenuItem.Click += new System.EventHandler(this.hexGridToolStripMenuItem_Click);
            // 
            // mapEditor
            // 
            this.mapEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.mapEditor.AutoScrollMargin = new System.Drawing.Size(10, 10);
            this.mapEditor.Location = new System.Drawing.Point(0, 27);
            this.mapEditor.Name = "mapEditor";
            this.mapEditor.Size = new System.Drawing.Size(640, 480);
            this.mapEditor.TabIndex = 1;
            this.mapEditor.ViewElevation = true;
            this.mapEditor.ViewHexGrid = true;
            this.mapEditor.ViewHexType = true;
            this.mapEditor.ViewPlayerStarts = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 507);
            this.Controls.Add(this.mapEditor);
            this.Controls.Add(this.mainMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.Name = "MainForm";
            this.Text = "Heavy Gear Tactics Map Editor";
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewElevationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewStartPositionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHexGridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem missionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem objectiveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem destroyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skirmishToolStripMenuItem;
        private MapEditor mapEditor;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem elevationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hexTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem placeStartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem player1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem player2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem player3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem player4ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jungleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem roughToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem swampToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem waterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem woodlandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewHexTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descriptionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threatValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

