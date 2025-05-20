namespace FrmLogin
{
    partial class FrmUserDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserDashboard));
            this.lblUserName = new System.Windows.Forms.Label();
            this.pnlFriendList = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFriendPhoneNumber = new System.Windows.Forms.TextBox();
            this.pcbUserFriendAdd = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pcbFriendSearch = new System.Windows.Forms.PictureBox();
            this.txtFindUserFriendsName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pcbUserImage = new System.Windows.Forms.PictureBox();
            this.pcbFrmUpdateUser = new System.Windows.Forms.PictureBox();
            this.pcbFrmLogin = new System.Windows.Forms.PictureBox();
            this.pnlFriendList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserFriendAdd)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFriendSearch)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrmUpdateUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrmLogin)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUserName.Location = new System.Drawing.Point(109, 105);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(36, 26);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Ad";
            // 
            // pnlFriendList
            // 
            this.pnlFriendList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.pnlFriendList.Controls.Add(this.label1);
            this.pnlFriendList.Controls.Add(this.txtFriendPhoneNumber);
            this.pnlFriendList.Controls.Add(this.pcbUserFriendAdd);
            this.pnlFriendList.Location = new System.Drawing.Point(0, 0);
            this.pnlFriendList.Name = "pnlFriendList";
            this.pnlFriendList.Size = new System.Drawing.Size(253, 78);
            this.pnlFriendList.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(84, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 26);
            this.label1.TabIndex = 5;
            this.label1.Text = "Telefon Numarası:";
            // 
            // txtFriendPhoneNumber
            // 
            this.txtFriendPhoneNumber.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtFriendPhoneNumber.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFriendPhoneNumber.Location = new System.Drawing.Point(89, 38);
            this.txtFriendPhoneNumber.MaxLength = 10;
            this.txtFriendPhoneNumber.Name = "txtFriendPhoneNumber";
            this.txtFriendPhoneNumber.Size = new System.Drawing.Size(157, 33);
            this.txtFriendPhoneNumber.TabIndex = 5;
            this.txtFriendPhoneNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFriendPhoneNumber_KeyPress);
            // 
            // pcbUserFriendAdd
            // 
            this.pcbUserFriendAdd.Image = global::FrmLogin.Properties.Resources.haydoAdd;
            this.pcbUserFriendAdd.Location = new System.Drawing.Point(-1, 1);
            this.pcbUserFriendAdd.Name = "pcbUserFriendAdd";
            this.pcbUserFriendAdd.Size = new System.Drawing.Size(84, 72);
            this.pcbUserFriendAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserFriendAdd.TabIndex = 5;
            this.pcbUserFriendAdd.TabStop = false;
            this.pcbUserFriendAdd.Click += new System.EventHandler(this.pcbUserFriendAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel1.Controls.Add(this.pcbFriendSearch);
            this.panel1.Controls.Add(this.txtFindUserFriendsName);
            this.panel1.Location = new System.Drawing.Point(0, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 39);
            this.panel1.TabIndex = 8;
            // 
            // pcbFriendSearch
            // 
            this.pcbFriendSearch.Image = global::FrmLogin.Properties.Resources.KullaniciAra;
            this.pcbFriendSearch.Location = new System.Drawing.Point(198, 3);
            this.pcbFriendSearch.Name = "pcbFriendSearch";
            this.pcbFriendSearch.Size = new System.Drawing.Size(52, 33);
            this.pcbFriendSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbFriendSearch.TabIndex = 6;
            this.pcbFriendSearch.TabStop = false;
            // 
            // txtFindUserFriendsName
            // 
            this.txtFindUserFriendsName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtFindUserFriendsName.Location = new System.Drawing.Point(3, 3);
            this.txtFindUserFriendsName.MaxLength = 10;
            this.txtFindUserFriendsName.Name = "txtFindUserFriendsName";
            this.txtFindUserFriendsName.Size = new System.Drawing.Size(189, 33);
            this.txtFindUserFriendsName.TabIndex = 6;
            this.txtFindUserFriendsName.Text = "Kullanici Ara";
            this.txtFindUserFriendsName.Click += new System.EventHandler(this.txtFindUserFriendsName_Click);
            this.txtFindUserFriendsName.TextChanged += new System.EventHandler(this.txtFindUserFriendsName_TextChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.panel2.Controls.Add(this.pcbUserImage);
            this.panel2.Controls.Add(this.pcbFrmUpdateUser);
            this.panel2.Controls.Add(this.lblUserName);
            this.panel2.Location = new System.Drawing.Point(360, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 135);
            this.panel2.TabIndex = 9;
            // 
            // pcbUserImage
            // 
            this.pcbUserImage.Location = new System.Drawing.Point(37, 1);
            this.pcbUserImage.Name = "pcbUserImage";
            this.pcbUserImage.Size = new System.Drawing.Size(193, 98);
            this.pcbUserImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbUserImage.TabIndex = 3;
            this.pcbUserImage.TabStop = false;
            // 
            // pcbFrmUpdateUser
            // 
            this.pcbFrmUpdateUser.Image = global::FrmLogin.Properties.Resources.isim_degis;
            this.pcbFrmUpdateUser.Location = new System.Drawing.Point(37, 105);
            this.pcbFrmUpdateUser.Name = "pcbFrmUpdateUser";
            this.pcbFrmUpdateUser.Size = new System.Drawing.Size(42, 26);
            this.pcbFrmUpdateUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbFrmUpdateUser.TabIndex = 6;
            this.pcbFrmUpdateUser.TabStop = false;
            this.pcbFrmUpdateUser.Click += new System.EventHandler(this.pcbFrmUpdateUser_Click);
            // 
            // pcbFrmLogin
            // 
            this.pcbFrmLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pcbFrmLogin.Image = global::FrmLogin.Properties.Resources.haydoQuit;
            this.pcbFrmLogin.Location = new System.Drawing.Point(706, 1);
            this.pcbFrmLogin.Name = "pcbFrmLogin";
            this.pcbFrmLogin.Size = new System.Drawing.Size(94, 77);
            this.pcbFrmLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbFrmLogin.TabIndex = 7;
            this.pcbFrmLogin.TabStop = false;
            this.pcbFrmLogin.Click += new System.EventHandler(this.pcbFrmLogin_Click);
            // 
            // FrmUserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pcbFrmLogin);
            this.Controls.Add(this.pnlFriendList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmUserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AnaSayfam";
            this.Load += new System.EventHandler(this.FrmUserDashboard_Load);
            this.pnlFriendList.ResumeLayout(false);
            this.pnlFriendList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserFriendAdd)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFriendSearch)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pcbUserImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrmUpdateUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFrmLogin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.PictureBox pcbUserImage;
        private System.Windows.Forms.Panel pnlFriendList;
        private System.Windows.Forms.PictureBox pcbUserFriendAdd;
        private System.Windows.Forms.TextBox txtFriendPhoneNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pcbFrmUpdateUser;
        private System.Windows.Forms.PictureBox pcbFrmLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFindUserFriendsName;
        private System.Windows.Forms.PictureBox pcbFriendSearch;
        private System.Windows.Forms.Panel panel2;
    }
}