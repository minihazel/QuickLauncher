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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.lblLimit1 = new System.Windows.Forms.Label();
            this.timeoutLimit = new System.Windows.Forms.NumericUpDown();
            this.lblLimit3 = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.panelBottom = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutLimit)).BeginInit();
            this.panelBottom.SuspendLayout();
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
            this.timeoutLimit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
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
            this.btnReload.Location = new System.Drawing.Point(284, 7);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 23);
            this.btnReload.TabIndex = 3;
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // panelBottom
            // 
            this.panelBottom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBottom.Controls.Add(this.btnReload);
            this.panelBottom.Controls.Add(this.lblLimit1);
            this.panelBottom.Controls.Add(this.lblLimit3);
            this.panelBottom.Controls.Add(this.timeoutLimit);
            this.panelBottom.Location = new System.Drawing.Point(6, 379);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(308, 33);
            this.panelBottom.TabIndex = 4;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(326, 419);
            this.Controls.Add(this.panelBottom);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QuickLauncher";
            this.Load += new System.EventHandler(this.mainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.timeoutLimit)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLimit1;
        private System.Windows.Forms.NumericUpDown timeoutLimit;
        private System.Windows.Forms.Label lblLimit3;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel panelBottom;
    }
}

