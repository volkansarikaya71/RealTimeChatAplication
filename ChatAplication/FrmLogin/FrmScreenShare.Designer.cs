namespace FrmLogin
{
    partial class FrmScreenShare
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
            this.pcbscreen = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcbscreen)).BeginInit();
            this.SuspendLayout();
            // 
            // pcbscreen
            // 
            this.pcbscreen.Image = global::FrmLogin.Properties.Resources.dowloadMovie;
            this.pcbscreen.Location = new System.Drawing.Point(13, 59);
            this.pcbscreen.Name = "pcbscreen";
            this.pcbscreen.Size = new System.Drawing.Size(775, 379);
            this.pcbscreen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcbscreen.TabIndex = 0;
            this.pcbscreen.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(320, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmScreenShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pcbscreen);
            this.Name = "FrmScreenShare";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmScreenShare";
            this.Load += new System.EventHandler(this.FrmScreenShare_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbscreen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pcbscreen;
        private System.Windows.Forms.Button button1;
    }
}