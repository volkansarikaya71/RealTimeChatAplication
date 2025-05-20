namespace FrmLogin
{
    partial class FrmCreateUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCreateUser));
            this.lblUserName = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserPhoneNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ofAddUserImage = new System.Windows.Forms.OpenFileDialog();
            this.pcbUserPasswordShoworHide = new System.Windows.Forms.PictureBox();
            this.pcbCreateUser = new System.Windows.Forms.PictureBox();
            this.imgFrmUserLogin = new System.Windows.Forms.PictureBox();
            this.pxbUserAddImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreateUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbUserAddImage)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lblUserName.Location = new System.Drawing.Point(112, 194);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(156, 26);
            this.lblUserName.TabIndex = 1;
            this.lblUserName.Text = "Ad ve Soyadınız :";
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserName.Location = new System.Drawing.Point(270, 191);
            this.txtUserName.MaxLength = 80;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(318, 33);
            this.txtUserName.TabIndex = 2;
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserPassword.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserPassword.Location = new System.Drawing.Point(270, 230);
            this.txtUserPassword.MaxLength = 100;
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(318, 33);
            this.txtUserPassword.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(188, 233);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 26);
            this.label1.TabIndex = 6;
            this.label1.Text = "Şifreniz:";
            // 
            // txtUserEmail
            // 
            this.txtUserEmail.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserEmail.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserEmail.Location = new System.Drawing.Point(270, 269);
            this.txtUserEmail.MaxLength = 255;
            this.txtUserEmail.Name = "txtUserEmail";
            this.txtUserEmail.Size = new System.Drawing.Size(318, 33);
            this.txtUserEmail.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.label2.Location = new System.Drawing.Point(121, 272);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 26);
            this.label2.TabIndex = 8;
            this.label2.Text = "Email Adresiniz:";
            // 
            // txtUserPhoneNumber
            // 
            this.txtUserPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserPhoneNumber.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserPhoneNumber.Location = new System.Drawing.Point(270, 308);
            this.txtUserPhoneNumber.MaxLength = 10;
            this.txtUserPhoneNumber.Name = "txtUserPhoneNumber";
            this.txtUserPhoneNumber.Size = new System.Drawing.Size(318, 33);
            this.txtUserPhoneNumber.TabIndex = 11;
            this.txtUserPhoneNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserPhoneNumber_KeyPress);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.label3.Location = new System.Drawing.Point(91, 311);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 26);
            this.label3.TabIndex = 10;
            this.label3.Text = "Telefon Numaranız:";
            // 
            // ofAddUserImage
            // 
            this.ofAddUserImage.FileName = "openFileDialog1";
            // 
            // pcbUserPasswordShoworHide
            // 
            this.pcbUserPasswordShoworHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserPasswordShoworHide.Image = global::FrmLogin.Properties.Resources.gizle;
            this.pcbUserPasswordShoworHide.Location = new System.Drawing.Point(594, 233);
            this.pcbUserPasswordShoworHide.Name = "pcbUserPasswordShoworHide";
            this.pcbUserPasswordShoworHide.Size = new System.Drawing.Size(53, 32);
            this.pcbUserPasswordShoworHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserPasswordShoworHide.TabIndex = 22;
            this.pcbUserPasswordShoworHide.TabStop = false;
            this.pcbUserPasswordShoworHide.Click += new System.EventHandler(this.pcbUserPasswordShoworHide_Click);
            // 
            // pcbCreateUser
            // 
            this.pcbCreateUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbCreateUser.Image = global::FrmLogin.Properties.Resources.save1;
            this.pcbCreateUser.Location = new System.Drawing.Point(596, 347);
            this.pcbCreateUser.Name = "pcbCreateUser";
            this.pcbCreateUser.Size = new System.Drawing.Size(124, 91);
            this.pcbCreateUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbCreateUser.TabIndex = 21;
            this.pcbCreateUser.TabStop = false;
            this.pcbCreateUser.Click += new System.EventHandler(this.pcbCreateUser_Click);
            // 
            // imgFrmUserLogin
            // 
            this.imgFrmUserLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.imgFrmUserLogin.Image = global::FrmLogin.Properties.Resources.geri;
            this.imgFrmUserLogin.Location = new System.Drawing.Point(12, 12);
            this.imgFrmUserLogin.Name = "imgFrmUserLogin";
            this.imgFrmUserLogin.Size = new System.Drawing.Size(124, 69);
            this.imgFrmUserLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFrmUserLogin.TabIndex = 20;
            this.imgFrmUserLogin.TabStop = false;
            this.imgFrmUserLogin.Click += new System.EventHandler(this.imgFrmUserLogin_Click);
            // 
            // pxbUserAddImage
            // 
            this.pxbUserAddImage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pxbUserAddImage.Image = global::FrmLogin.Properties.Resources.resim_ekle;
            this.pxbUserAddImage.Location = new System.Drawing.Point(270, 12);
            this.pxbUserAddImage.Name = "pxbUserAddImage";
            this.pxbUserAddImage.Size = new System.Drawing.Size(318, 165);
            this.pxbUserAddImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pxbUserAddImage.TabIndex = 0;
            this.pxbUserAddImage.TabStop = false;
            this.pxbUserAddImage.Click += new System.EventHandler(this.pxbUserAddImage_Click);
            // 
            // FrmCreateUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pcbUserPasswordShoworHide);
            this.Controls.Add(this.pcbCreateUser);
            this.Controls.Add(this.imgFrmUserLogin);
            this.Controls.Add(this.txtUserPhoneNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.pxbUserAddImage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmCreateUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Yeni Üyelik ";
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbCreateUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pxbUserAddImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pxbUserAddImage;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUserEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserPhoneNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog ofAddUserImage;
        private System.Windows.Forms.PictureBox imgFrmUserLogin;
        private System.Windows.Forms.PictureBox pcbCreateUser;
        private System.Windows.Forms.PictureBox pcbUserPasswordShoworHide;
    }
}