namespace FrmLogin
{
    partial class FrmForgotPassword
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
            this.txtUserEmail = new System.Windows.Forms.TextBox();
            this.lblUserMail = new System.Windows.Forms.Label();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.lblUserpassword = new System.Windows.Forms.Label();
            this.txtUserToken = new System.Windows.Forms.TextBox();
            this.lblUserToken = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pcbUserPasswordShoworHide = new System.Windows.Forms.PictureBox();
            this.pcbUserPasswordUpdate = new System.Windows.Forms.PictureBox();
            this.pcbAddUserToken = new System.Windows.Forms.PictureBox();
            this.imgUserUpdate = new System.Windows.Forms.PictureBox();
            this.imgFrmUserLogin = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddUserToken)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgUserUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserEmail
            // 
            this.txtUserEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserEmail.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserEmail.Location = new System.Drawing.Point(306, 174);
            this.txtUserEmail.MaxLength = 255;
            this.txtUserEmail.Name = "txtUserEmail";
            this.txtUserEmail.Size = new System.Drawing.Size(244, 33);
            this.txtUserEmail.TabIndex = 11;
            // 
            // lblUserMail
            // 
            this.lblUserMail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserMail.AutoSize = true;
            this.lblUserMail.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserMail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lblUserMail.Location = new System.Drawing.Point(140, 177);
            this.lblUserMail.Name = "lblUserMail";
            this.lblUserMail.Size = new System.Drawing.Size(147, 26);
            this.lblUserMail.TabIndex = 10;
            this.lblUserMail.Text = "Email Adresiniz:";
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserPassword.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserPassword.Location = new System.Drawing.Point(306, 277);
            this.txtUserPassword.MaxLength = 100;
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(244, 33);
            this.txtUserPassword.TabIndex = 13;
            this.txtUserPassword.Visible = false;
            // 
            // lblUserpassword
            // 
            this.lblUserpassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserpassword.AutoSize = true;
            this.lblUserpassword.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserpassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lblUserpassword.Location = new System.Drawing.Point(207, 284);
            this.lblUserpassword.Name = "lblUserpassword";
            this.lblUserpassword.Size = new System.Drawing.Size(80, 26);
            this.lblUserpassword.TabIndex = 12;
            this.lblUserpassword.Text = "Şifreniz:";
            this.lblUserpassword.Visible = false;
            // 
            // txtUserToken
            // 
            this.txtUserToken.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserToken.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserToken.Location = new System.Drawing.Point(306, 322);
            this.txtUserToken.MaxLength = 32600;
            this.txtUserToken.Name = "txtUserToken";
            this.txtUserToken.Size = new System.Drawing.Size(244, 33);
            this.txtUserToken.TabIndex = 15;
            this.txtUserToken.Visible = false;
            // 
            // lblUserToken
            // 
            this.lblUserToken.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserToken.AutoSize = true;
            this.lblUserToken.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserToken.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lblUserToken.Location = new System.Drawing.Point(238, 322);
            this.lblUserToken.Name = "lblUserToken";
            this.lblUserToken.Size = new System.Drawing.Size(49, 26);
            this.lblUserToken.TabIndex = 14;
            this.lblUserToken.Text = "Key:";
            this.lblUserToken.Visible = false;
            // 
            // pcbUserPasswordShoworHide
            // 
            this.pcbUserPasswordShoworHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserPasswordShoworHide.Image = global::FrmLogin.Properties.Resources.gizle;
            this.pcbUserPasswordShoworHide.Location = new System.Drawing.Point(556, 278);
            this.pcbUserPasswordShoworHide.Name = "pcbUserPasswordShoworHide";
            this.pcbUserPasswordShoworHide.Size = new System.Drawing.Size(53, 32);
            this.pcbUserPasswordShoworHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserPasswordShoworHide.TabIndex = 23;
            this.pcbUserPasswordShoworHide.TabStop = false;
            this.pcbUserPasswordShoworHide.Visible = false;
            this.pcbUserPasswordShoworHide.Click += new System.EventHandler(this.pcbUserPasswordShoworHide_Click);
            // 
            // pcbUserPasswordUpdate
            // 
            this.pcbUserPasswordUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserPasswordUpdate.Image = global::FrmLogin.Properties.Resources.save;
            this.pcbUserPasswordUpdate.Location = new System.Drawing.Point(560, 371);
            this.pcbUserPasswordUpdate.Name = "pcbUserPasswordUpdate";
            this.pcbUserPasswordUpdate.Size = new System.Drawing.Size(101, 67);
            this.pcbUserPasswordUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserPasswordUpdate.TabIndex = 22;
            this.pcbUserPasswordUpdate.TabStop = false;
            this.pcbUserPasswordUpdate.Visible = false;
            this.pcbUserPasswordUpdate.Click += new System.EventHandler(this.pcbUserPasswordUpdate_Click);
            // 
            // pcbAddUserToken
            // 
            this.pcbAddUserToken.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbAddUserToken.Image = global::FrmLogin.Properties.Resources.Eposta_yolla;
            this.pcbAddUserToken.Location = new System.Drawing.Point(381, 213);
            this.pcbAddUserToken.Name = "pcbAddUserToken";
            this.pcbAddUserToken.Size = new System.Drawing.Size(101, 42);
            this.pcbAddUserToken.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAddUserToken.TabIndex = 21;
            this.pcbAddUserToken.TabStop = false;
            this.pcbAddUserToken.Click += new System.EventHandler(this.pcbAddUserToken_Click);
            // 
            // imgUserUpdate
            // 
            this.imgUserUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgUserUpdate.Image = global::FrmLogin.Properties.Resources.Sifre_Sifirlama;
            this.imgUserUpdate.Location = new System.Drawing.Point(296, 12);
            this.imgUserUpdate.Name = "imgUserUpdate";
            this.imgUserUpdate.Size = new System.Drawing.Size(254, 146);
            this.imgUserUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgUserUpdate.TabIndex = 20;
            this.imgUserUpdate.TabStop = false;
            // 
            // imgFrmUserLogin
            // 
            this.imgFrmUserLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgFrmUserLogin.Image = global::FrmLogin.Properties.Resources.geri;
            this.imgFrmUserLogin.Location = new System.Drawing.Point(12, 23);
            this.imgFrmUserLogin.Name = "imgFrmUserLogin";
            this.imgFrmUserLogin.Size = new System.Drawing.Size(124, 69);
            this.imgFrmUserLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFrmUserLogin.TabIndex = 19;
            this.imgFrmUserLogin.TabStop = false;
            this.imgFrmUserLogin.Click += new System.EventHandler(this.imgFrmUserLogin_Click);
            // 
            // FrmForgotPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pcbUserPasswordShoworHide);
            this.Controls.Add(this.pcbUserPasswordUpdate);
            this.Controls.Add(this.pcbAddUserToken);
            this.Controls.Add(this.imgUserUpdate);
            this.Controls.Add(this.imgFrmUserLogin);
            this.Controls.Add(this.txtUserToken);
            this.Controls.Add(this.lblUserToken);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.lblUserpassword);
            this.Controls.Add(this.txtUserEmail);
            this.Controls.Add(this.lblUserMail);
            this.Name = "FrmForgotPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Şifremi Unuttum";
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddUserToken)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgUserUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserLogin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserEmail;
        private System.Windows.Forms.Label lblUserMail;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label lblUserpassword;
        private System.Windows.Forms.TextBox txtUserToken;
        private System.Windows.Forms.Label lblUserToken;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.PictureBox imgFrmUserLogin;
        private System.Windows.Forms.PictureBox imgUserUpdate;
        private System.Windows.Forms.PictureBox pcbAddUserToken;
        private System.Windows.Forms.PictureBox pcbUserPasswordUpdate;
        private System.Windows.Forms.PictureBox pcbUserPasswordShoworHide;
    }
}