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
            this.lblLimit1 = new System.Windows.Forms.Label();
            this.timeoutLimit = new System.Windows.Forms.NumericUpDown();
            this.lblLimit3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.timeoutLimit)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLimit1
            // 
            this.lblLimit1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLimit1.AutoSize = true;
            this.lblLimit1.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.lblLimit1.Location = new System.Drawing.Point(3, 390);
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
            this.timeoutLimit.Location = new System.Drawing.Point(88, 387);
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
            this.lblLimit3.Location = new System.Drawing.Point(141, 390);
            this.lblLimit3.Name = "lblLimit3";
            this.lblLimit3.Size = new System.Drawing.Size(50, 14);
            this.lblLimit3.TabIndex = 2;
            this.lblLimit3.Text = "minutes";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(326, 419);
            this.Controls.Add(this.lblLimit3);
            this.Controls.Add(this.timeoutLimit);
            this.Controls.Add(this.lblLimit1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblLimit1;
        private System.Windows.Forms.NumericUpDown timeoutLimit;
        private System.Windows.Forms.Label lblLimit3;
    }
}

