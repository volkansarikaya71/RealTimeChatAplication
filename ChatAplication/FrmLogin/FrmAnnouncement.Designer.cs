namespace FrmLogin
{
    partial class FrmAnnouncement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnnouncement));
            this.lblUserName = new System.Windows.Forms.Label();
            this.vwmovie = new LibVLCSharp.WinForms.VideoView();
            ((System.ComponentModel.ISupportInitialize)(this.vwmovie)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserName.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblUserName.Location = new System.Drawing.Point(198, 9);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(238, 26);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Kullanıcısından Yeni Mesaj";
            // 
            // vwmovie
            // 
            this.vwmovie.BackColor = System.Drawing.Color.Black;
            this.vwmovie.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.vwmovie.Location = new System.Drawing.Point(2, 38);
            this.vwmovie.MediaPlayer = null;
            this.vwmovie.Name = "vwmovie";
            this.vwmovie.Size = new System.Drawing.Size(636, 124);
            this.vwmovie.TabIndex = 1;
            this.vwmovie.Text = "videoView1";
            // 
            // FrmAnnouncement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(640, 166);
            this.Controls.Add(this.vwmovie);
            this.Controls.Add(this.lblUserName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmAnnouncement";
            this.Text = "Bildirim";
            this.Load += new System.EventHandler(this.FrmAnnouncement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vwmovie)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private LibVLCSharp.WinForms.VideoView vwmovie;
    }
}