namespace FrmLogin
{
    partial class FrmLogin
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.txtUserPhoneNumber = new System.Windows.Forms.TextBox();
            this.txtUserPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lnkUserCreate = new System.Windows.Forms.LinkLabel();
            this.lnkUserUpdate = new System.Windows.Forms.LinkLabel();
            this.login_label = new System.Windows.Forms.Label();
            this.pcbUserUpdate = new System.Windows.Forms.PictureBox();
            this.pcbUserCreate = new System.Windows.Forms.PictureBox();
            this.pcbLogin = new System.Windows.Forms.PictureBox();
            this.pcbUserPasswordShoworHide = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserCreate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtUserPhoneNumber
            // 
            this.txtUserPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserPhoneNumber.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserPhoneNumber.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtUserPhoneNumber.Location = new System.Drawing.Point(228, 208);
            this.txtUserPhoneNumber.MaxLength = 10;
            this.txtUserPhoneNumber.Name = "txtUserPhoneNumber";
            this.txtUserPhoneNumber.Size = new System.Drawing.Size(319, 33);
            this.txtUserPhoneNumber.TabIndex = 2;
            this.txtUserPhoneNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserPhoneNumber_KeyPress);
            // 
            // txtUserPassword
            // 
            this.txtUserPassword.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUserPassword.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtUserPassword.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtUserPassword.Location = new System.Drawing.Point(228, 253);
            this.txtUserPassword.MaxLength = 100;
            this.txtUserPassword.Name = "txtUserPassword";
            this.txtUserPassword.Size = new System.Drawing.Size(319, 33);
            this.txtUserPassword.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.label1.Location = new System.Drawing.Point(45, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 26);
            this.label1.TabIndex = 4;
            this.label1.Text = "Telefon Numaranız:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.label2.Location = new System.Drawing.Point(142, 256);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 26);
            this.label2.TabIndex = 5;
            this.label2.Text = "Şifreniz:";
            // 
            // lnkUserCreate
            // 
            this.lnkUserCreate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkUserCreate.AutoSize = true;
            this.lnkUserCreate.Font = new System.Drawing.Font("Adobe Caslon Pro", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUserCreate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lnkUserCreate.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lnkUserCreate.Location = new System.Drawing.Point(333, 314);
            this.lnkUserCreate.Name = "lnkUserCreate";
            this.lnkUserCreate.Size = new System.Drawing.Size(167, 36);
            this.lnkUserCreate.TabIndex = 27;
            this.lnkUserCreate.TabStop = true;
            this.lnkUserCreate.Text = "Yeni hesap olustur";
            this.lnkUserCreate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserCreate_LinkClicked);
            // 
            // lnkUserUpdate
            // 
            this.lnkUserUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lnkUserUpdate.AutoSize = true;
            this.lnkUserUpdate.Font = new System.Drawing.Font("Adobe Caslon Pro", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkUserUpdate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lnkUserUpdate.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(183)))), ((int)(((byte)(196)))));
            this.lnkUserUpdate.Location = new System.Drawing.Point(333, 371);
            this.lnkUserUpdate.Name = "lnkUserUpdate";
            this.lnkUserUpdate.Size = new System.Drawing.Size(159, 36);
            this.lnkUserUpdate.TabIndex = 28;
            this.lnkUserUpdate.TabStop = true;
            this.lnkUserUpdate.Text = "Sifremi unuttum";
            this.lnkUserUpdate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkUserUpdate_LinkClicked);
            // 
            // login_label
            // 
            this.login_label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.login_label.AutoSize = true;
            this.login_label.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.login_label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(162)))), ((int)(((byte)(97)))));
            this.login_label.Location = new System.Drawing.Point(353, 179);
            this.login_label.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.login_label.Name = "login_label";
            this.login_label.Size = new System.Drawing.Size(68, 26);
            this.login_label.TabIndex = 29;
            this.login_label.Text = "LOGİN";
            // 
            // pcbUserUpdate
            // 
            this.pcbUserUpdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserUpdate.Image = global::FrmLogin.Properties.Resources.haydopassword;
            this.pcbUserUpdate.Location = new System.Drawing.Point(266, 361);
            this.pcbUserUpdate.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbUserUpdate.Name = "pcbUserUpdate";
            this.pcbUserUpdate.Size = new System.Drawing.Size(58, 57);
            this.pcbUserUpdate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserUpdate.TabIndex = 26;
            this.pcbUserUpdate.TabStop = false;
            this.pcbUserUpdate.Click += new System.EventHandler(this.pcbUserUpdate_Click);
            // 
            // pcbUserCreate
            // 
            this.pcbUserCreate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserCreate.Image = global::FrmLogin.Properties.Resources.haydoAdd;
            this.pcbUserCreate.Location = new System.Drawing.Point(266, 297);
            this.pcbUserCreate.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbUserCreate.Name = "pcbUserCreate";
            this.pcbUserCreate.Size = new System.Drawing.Size(58, 57);
            this.pcbUserCreate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserCreate.TabIndex = 25;
            this.pcbUserCreate.TabStop = false;
            this.pcbUserCreate.Click += new System.EventHandler(this.pcbUserCreate_Click);
            // 
            // pcbLogin
            // 
            this.pcbLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbLogin.Image = global::FrmLogin.Properties.Resources.haydodoor;
            this.pcbLogin.Location = new System.Drawing.Point(639, 329);
            this.pcbLogin.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbLogin.Name = "pcbLogin";
            this.pcbLogin.Size = new System.Drawing.Size(146, 115);
            this.pcbLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbLogin.TabIndex = 24;
            this.pcbLogin.TabStop = false;
            this.pcbLogin.Click += new System.EventHandler(this.pcbLogin_Click);
            // 
            // pcbUserPasswordShoworHide
            // 
            this.pcbUserPasswordShoworHide.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pcbUserPasswordShoworHide.Image = global::FrmLogin.Properties.Resources.haydoShow;
            this.pcbUserPasswordShoworHide.Location = new System.Drawing.Point(553, 254);
            this.pcbUserPasswordShoworHide.Name = "pcbUserPasswordShoworHide";
            this.pcbUserPasswordShoworHide.Size = new System.Drawing.Size(53, 32);
            this.pcbUserPasswordShoworHide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserPasswordShoworHide.TabIndex = 23;
            this.pcbUserPasswordShoworHide.TabStop = false;
            this.pcbUserPasswordShoworHide.Click += new System.EventHandler(this.pcbUserPasswordShoworHide_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::FrmLogin.Properties.Resources.haydoo;
            this.pictureBox1.Location = new System.Drawing.Point(228, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(392, 164);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.login_label);
            this.Controls.Add(this.lnkUserUpdate);
            this.Controls.Add(this.lnkUserCreate);
            this.Controls.Add(this.pcbUserUpdate);
            this.Controls.Add(this.pcbUserCreate);
            this.Controls.Add(this.pcbLogin);
            this.Controls.Add(this.pcbUserPasswordShoworHide);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUserPassword);
            this.Controls.Add(this.txtUserPhoneNumber);
            this.Enabled = false;
            this.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Giris Sayfası";
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserCreate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbLogin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserPasswordShoworHide)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUserPhoneNumber;
        private System.Windows.Forms.TextBox txtUserPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pcbUserPasswordShoworHide;
        private System.Windows.Forms.PictureBox pcbLogin;
        private System.Windows.Forms.PictureBox pcbUserCreate;
        private System.Windows.Forms.PictureBox pcbUserUpdate;
        private System.Windows.Forms.LinkLabel lnkUserCreate;
        private System.Windows.Forms.LinkLabel lnkUserUpdate;
        private System.Windows.Forms.Label login_label;
    }
}

