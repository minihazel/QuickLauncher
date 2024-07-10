namespace QuickLauncher
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.lblLimit1 = new System.Windows.Forms.Label();
            this.timeoutLimit = new System.Windows.Forms.NumericUpDown();
            this.lblLimit3 = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.tooltipMain = new System.Windows.Forms.ToolTip(this.components);
            this.panelPath = new System.Windows.Forms.Panel();
            this.btnShowPath = new System.Windows.Forms.Button();
            this.chkToggleMenu = new System.Windows.Forms.Button();
            this.btnClearTempFiles = new System.Windows.Forms.Button();
            this.chkToggleServer = new System.Windows.Forms.Button();
            this.btnViewFAQ = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.chkTogglePath = new System.Windows.Forms.Button();
            this.btnClearPath = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutLimit)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.panelPath.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLimit1
            // 
            this.lblLimit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLimit1.AutoSize = true;
            this.lblLimit1.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.lblLimit1.Location = new System.Drawing.Point(1, 10);
            this.lblLimit1.Name = "lblLimit1";
            this.lblLimit1.Size = new System.Drawing.Size(79, 14);
            this.lblLimit1.TabIndex = 0;
            this.lblLimit1.Text = "Timeout limit:";
            // 
            // timeoutLimit
            // 
            this.timeoutLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.timeoutLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.timeoutLimit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.timeoutLimit.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.timeoutLimit.ForeColor = System.Drawing.Color.LightGray;
            this.timeoutLimit.Location = new System.Drawing.Point(86, 7);
            this.timeoutLimit.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.timeoutLimit.Name = "timeoutLimit";
            this.timeoutLimit.Size = new System.Drawing.Size(47, 23);
            this.timeoutLimit.TabIndex = 1;
            this.timeoutLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblLimit3
            // 
            this.lblLimit3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLimit3.AutoSize = true;
            this.lblLimit3.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.lblLimit3.Location = new System.Drawing.Point(139, 10);
            this.lblLimit3.Name = "lblLimit3";
            this.lblLimit3.Size = new System.Drawing.Size(50, 14);
            this.lblLimit3.TabIndex = 2;
            this.lblLimit3.Text = "minutes";
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelBottom.Controls.Add(this.lblLimit1);
            this.panelBottom.Controls.Add(this.lblLimit3);
            this.panelBottom.Controls.Add(this.timeoutLimit);
            this.panelBottom.Location = new System.Drawing.Point(9, 337);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(308, 36);
            this.panelBottom.TabIndex = 4;
            this.panelBottom.Visible = false;
            // 
            // tooltipMain
            // 
            this.tooltipMain.AutoPopDelay = 10000;
            this.tooltipMain.InitialDelay = 500;
            this.tooltipMain.ReshowDelay = 100;
            this.tooltipMain.ToolTipTitle = "QuickLauncher";
            // 
            // panelPath
            // 
            this.panelPath.AllowDrop = true;
            this.panelPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelPath.Controls.Add(this.btnClearPath);
            this.panelPath.Controls.Add(this.btnShowPath);
            this.panelPath.Location = new System.Drawing.Point(9, 337);
            this.panelPath.Name = "panelPath";
            this.panelPath.Size = new System.Drawing.Size(308, 36);
            this.panelPath.TabIndex = 9;
            this.panelPath.Visible = false;
            // 
            // btnShowPath
            // 
            this.btnShowPath.AllowDrop = true;
            this.btnShowPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowPath.AutoEllipsis = true;
            this.btnShowPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnShowPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnShowPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowPath.Font = new System.Drawing.Font("Bahnschrift Light", 8F);
            this.btnShowPath.Location = new System.Drawing.Point(3, 3);
            this.btnShowPath.Name = "btnShowPath";
            this.btnShowPath.Size = new System.Drawing.Size(266, 30);
            this.btnShowPath.TabIndex = 0;
            this.btnShowPath.Text = "No path set!";
            this.btnShowPath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnShowPath.UseVisualStyleBackColor = true;
            this.btnShowPath.Click += new System.EventHandler(this.btnShowPath_Click);
            // 
            // chkToggleMenu
            // 
            this.chkToggleMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkToggleMenu.BackgroundImage = global::QuickLauncher.Properties.Resources.menu_bar;
            this.chkToggleMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkToggleMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkToggleMenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.chkToggleMenu.FlatAppearance.BorderSize = 0;
            this.chkToggleMenu.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.chkToggleMenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.chkToggleMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkToggleMenu.Location = new System.Drawing.Point(9, 384);
            this.chkToggleMenu.Name = "chkToggleMenu";
            this.chkToggleMenu.Size = new System.Drawing.Size(23, 23);
            this.chkToggleMenu.TabIndex = 8;
            this.tooltipMain.SetToolTip(this.chkToggleMenu, "Show the mini-bar that shows the timeout limit");
            this.chkToggleMenu.UseVisualStyleBackColor = true;
            this.chkToggleMenu.Click += new System.EventHandler(this.chkToggleMenu_Click);
            // 
            // btnClearTempFiles
            // 
            this.btnClearTempFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearTempFiles.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClearTempFiles.BackgroundImage")));
            this.btnClearTempFiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClearTempFiles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearTempFiles.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.btnClearTempFiles.FlatAppearance.BorderSize = 0;
            this.btnClearTempFiles.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClearTempFiles.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClearTempFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearTempFiles.Location = new System.Drawing.Point(185, 379);
            this.btnClearTempFiles.Name = "btnClearTempFiles";
            this.btnClearTempFiles.Size = new System.Drawing.Size(32, 32);
            this.btnClearTempFiles.TabIndex = 7;
            this.btnClearTempFiles.Tag = "inactive";
            this.tooltipMain.SetToolTip(this.btnClearTempFiles, "Clear the SPT temp files");
            this.btnClearTempFiles.UseVisualStyleBackColor = true;
            // 
            // chkToggleServer
            // 
            this.chkToggleServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkToggleServer.BackgroundImage = global::QuickLauncher.Properties.Resources.send_inactive;
            this.chkToggleServer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkToggleServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkToggleServer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkToggleServer.FlatAppearance.BorderSize = 0;
            this.chkToggleServer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.chkToggleServer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.chkToggleServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkToggleServer.Location = new System.Drawing.Point(223, 381);
            this.chkToggleServer.Name = "chkToggleServer";
            this.chkToggleServer.Size = new System.Drawing.Size(28, 28);
            this.chkToggleServer.TabIndex = 6;
            this.chkToggleServer.Tag = "inactive";
            this.tooltipMain.SetToolTip(this.chkToggleServer, "Toggle-able: Should the server console show when SPT is launched?");
            this.chkToggleServer.UseVisualStyleBackColor = true;
            this.chkToggleServer.Click += new System.EventHandler(this.chkToggleServer_Click);
            // 
            // btnViewFAQ
            // 
            this.btnViewFAQ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewFAQ.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnViewFAQ.BackgroundImage")));
            this.btnViewFAQ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnViewFAQ.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewFAQ.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnViewFAQ.FlatAppearance.BorderSize = 0;
            this.btnViewFAQ.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnViewFAQ.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnViewFAQ.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewFAQ.Location = new System.Drawing.Point(257, 381);
            this.btnViewFAQ.Name = "btnViewFAQ";
            this.btnViewFAQ.Size = new System.Drawing.Size(28, 28);
            this.btnViewFAQ.TabIndex = 5;
            this.tooltipMain.SetToolTip(this.btnViewFAQ, "Display the FAQ window");
            this.btnViewFAQ.UseVisualStyleBackColor = true;
            this.btnViewFAQ.Click += new System.EventHandler(this.btnViewPlaytime_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReload.BackgroundImage")));
            this.btnReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnReload.FlatAppearance.BorderSize = 0;
            this.btnReload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Location = new System.Drawing.Point(291, 384);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 23);
            this.btnReload.TabIndex = 3;
            this.tooltipMain.SetToolTip(this.btnReload, "Reload the app");
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // chkTogglePath
            // 
            this.chkTogglePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkTogglePath.BackgroundImage = global::QuickLauncher.Properties.Resources.path;
            this.chkTogglePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.chkTogglePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkTogglePath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.chkTogglePath.FlatAppearance.BorderSize = 0;
            this.chkTogglePath.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.chkTogglePath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.chkTogglePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkTogglePath.Location = new System.Drawing.Point(41, 384);
            this.chkTogglePath.Name = "chkTogglePath";
            this.chkTogglePath.Size = new System.Drawing.Size(23, 23);
            this.chkTogglePath.TabIndex = 10;
            this.chkTogglePath.Tag = "inactive";
            this.tooltipMain.SetToolTip(this.chkTogglePath, "Clear the SPT temp files");
            this.chkTogglePath.UseVisualStyleBackColor = true;
            this.chkTogglePath.Click += new System.EventHandler(this.chkTogglePath_Click);
            // 
            // btnClearPath
            // 
            this.btnClearPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearPath.AutoEllipsis = true;
            this.btnClearPath.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnClearPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearPath.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.btnClearPath.ForeColor = System.Drawing.Color.IndianRed;
            this.btnClearPath.Location = new System.Drawing.Point(275, 3);
            this.btnClearPath.Name = "btnClearPath";
            this.btnClearPath.Size = new System.Drawing.Size(30, 30);
            this.btnClearPath.TabIndex = 1;
            this.btnClearPath.Text = "X";
            this.btnClearPath.UseVisualStyleBackColor = true;
            this.btnClearPath.Click += new System.EventHandler(this.btnClearPath_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(326, 419);
            this.Controls.Add(this.chkTogglePath);
            this.Controls.Add(this.panelPath);
            this.Controls.Add(this.chkToggleMenu);
            this.Controls.Add(this.btnClearTempFiles);
            this.Controls.Add(this.chkToggleServer);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.btnViewFAQ);
            this.Controls.Add(this.btnReload);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickLauncher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeoutLimit)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.panelPath.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLimit1;
        private System.Windows.Forms.NumericUpDown timeoutLimit;
        private System.Windows.Forms.Label lblLimit3;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Button btnViewFAQ;
        private System.Windows.Forms.Button chkToggleServer;
        private System.Windows.Forms.ToolTip tooltipMain;
        private System.Windows.Forms.Button btnClearTempFiles;
        private System.Windows.Forms.Button chkToggleMenu;
        private System.Windows.Forms.Panel panelPath;
        private System.Windows.Forms.Button btnShowPath;
        private System.Windows.Forms.Button chkTogglePath;
        private System.Windows.Forms.Button btnClearPath;
    }
}

