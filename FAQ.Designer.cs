namespace QuickLauncher
{
    partial class FAQ
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAQ));
            this.faqTitle = new System.Windows.Forms.Label();
            this.faqText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // faqTitle
            // 
            this.faqTitle.Font = new System.Drawing.Font("Bahnschrift", 22F);
            this.faqTitle.Location = new System.Drawing.Point(12, 9);
            this.faqTitle.Name = "faqTitle";
            this.faqTitle.Size = new System.Drawing.Size(465, 85);
            this.faqTitle.TabIndex = 0;
            this.faqTitle.Text = "What is this?";
            this.faqTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // faqText
            // 
            this.faqText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.faqText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.faqText.Font = new System.Drawing.Font("Bahnschrift Light", 16F);
            this.faqText.ForeColor = System.Drawing.Color.LightGray;
            this.faqText.Location = new System.Drawing.Point(18, 97);
            this.faqText.Name = "faqText";
            this.faqText.ReadOnly = true;
            this.faqText.Size = new System.Drawing.Size(459, 503);
            this.faqText.TabIndex = 1;
            this.faqText.Text = resources.GetString("faqText.Text");
            this.faqText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
            // 
            // FAQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.ClientSize = new System.Drawing.Size(489, 612);
            this.Controls.Add(this.faqText);
            this.Controls.Add(this.faqTitle);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 12F);
            this.ForeColor = System.Drawing.Color.LightGray;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FAQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FAQ";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label faqTitle;
        private System.Windows.Forms.RichTextBox faqText;
    }
}