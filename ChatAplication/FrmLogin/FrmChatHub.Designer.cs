namespace FrmLogin
{
    partial class FrmChatHub
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmChatHub));
            this.lblFriendOrGroupName = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlEmoji = new System.Windows.Forms.FlowLayoutPanel();
            this.lbl_timer = new System.Windows.Forms.Label();
            this.txtAddMessage = new System.Windows.Forms.TextBox();
            this.ofdData = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.pcbRealTimeSound = new System.Windows.Forms.PictureBox();
            this.pcbScrenShareAdd = new System.Windows.Forms.PictureBox();
            this.emoji_img = new System.Windows.Forms.PictureBox();
            this.pcbAddImage = new System.Windows.Forms.PictureBox();
            this.pcbAddWinrar = new System.Windows.Forms.PictureBox();
            this.pcbSoundAdd = new System.Windows.Forms.PictureBox();
            this.pcbAddMovie = new System.Windows.Forms.PictureBox();
            this.pcbAddMessage = new System.Windows.Forms.PictureBox();
            this.pcbFriendOrGroupImage = new System.Windows.Forms.PictureBox();
            this.imgFrmUserDashboard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pcbRealTimeSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbScrenShareAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddWinrar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSoundAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddMovie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFriendOrGroupImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserDashboard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFriendOrGroupName
            // 
            this.lblFriendOrGroupName.AutoSize = true;
            this.lblFriendOrGroupName.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblFriendOrGroupName.Location = new System.Drawing.Point(846, 101);
            this.lblFriendOrGroupName.Name = "lblFriendOrGroupName";
            this.lblFriendOrGroupName.Size = new System.Drawing.Size(36, 26);
            this.lblFriendOrGroupName.TabIndex = 36;
            this.lblFriendOrGroupName.Text = "Ad";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTime.Location = new System.Drawing.Point(351, 9);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(54, 26);
            this.lblTime.TabIndex = 37;
            this.lblTime.Text = "-------";
            // 
            // pnlEmoji
            // 
            this.pnlEmoji.AutoScroll = true;
            this.pnlEmoji.Location = new System.Drawing.Point(0, 597);
            this.pnlEmoji.Name = "pnlEmoji";
            this.pnlEmoji.Size = new System.Drawing.Size(749, 95);
            this.pnlEmoji.TabIndex = 53;
            this.pnlEmoji.Visible = false;
            // 
            // lbl_timer
            // 
            this.lbl_timer.AutoSize = true;
            this.lbl_timer.Location = new System.Drawing.Point(710, 581);
            this.lbl_timer.Name = "lbl_timer";
            this.lbl_timer.Size = new System.Drawing.Size(28, 13);
            this.lbl_timer.TabIndex = 50;
            this.lbl_timer.Text = "0.00";
            this.lbl_timer.Visible = false;
            // 
            // txtAddMessage
            // 
            this.txtAddMessage.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAddMessage.Location = new System.Drawing.Point(0, 501);
            this.txtAddMessage.MaxLength = 250;
            this.txtAddMessage.Multiline = true;
            this.txtAddMessage.Name = "txtAddMessage";
            this.txtAddMessage.Size = new System.Drawing.Size(749, 81);
            this.txtAddMessage.TabIndex = 46;
            // 
            // ofdData
            // 
            this.ofdData.FileName = "ofdData";
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(624, 46);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 23);
            this.button1.TabIndex = 55;
            this.button1.Text = "ekran paylasanı izle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(677, 75);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 57;
            this.button2.Text = "mute";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pcbRealTimeSound
            // 
            this.pcbRealTimeSound.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pcbRealTimeSound.Image = global::FrmLogin.Properties.Resources.Phonecall;
            this.pcbRealTimeSound.Location = new System.Drawing.Point(425, 550);
            this.pcbRealTimeSound.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbRealTimeSound.Name = "pcbRealTimeSound";
            this.pcbRealTimeSound.Size = new System.Drawing.Size(36, 32);
            this.pcbRealTimeSound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbRealTimeSound.TabIndex = 56;
            this.pcbRealTimeSound.TabStop = false;
            this.pcbRealTimeSound.Click += new System.EventHandler(this.pcbRealTimeSound_Click);
            // 
            // pcbScrenShareAdd
            // 
            this.pcbScrenShareAdd.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pcbScrenShareAdd.Image = global::FrmLogin.Properties.Resources.screenShare;
            this.pcbScrenShareAdd.Location = new System.Drawing.Point(473, 550);
            this.pcbScrenShareAdd.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbScrenShareAdd.Name = "pcbScrenShareAdd";
            this.pcbScrenShareAdd.Size = new System.Drawing.Size(36, 32);
            this.pcbScrenShareAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbScrenShareAdd.TabIndex = 54;
            this.pcbScrenShareAdd.TabStop = false;
            this.pcbScrenShareAdd.Click += new System.EventHandler(this.pcbScrenShareAdd_Click);
            // 
            // emoji_img
            // 
            this.emoji_img.BackColor = System.Drawing.SystemColors.ControlDark;
            this.emoji_img.Image = ((System.Drawing.Image)(resources.GetObject("emoji_img.Image")));
            this.emoji_img.Location = new System.Drawing.Point(521, 550);
            this.emoji_img.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.emoji_img.Name = "emoji_img";
            this.emoji_img.Size = new System.Drawing.Size(36, 32);
            this.emoji_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.emoji_img.TabIndex = 52;
            this.emoji_img.TabStop = false;
            this.emoji_img.Click += new System.EventHandler(this.emoji_img_Click);
            // 
            // pcbAddImage
            // 
            this.pcbAddImage.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pcbAddImage.Image = ((System.Drawing.Image)(resources.GetObject("pcbAddImage.Image")));
            this.pcbAddImage.Location = new System.Drawing.Point(569, 550);
            this.pcbAddImage.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbAddImage.Name = "pcbAddImage";
            this.pcbAddImage.Size = new System.Drawing.Size(36, 32);
            this.pcbAddImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAddImage.TabIndex = 51;
            this.pcbAddImage.TabStop = false;
            this.pcbAddImage.Click += new System.EventHandler(this.pcbAddImage_Click);
            // 
            // pcbAddWinrar
            // 
            this.pcbAddWinrar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pcbAddWinrar.Image = ((System.Drawing.Image)(resources.GetObject("pcbAddWinrar.Image")));
            this.pcbAddWinrar.Location = new System.Drawing.Point(617, 550);
            this.pcbAddWinrar.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbAddWinrar.Name = "pcbAddWinrar";
            this.pcbAddWinrar.Size = new System.Drawing.Size(36, 32);
            this.pcbAddWinrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAddWinrar.TabIndex = 49;
            this.pcbAddWinrar.TabStop = false;
            this.pcbAddWinrar.Click += new System.EventHandler(this.pcbAddWinrar_Click);
            // 
            // pcbSoundAdd
            // 
            this.pcbSoundAdd.Image = global::FrmLogin.Properties.Resources.mikrofon;
            this.pcbSoundAdd.Location = new System.Drawing.Point(710, 550);
            this.pcbSoundAdd.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbSoundAdd.Name = "pcbSoundAdd";
            this.pcbSoundAdd.Size = new System.Drawing.Size(36, 32);
            this.pcbSoundAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbSoundAdd.TabIndex = 48;
            this.pcbSoundAdd.TabStop = false;
            this.pcbSoundAdd.Click += new System.EventHandler(this.pcbSoundAdd_Click);
            // 
            // pcbAddMovie
            // 
            this.pcbAddMovie.Image = ((System.Drawing.Image)(resources.GetObject("pcbAddMovie.Image")));
            this.pcbAddMovie.Location = new System.Drawing.Point(665, 550);
            this.pcbAddMovie.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbAddMovie.Name = "pcbAddMovie";
            this.pcbAddMovie.Size = new System.Drawing.Size(36, 32);
            this.pcbAddMovie.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAddMovie.TabIndex = 47;
            this.pcbAddMovie.TabStop = false;
            this.pcbAddMovie.Click += new System.EventHandler(this.pcbAddMovie_Click);
            // 
            // pcbAddMessage
            // 
            this.pcbAddMessage.Image = ((System.Drawing.Image)(resources.GetObject("pcbAddMessage.Image")));
            this.pcbAddMessage.Location = new System.Drawing.Point(758, 501);
            this.pcbAddMessage.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.pcbAddMessage.Name = "pcbAddMessage";
            this.pcbAddMessage.Size = new System.Drawing.Size(193, 81);
            this.pcbAddMessage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbAddMessage.TabIndex = 45;
            this.pcbAddMessage.TabStop = false;
            this.pcbAddMessage.Click += new System.EventHandler(this.pcbAddMessage_Click);
            // 
            // pcbFriendOrGroupImage
            // 
            this.pcbFriendOrGroupImage.Location = new System.Drawing.Point(758, 0);
            this.pcbFriendOrGroupImage.Name = "pcbFriendOrGroupImage";
            this.pcbFriendOrGroupImage.Size = new System.Drawing.Size(193, 98);
            this.pcbFriendOrGroupImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pcbFriendOrGroupImage.TabIndex = 35;
            this.pcbFriendOrGroupImage.TabStop = false;
            // 
            // imgFrmUserDashboard
            // 
            this.imgFrmUserDashboard.Image = ((System.Drawing.Image)(resources.GetObject("imgFrmUserDashboard.Image")));
            this.imgFrmUserDashboard.Location = new System.Drawing.Point(0, 0);
            this.imgFrmUserDashboard.Name = "imgFrmUserDashboard";
            this.imgFrmUserDashboard.Size = new System.Drawing.Size(124, 69);
            this.imgFrmUserDashboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFrmUserDashboard.TabIndex = 33;
            this.imgFrmUserDashboard.TabStop = false;
            this.imgFrmUserDashboard.Click += new System.EventHandler(this.imgFrmUserDashboard_Click);
            // 
            // FrmChatHub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(953, 701);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.pcbRealTimeSound);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pcbScrenShareAdd);
            this.Controls.Add(this.lbl_timer);
            this.Controls.Add(this.pnlEmoji);
            this.Controls.Add(this.emoji_img);
            this.Controls.Add(this.pcbAddImage);
            this.Controls.Add(this.pcbAddWinrar);
            this.Controls.Add(this.pcbSoundAdd);
            this.Controls.Add(this.pcbAddMovie);
            this.Controls.Add(this.txtAddMessage);
            this.Controls.Add(this.pcbAddMessage);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblFriendOrGroupName);
            this.Controls.Add(this.pcbFriendOrGroupImage);
            this.Controls.Add(this.imgFrmUserDashboard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmChatHub";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatHub";
            this.Load += new System.EventHandler(this.FrmChatHub_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pcbRealTimeSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbScrenShareAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emoji_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddWinrar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbSoundAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddMovie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbAddMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcbFriendOrGroupImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imgFrmUserDashboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imgFrmUserDashboard;
        private System.Windows.Forms.PictureBox pcbFriendOrGroupImage;
        private System.Windows.Forms.Label lblFriendOrGroupName;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.FlowLayoutPanel pnlEmoji;
        private System.Windows.Forms.PictureBox emoji_img;
        private System.Windows.Forms.PictureBox pcbAddImage;
        private System.Windows.Forms.Label lbl_timer;
        private System.Windows.Forms.PictureBox pcbAddWinrar;
        private System.Windows.Forms.PictureBox pcbSoundAdd;
        private System.Windows.Forms.PictureBox pcbAddMovie;
        private System.Windows.Forms.TextBox txtAddMessage;
        private System.Windows.Forms.PictureBox pcbAddMessage;
        private System.Windows.Forms.OpenFileDialog ofdData;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pcbScrenShareAdd;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pcbRealTimeSound;
        private System.Windows.Forms.Button button2;
    }
}