namespace WPF_CloverApp
{
    partial class FormInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelArtistLink = new System.Windows.Forms.LinkLabel();
            this.labelClover = new System.Windows.Forms.LinkLabel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDebug = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clover Desktop Companion";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(244, 14);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(41, 13);
            this.labelVersion.TabIndex = 1;
            this.labelVersion.Text = "version";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Written by: Kat";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 42);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Art by:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 55);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Art for:";
            // 
            // labelArtistLink
            // 
            this.labelArtistLink.AutoSize = true;
            this.labelArtistLink.Location = new System.Drawing.Point(60, 42);
            this.labelArtistLink.Name = "labelArtistLink";
            this.labelArtistLink.Size = new System.Drawing.Size(139, 13);
            this.labelArtistLink.TabIndex = 5;
            this.labelArtistLink.TabStop = true;
            this.labelArtistLink.Text = "マヤヅカ (3Renico @ twitter)";
            this.labelArtistLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelArtistLink_LinkClicked);
            // 
            // labelClover
            // 
            this.labelClover.AutoSize = true;
            this.labelClover.Location = new System.Drawing.Point(60, 55);
            this.labelClover.Name = "labelClover";
            this.labelClover.Size = new System.Drawing.Size(139, 13);
            this.labelClover.TabIndex = 6;
            this.labelClover.TabStop = true;
            this.labelClover.Text = "Clover (cloverinari @ twitter)";
            this.labelClover.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.labelClover_LinkClicked);
            this.labelClover.DoubleClick += new System.EventHandler(this.labelClover_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(241, 88);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close app";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDebug
            // 
            this.btnDebug.Location = new System.Drawing.Point(241, 44);
            this.btnDebug.Name = "btnDebug";
            this.btnDebug.Size = new System.Drawing.Size(75, 23);
            this.btnDebug.TabIndex = 8;
            this.btnDebug.Text = "Debug log";
            this.btnDebug.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 39);
            this.label2.TabIndex = 9;
            this.label2.Text = "All artwork in this application is property of\r\nits rightful owner(s). Artwork is" +
    " used with\r\nwritten permission from the artist.";
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(241, 66);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 23);
            this.btnSettings.TabIndex = 10;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // FormInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 122);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDebug);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.labelClover);
            this.Controls.Add(this.labelArtistLink);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel labelArtistLink;
        private System.Windows.Forms.LinkLabel labelClover;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDebug;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSettings;
    }
}