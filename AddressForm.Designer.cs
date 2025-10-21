namespace QuickLauncher
{
    partial class AddressForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddressForm));
            this.panelAddress = new System.Windows.Forms.Panel();
            this.valueAddress = new System.Windows.Forms.TextBox();
            this.btnClearAddress = new System.Windows.Forms.Button();
            this.btnConfused = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.valueProfile = new System.Windows.Forms.TextBox();
            this.btnClearProfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSanitizedInput = new System.Windows.Forms.Label();
            this.panelAddress.SuspendLayout();
            this.panelProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelAddress
            // 
            this.panelAddress.AllowDrop = true;
            this.panelAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelAddress.Controls.Add(this.valueAddress);
            this.panelAddress.Controls.Add(this.btnClearAddress);
            this.panelAddress.Location = new System.Drawing.Point(12, 12);
            this.panelAddress.Name = "panelAddress";
            this.panelAddress.Size = new System.Drawing.Size(644, 36);
            this.panelAddress.TabIndex = 10;
            // 
            // valueAddress
            // 
            this.valueAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.valueAddress.ForeColor = System.Drawing.Color.Silver;
            this.valueAddress.Location = new System.Drawing.Point(3, 4);
            this.valueAddress.Name = "valueAddress";
            this.valueAddress.Size = new System.Drawing.Size(602, 27);
            this.valueAddress.TabIndex = 2;
            this.valueAddress.Text = "https://127.0.0.1:6969";
            this.valueAddress.TextChanged += new System.EventHandler(this.valueAddress_TextChanged);
            // 
            // btnClearAddress
            // 
            this.btnClearAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearAddress.AutoEllipsis = true;
            this.btnClearAddress.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearAddress.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnClearAddress.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearAddress.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.btnClearAddress.ForeColor = System.Drawing.Color.IndianRed;
            this.btnClearAddress.Location = new System.Drawing.Point(611, 3);
            this.btnClearAddress.Name = "btnClearAddress";
            this.btnClearAddress.Size = new System.Drawing.Size(30, 30);
            this.btnClearAddress.TabIndex = 1;
            this.btnClearAddress.Text = "X";
            this.btnClearAddress.UseVisualStyleBackColor = true;
            this.btnClearAddress.Click += new System.EventHandler(this.btnClearAddress_Click);
            // 
            // btnConfused
            // 
            this.btnConfused.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfused.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnConfused.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfused.ForeColor = System.Drawing.Color.Silver;
            this.btnConfused.Location = new System.Drawing.Point(467, 153);
            this.btnConfused.Name = "btnConfused";
            this.btnConfused.Size = new System.Drawing.Size(189, 33);
            this.btnConfused.TabIndex = 11;
            this.btnConfused.Text = "❓ Confused?";
            this.btnConfused.UseVisualStyleBackColor = true;
            this.btnConfused.Click += new System.EventHandler(this.btnConfused_Click);
            // 
            // btnApply
            // 
            this.btnApply.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.ForeColor = System.Drawing.Color.DodgerBlue;
            this.btnApply.Location = new System.Drawing.Point(467, 114);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(189, 33);
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "✔️ Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // panelProfile
            // 
            this.panelProfile.AllowDrop = true;
            this.panelProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.panelProfile.Controls.Add(this.valueProfile);
            this.panelProfile.Controls.Add(this.btnClearProfile);
            this.panelProfile.Location = new System.Drawing.Point(12, 63);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(644, 36);
            this.panelProfile.TabIndex = 13;
            // 
            // valueProfile
            // 
            this.valueProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.valueProfile.ForeColor = System.Drawing.Color.Silver;
            this.valueProfile.Location = new System.Drawing.Point(3, 4);
            this.valueProfile.Name = "valueProfile";
            this.valueProfile.Size = new System.Drawing.Size(602, 27);
            this.valueProfile.TabIndex = 2;
            this.valueProfile.Text = "191";
            // 
            // btnClearProfile
            // 
            this.btnClearProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearProfile.AutoEllipsis = true;
            this.btnClearProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearProfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.btnClearProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearProfile.Font = new System.Drawing.Font("Bahnschrift", 12F);
            this.btnClearProfile.ForeColor = System.Drawing.Color.IndianRed;
            this.btnClearProfile.Location = new System.Drawing.Point(611, 3);
            this.btnClearProfile.Name = "btnClearProfile";
            this.btnClearProfile.Size = new System.Drawing.Size(30, 30);
            this.btnClearProfile.TabIndex = 1;
            this.btnClearProfile.Text = "X";
            this.btnClearProfile.UseVisualStyleBackColor = true;
            this.btnClearProfile.Click += new System.EventHandler(this.btnClearProfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(9, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(332, 34);
            this.label1.TabIndex = 14;
            this.label1.Text = "Insert the AID from your profile and remove `.json`\r\nExample: 68eacb98ba8fa004842" +
    "fbc42.json";
            // 
            // lblSanitizedInput
            // 
            this.lblSanitizedInput.AutoSize = true;
            this.lblSanitizedInput.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.lblSanitizedInput.ForeColor = System.Drawing.Color.Gray;
            this.lblSanitizedInput.Location = new System.Drawing.Point(12, 46);
            this.lblSanitizedInput.Name = "lblSanitizedInput";
            this.lblSanitizedInput.Size = new System.Drawing.Size(89, 14);
            this.lblSanitizedInput.TabIndex = 15;
            this.lblSanitizedInput.Text = "Sanitized input:";
            // 
            // AddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(668, 207);
            this.Controls.Add(this.lblSanitizedInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelProfile);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnConfused);
            this.Controls.Add(this.panelAddress);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddressForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Address";
            this.Load += new System.EventHandler(this.AddressForm_Load);
            this.panelAddress.ResumeLayout(false);
            this.panelAddress.PerformLayout();
            this.panelProfile.ResumeLayout(false);
            this.panelProfile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelAddress;
        private System.Windows.Forms.Button btnClearAddress;
        private System.Windows.Forms.Button btnConfused;
        private System.Windows.Forms.TextBox valueAddress;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.TextBox valueProfile;
        private System.Windows.Forms.Button btnClearProfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSanitizedInput;
    }
}