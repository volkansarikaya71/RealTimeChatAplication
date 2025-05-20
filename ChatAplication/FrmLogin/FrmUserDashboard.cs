using BusinessLayer.Concrete;
using BusinessLayer.Models;
using ChatApi.BusinessLayer.Concrete;
using ChatApi.DAL;
using FrmLogin.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FrmLogin
{
    public partial class FrmUserDashboard : Form
    {
        public FrmUserDashboard()
        {
            InitializeComponent();
            this.FormClosing += async (sender, e) => await LogOut.FormClosing(sender, e);
        }

        UserManager _userManager = new UserManager();
        UserFriendManager _userFriendManager = new UserFriendManager();
        private Panel userpanel, Motherpanel,MessageListpanel, friendMessagePanel;
        private List<ListUserFriendViewModel> allFriends = new List<ListUserFriendViewModel>();
        private List<ChatMessageCountAndAbout> allFriendsMessage = new List<ChatMessageCountAndAbout>();
        private ContextMenuStrip contextMenuStrip;
        public static string chatId,userName;
        public int lastlocation=0;

        #region formload anaforum
        private async void FrmUserDashboard_Load(object sender, EventArgs e)
        {
            var values = _userManager.TGetByToken(FrmLogin.userInformation);
            lblUserName.Text = values.Data.UserName;
            pcbUserImage.ImageLocation = await ImageDowload(values.Data.UserImage,values.Data.PhoneNumber);

            var friendlist = new ListUserFriendViewModel
            {
                UserId = values.Data.UserId,
                Token = FrmLogin.userInformation,
            };

            #region Gelen mesajlar dinleme
            FrmLogin._connection.On<string, int, string, int>("ReceiveMessage", (message, senderUserId, type, messageId) =>
            {
                // Mesajı alıp ekranda göstermek için
                Invoke(new Action(() =>
                {
                    DisplayMessage(message, senderUserId, type, messageId);
                }));
            });
            #endregion

            FriendList("");
            MesajControl();
            #region Giriş yapanları dinleme(Online durumu)
            FrmLogin._connection.On<string>("MessageFromLogin", (message) =>
            {
                int loggedInUserId = int.Parse(message);
                var user = allFriends.FirstOrDefault(f => f.UserId == loggedInUserId);

                if (user != null)
                {
                    user.UserStatus = true;

                    allFriends = allFriends.OrderBy(f => f.UserStatus == false)
                               .ThenBy(f => f.UserName)
                               .ToList();

                    FriendList("");
                }

            });
            #endregion
            #region Çıkış yapanları dinleme (Ofline durumu)
            FrmLogin._connection.On<string>("MessageFromLogOut", (message) =>
            {
                int loggedInUserId = int.Parse(message);
                var user = allFriends.FirstOrDefault(f => f.UserId == loggedInUserId);

                if (user != null)
                {
                    user.UserStatus = false;

                    allFriends = allFriends.OrderBy(f => f.UserStatus == false)
                           .ThenBy(f => f.UserName)
                           .ToList();

                    FriendList("");
                }
            });
            #endregion
            #region diğer kullanıcıların profil değişiklerini dinleme (diğer bir kullanıcının adı,resmi vb değişkiliği durumunda)
            FrmLogin._connection.On<ListUserFriendViewModel>("MessageFromUserAboutUpdate", (message) =>
            {
                Invoke(new Action(() =>
                {
                    var existingFriend = allFriends.FirstOrDefault(f => f.UserId == message.UserId);

                    if (existingFriend != null)
                    {
                        existingFriend.UserName = message.UserName;
                        existingFriend.UserImage = message.UserImage;
                        existingFriend.UserStatus = message.UserStatus;
                        existingFriend.UserLastOnlineDate = message.UserLastOnlineDate;
                        FriendList("");
                    }

                }));
            });
            #endregion


        }
        #endregion

        #region bildirim ekrana bastırma
        public async void DisplayMessage(string message,int senderUserId, string type, int messageId)
        {
            bool panelFound = false;
            foreach (Control control in MessageListpanel.Controls)
            {
                // userpanel'i mesajId'ye göre bul
                if (control is Panel friendMessagePanel && (string)friendMessagePanel.Name == senderUserId.ToString())
                {
                    panelFound = true;
                    // userpanel içinde bulunan tüm PictureBox'ları bul ve ismine göre sil
                    foreach (var Label in friendMessagePanel.Controls.OfType<Label>())
                    {
                        if (Label.Name == "Count")  // İlgili PictureBox'ı isme göre bul
                        {
                            string[] textParts = Label.Text.Split(':');  // "6: Yeni Mesaj" gibi bir formatta ikiye ayır
                            if (textParts.Length > 0 && int.TryParse(textParts[0], out int count))
                            {
                                count++;  // Sayıyı bir artır
                                Label.Text = count.ToString() + ": Yeni Mesaj";  // Yeni değeri Label'a ata
                                FrmAnnouncement frmAnnouncement = new FrmAnnouncement();
                                frmAnnouncement.Show();
                            }
                            break; // İşlem tamamlandı, döngüyü sonlandır
                        }
                    }
                }
            }
            if(!panelFound)
            {
                LastPanelLocation();
                var Sendervalues = await _userManager.TGetByIdFriend(senderUserId, FrmLogin.userInformation);
                if(Sendervalues != null)
                { 
                MessagePanel(senderUserId);
                ImageShow(Sendervalues.Data.ChatImage, Sendervalues.Data.PhoneNumber);
                SenderName(Sendervalues.Data.ChatName);

                if (type == "Text")
                {
                    SenderMessageContext(message);
                }
                if (type == "Image")
                {
                    SenderMessageContext("Resim Dosyası Yolladı.");
                }
                if (type == "Winrar")
                {
                    SenderMessageContext("Wnrar Dosyası Yolladı.");
                }
                if (type == "Movie")
                {
                    SenderMessageContext("Video Dosyası Yolladı.");
                }
                if (type == "Sound")
                {
                    SenderMessageContext("Ses Kaydı Yolladı.");
                }
                    userName = Sendervalues.Data.ChatName;
                    FrmAnnouncement frmAnnouncement = new FrmAnnouncement();
                frmAnnouncement.Show();
                SenderMessageCount(1);
                SenderMessageDatetime(DateTime.Now);
                }

            }
            
        }
        #endregion




        #region update sayfası açıyor
        private void pcbFrmUpdateUser_Click(object sender, EventArgs e)
        {
            FrmUpdateUser frmUpdateUser = new FrmUpdateUser();
            frmUpdateUser.Show();
            this.Hide();
        }
        #endregion

        #region logout işlemi çıkış işlemi
        private async void pcbFrmLogin_Click(object sender, EventArgs e)
        {

            var values = await _userManager.LogOut(FrmLogin.userInformation);
            if (values)
            {
                string resultMessage = await Signalr.SignalrLogOut();

                // Mesajı kontrol ederek uygun işlemleri yap
                if (resultMessage == "Çıkış işlemi başarılı") // Sunucudan dönen başarılı mesaj
                {
                    MessageBox.Show(resultMessage);
                    FrmLogin frmLogin = new FrmLogin();
                    frmLogin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show(resultMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region arkadas ekleme kısmı 
        private async void pcbUserFriendAdd_Click(object sender, EventArgs e)
        {
            var Uservalues = _userManager.TGetByToken(FrmLogin.userInformation);

            AddUserFriendViewModel userFriendViewModel = new AddUserFriendViewModel();
            userFriendViewModel.FriendPhoneNumber = txtFriendPhoneNumber.Text;
            userFriendViewModel.UserId = Uservalues.Data.UserId;
            userFriendViewModel.DeleteStatus = false;
            userFriendViewModel.Token = FrmLogin.userInformation;

            var values = await _userFriendManager.TAdd(userFriendViewModel);
            if (values.Success)
            {
                var dialogResult = MessageBox.Show("Kullanıcı başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                allFriends.Clear();
                FriendList("");
            }
            else
            {
                MessageBox.Show(values.ErrorMessage);
            }
        }

        #endregion

        #region arkadas ekleme kısmı textbox sadece rakam girilme olayı
        private void txtFriendPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion

        public async void FriendList(string SearchName)
        {
            #region panel varsa silme yoksa olusturma
            if (Motherpanel != null)
            {
                // UI işlemleri Invoke ile yapılmalı
                if (Motherpanel.InvokeRequired)
                {
                    Motherpanel.Invoke(new Action(() =>
                    {
                        // Panelin içindeki tüm kontrolleri kaldırma işlemi
                        while (Motherpanel.Controls.Count > 0)
                        {
                            Control control = Motherpanel.Controls[0];
                            Motherpanel.Controls.Remove(control);
                            control.Dispose(); // Kontrolü bellekten serbest bırak
                        }

                        // Paneli formdan kaldırma ve dispose etme
                        this.Controls.Remove(Motherpanel);
                        Motherpanel.Dispose();
                        Motherpanel = null; // Referansı null'a ayarlıyoruz
                    }));
                }
                else
                {
                    // Eğer Invoke gerekmezse, doğrudan işlemi yapabilirsiniz
                    while (Motherpanel.Controls.Count > 0)
                    {
                        Control control = Motherpanel.Controls[0];
                        Motherpanel.Controls.Remove(control);
                        control.Dispose(); // Kontrolü bellekten serbest bırak
                    }

                    // Paneli formdan kaldırma ve dispose etme
                    this.Controls.Remove(Motherpanel);
                    Motherpanel.Dispose();
                    Motherpanel = null; // Referansı null'a ayarlıyoruz
                }
            }

            #endregion

            #region tokendan kullanıcı bilgilerini alma
            var values = _userManager.TGetByToken(FrmLogin.userInformation);
            var friendlist = new ListUserFriendViewModel
            {
                UserId = values.Data.UserId,
                Token = FrmLogin.userInformation,
            };
            #endregion

            #region veritabanından çekme işlemi
            if (allFriends.Count == 0)  // Eğer allFriends boşsa, veritabanından çek
            {
                allFriends = await _userFriendManager.TGetUserFriendsListWithUserId(friendlist);
            }
            #endregion

            #region Arama yapma kısmı
            List<ListUserFriendViewModel> friendsAbout = new List<ListUserFriendViewModel>();
            if (SearchName == "")
            {
                friendsAbout = allFriends;  // Eğer arama yapılmıyorsa, tüm arkadaşlar listesi
            }
            else
            {
                friendsAbout = allFriends
                    .Where(f => f.UserName.ToLower().StartsWith(SearchName.ToLower()))  // Adı arama terimiyle başlayan arkadaşları filtrele
                    .OrderBy(f => f.UserName)  // Sonuçları A-Z sıralama
                    .ToList();
            }
            #endregion

            #region Ana Panel
            Motherpanel = new Panel();
            Motherpanel.Size = new Size(253, 350);
            Motherpanel.Location = new Point(0, 115);
            Motherpanel.BorderStyle = BorderStyle.FixedSingle;
            Motherpanel.BackColor = Color.FromArgb(52, 73, 94);
            Motherpanel.BorderStyle = BorderStyle.None;
            Motherpanel.AutoScroll = true;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Controls.Add(Motherpanel); // UI işlemleri burada yapılır.
                }));
            }
            else
            {
                this.Controls.Add(Motherpanel); // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz.
            }
            #endregion

            foreach (var item in friendsAbout)
            {
                #region resmin bilgisayardaki konumu
                string Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + $"Data/{item.UserPhoneNumber}", Path.GetFileName(item.UserImage));
                #endregion

                #region sağ tık menüsü
                contextMenuStrip = new ContextMenuStrip();

                var ContextMenuDelete = new ToolStripMenuItem("Sil");
                ContextMenuDelete.Click += ContextMenuDelete_Click;

                var ContextMenuBlock = new ToolStripMenuItem("Engelle");
                ContextMenuBlock.Click += ContextMenuBlock_Click;

                contextMenuStrip.Items.Add(ContextMenuDelete);
                contextMenuStrip.Items.Add(ContextMenuBlock);

                #endregion

                #region kullanici yani user paneli
                userpanel = new Panel();
                if (Motherpanel.AutoScrollPosition.Y != 0)
                {
                    userpanel.Size = new Size(236, 50);  // Eğer scroll varsa, 236 yapıyoruz
                }
                else
                {
                    userpanel.Size = new Size(256, 50);  // Scroll yoksa 256 yapıyoruz
                }
                userpanel.Location = new Point(0, Motherpanel.Controls.Count * 52);
                userpanel.BorderStyle = BorderStyle.FixedSingle;
                userpanel.BorderStyle = BorderStyle.None;
                userpanel.MouseClick += userPanel_LeftClick;
                userpanel.MouseClick += UserPanel_RightClick;
                userpanel.Name = item.UserId.ToString();
                if (item.UserStatus)
                {

                    userpanel.BackColor = Color.Blue;
                }
                else
                {
                    userpanel.BackColor = Color.Red;
                }
                if (Motherpanel.InvokeRequired)
                {
                    Motherpanel.Invoke(new Action(() =>
                    {
                        Motherpanel.Controls.Add(userpanel);  // UI işlemi ana iş parçacığından yapılır
                    }));
                }
                else
                {
                    Motherpanel.Controls.Add(userpanel);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
                }
                #endregion

                #region kullanici resmi

                PictureBox pcbUserImage = new PictureBox();
                pcbUserImage.Size = new Size(70, 50);
                pcbUserImage.Location = new Point(0, 0);
                pcbUserImage.BorderStyle = BorderStyle.FixedSingle;
                pcbUserImage.SizeMode = PictureBoxSizeMode.StretchImage;
                pcbUserImage.BorderStyle = BorderStyle.None;
                pcbUserImage.ImageLocation = await ImageDowload(item.UserImage,item.UserPhoneNumber);
                pcbUserImage.MouseClick += userPanel_LeftClick;
                pcbUserImage.MouseClick += UserPanel_RightClick;
                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbUserImage);  // UI işlemi ana iş parçacığından yapılır
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbUserImage);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
                }
                #endregion

                #region kullanici ismi
                Label lblUserName = new Label();
                lblUserName.Text = item.UserName.Length > 10 ? item.UserName.Substring(0, 10) : item.UserName; 
                lblUserName.AutoSize = true;
                lblUserName.Location = new Point(70, 10);
                lblUserName.Font = new Font("Calibri", 16, FontStyle.Bold);
                lblUserName.MouseClick += userPanel_LeftClick;
                lblUserName.MouseClick += UserPanel_RightClick;
                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(lblUserName);  // UI işlemi ana iş parçacığından yapılır
                    }));
                }
                else
                {
                    userpanel.Controls.Add(lblUserName);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
                }
                #endregion

                #region kullanici bildirim sayısı
                PictureBox pcbUserNotification = new PictureBox();
                pcbUserNotification.Size = new Size(25, 25);
                pcbUserNotification.Location = new Point(200, 10);
                pcbUserNotification.BorderStyle = BorderStyle.FixedSingle;
                pcbUserNotification.SizeMode = PictureBoxSizeMode.StretchImage;
                pcbUserNotification.BorderStyle = BorderStyle.None;
                pcbUserNotification.Image = Properties.Resources.bildirim;
                //if (values.Data.UserStatus)
                //{


                //}
                //else
                //{
                //    pcbUserNotification.Image = Properties.Resources.bildirim;
                //}
                pcbUserNotification.MouseClick += userPanel_LeftClick;
                pcbUserNotification.MouseClick += UserPanel_RightClick;
                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbUserNotification);  // UI işlemi ana iş parçacığından yapılır
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbUserNotification);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
                }

                #endregion
            }
        }

        #region arkadas listesi panelinde sol tık olayı
        private void userPanel_LeftClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Sağ tık kontrolü
            {
                Control clickedControl = sender as Control;
                Panel parentPanel = clickedControl is Panel ? clickedControl as Panel : clickedControl.Parent as Panel;

                if (parentPanel != null)
                {
                    chatId = parentPanel.Name;
                    FrmChatHub chatHub = new FrmChatHub();
                    chatHub.Show();
                    this.Hide();
                }
            }
        }
        #endregion

        #region kullanıcı adına göre kullanıcıları listeleme
        private void txtFindUserFriendsName_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtFindUserFriendsName.Text.ToLower();
            FriendList(searchTerm);
        }
        #endregion

        #region arkadas listesi panelinde sağ tık olayı
        private void UserPanel_RightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) // Sağ tık kontrolü
            {
                Control clickedControl = sender as Control;
                Panel parentPanel = clickedControl is Panel ? clickedControl as Panel : clickedControl.Parent as Panel;

                if (parentPanel != null)
                {
                    contextMenuStrip.Tag = parentPanel; // Panel referansını menüye ata
                    contextMenuStrip.Show(Cursor.Position); // Menü göster
                }
            }

        }
        #endregion

        #region Resim var mı diye Kontrol Et Yoksa İndir
        public async Task<string> ImageDowload(string ImageLocation,string PhoneNumber)
        {

            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{PhoneNumber}");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + $"Data/{PhoneNumber}", Path.GetFileName(ImageLocation));
            if (File.Exists(Image))
            {
                return Image;
            }
            else
            {
                await FileZilla.dowloadData(ImageLocation, PhoneNumber);
            }
            return Image;

        }
        #endregion

        #region paneldelete işlemi sag tık yapınca silme işlemi yapma kısmı
        private async void ContextMenuDelete_Click(object sender, EventArgs e)
        {
            // Tıklanan menü öğesi "Sil" ise
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Owner is ContextMenuStrip menu)
            {
                var parentPanel = menu.Tag as Panel; // Sağ tıklanan paneli al
                if (parentPanel != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Arkadaşınızı silmek istediğinize emin misiniz?", "Arkadaş Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        var values = _userManager.TGetByToken(FrmLogin.userInformation);

                        var UserFriendViewModel = new AddUserFriendViewModel
                        {
                            UserId = values.Data.UserId,
                            UserFriendId = int.Parse(parentPanel.Name),
                            Token = FrmLogin.userInformation,
                        };

                        var delete = await _userFriendManager.TUpdate(UserFriendViewModel);
                        allFriends.Clear();
                        FriendList("");
                        MessageBox.Show($"Arkadaşınız silindi!");

                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        #region kullanici arama kısmına tıklandığı içeriği silme
        private void txtFindUserFriendsName_Click(object sender, EventArgs e)
        {
            if (txtFindUserFriendsName.Text == "Kullanici Ara")
            {
                txtFindUserFriendsName.Clear();
            }
        }


        #endregion

        #region panelblock işlemi sag tık yapınca kullanıcı engeleme işlemi yapma kısmı
        private void ContextMenuBlock_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Owner is ContextMenuStrip menu)
            {
                var parentPanel = menu.Tag as Panel; // Sağ tıklanan paneli al
                if (parentPanel != null)
                {
                    DialogResult dialogResult = MessageBox.Show("Arkadaşınızı Engellemek istediğinize emin misiniz?", "Arkadaş Engelleme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (dialogResult == DialogResult.Yes)
                    {
                        MessageBox.Show($"Arkadaşınız Engellendi! Panel ID: {parentPanel.Name}");
                        parentPanel.Parent.Controls.Remove(parentPanel);
                    }
                    else
                    {
                        MessageBox.Show("Engelleme işlemi iptal edildi.", "İptal", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        #endregion

        public async void MesajControl()
        {

            #region panel varsa silme yoksa olusturma
            if (MessageListpanel != null)
            {
                // UI işlemleri Invoke ile yapılmalı
                if (MessageListpanel.InvokeRequired)
                {
                    MessageListpanel.Invoke(new Action(() =>
                    {
                        // Panelin içindeki tüm kontrolleri kaldırma işlemi
                        while (MessageListpanel.Controls.Count > 0)
                        {
                            Control control = MessageListpanel.Controls[0];
                            MessageListpanel.Controls.Remove(control);
                            control.Dispose(); // Kontrolü bellekten serbest bırak
                        }

                        // Paneli formdan kaldırma ve dispose etme
                        this.Controls.Remove(MessageListpanel);
                        MessageListpanel.Dispose();
                        MessageListpanel = null; // Referansı null'a ayarlıyoruz
                    }));
                }
                else
                {
                    // Eğer Invoke gerekmezse, doğrudan işlemi yapabilirsiniz
                    while (MessageListpanel.Controls.Count > 0)
                    {
                        Control control = MessageListpanel.Controls[0];
                        MessageListpanel.Controls.Remove(control);
                        control.Dispose(); // Kontrolü bellekten serbest bırak
                    }

                    // Paneli formdan kaldırma ve dispose etme
                    this.Controls.Remove(MessageListpanel);
                    MessageListpanel.Dispose();
                    MessageListpanel = null; // Referansı null'a ayarlıyoruz
                }
            }

            #endregion

            #region Message Ana Panel
            MessageListpanel = new Panel();
            MessageListpanel.Size = new Size(540, 300);
            MessageListpanel.Location = new Point(255, 141);
            MessageListpanel.BorderStyle = BorderStyle.FixedSingle;
            MessageListpanel.BackColor = Color.FromArgb(52, 73, 94);
            MessageListpanel.BorderStyle = BorderStyle.None;
            MessageListpanel.AutoScroll = true;
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.Controls.Add(MessageListpanel); // UI işlemleri burada yapılır.
                }));
            }
            else
            {
                this.Controls.Add(MessageListpanel); // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz.
            }
            #endregion

            #region veritabanından çekme işlemi
            var values = _userManager.TGetByToken(FrmLogin.userInformation);
            if (allFriendsMessage.Count == 0)  // Eğer allFriends boşsa, veritabanından çek
            {
                allFriendsMessage = await _userManager.TGetByIdFriendMessageList(values.Data.UserId, values.Data.Token);
            }
            #endregion

            foreach (var item in allFriendsMessage)
            {

                MessagePanel(item.SenderId);
                ImageShow(item.SenderImage,item.PhoneNumber);
                SenderName(item.SenderName);
                if (item.MessageType == "Text")
                {
                    SenderMessageContext(item.MessageContext);
                }
                if (item.MessageType == "Image")
                {
                    SenderMessageContext("Resim Dosyası Yolladı.");
                }
                if (item.MessageType == "Winrar")
                {
                    SenderMessageContext("Wnrar Dosyası Yolladı.");
                }
                if (item.MessageType == "Movie")
                {
                    SenderMessageContext("Video Dosyası Yolladı.");
                }
                if (item.MessageType == "Sound")
                {
                    SenderMessageContext("Ses Kaydı Yolladı.");
                }
                SenderMessageCount(item.MessageCount);
                SenderMessageDatetime(item.MessageTime);
                lastlocation += friendMessagePanel.Height + 10;

            }
        }

        #region son eklenen message panelinin locationu
        public void LastPanelLocation()
        {
            if (MessageListpanel.Controls.Count > 0)
            {
                // Son eklenen panel (son kontrol)
                Panel lastPanel = (Panel)MessageListpanel.Controls[MessageListpanel.Controls.Count - 1];

                // Son eklenen panelin konumu
                Point lastPanelLocation = lastPanel.Location;
                lastlocation = lastPanelLocation.Y;
                lastlocation += friendMessagePanel.Height + 10;
            }
            else
            {
                lastlocation = 0;
            }
        }
        #endregion

        #region MessageList paneli yani mesajların tutulduğu panel
        public void MessagePanel(int senderId)
        {

            friendMessagePanel = new Panel();

            if (MessageListpanel.AutoScrollPosition.Y != 0)
            {
                friendMessagePanel.Size = new Size(520, 0);  // Eğer scroll varsa, 916 yapıyoruz
            }
            else
            {
                friendMessagePanel.Size = new Size(540, 0);  // Scroll yoksa 936 yapıyoruz
            }

            friendMessagePanel.Location = new Point(0, lastlocation);
            friendMessagePanel.BorderStyle = BorderStyle.FixedSingle;
            friendMessagePanel.AutoSize = true;
            friendMessagePanel.BackColor = Color.Gold;
            friendMessagePanel.MouseClick += userPanel_LeftClick;
            friendMessagePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            friendMessagePanel.Name = senderId.ToString();

            if (MessageListpanel.InvokeRequired)
            {
                MessageListpanel.Invoke(new Action(() =>
                {
                    MessageListpanel.Controls.Add(friendMessagePanel);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                MessageListpanel.Controls.Add(friendMessagePanel);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesaj atan kişnin resmi 
        private async void ImageShow(string ImageLocation,string phoneNumber)
        {
            PictureBox pcbImageShow = new PictureBox();

            pcbImageShow.Size = new Size(50, 50);
            pcbImageShow.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbImageShow.Location = new Point(0, 0);
            pcbImageShow.MouseClick += userPanel_LeftClick;
            string Exit = DataControl(ImageLocation, phoneNumber);
            if (Exit=="false")
            {
                bool dowloadExit = await FileZilla.dowloadData(ImageLocation, phoneNumber);
                if (dowloadExit)
                {
                    string Imagelocation = DataControl(ImageLocation, phoneNumber);
                    pcbImageShow.ImageLocation = Imagelocation;
                }
                else
                {
                    MessageBox.Show("Resim indirirken bir hata oluştu!");
                }
            }
            else
            {
                pcbImageShow.ImageLocation = Exit;
            }
            
           

            if (friendMessagePanel.InvokeRequired)
            {
                friendMessagePanel.Invoke(new Action(() =>
                {
                    friendMessagePanel.Controls.Add(pcbImageShow);
                }));
            }
            else
            {
                friendMessagePanel.Controls.Add(pcbImageShow);
            }
        }
        #endregion

        #region Data var mı Kontrol
        public string DataControl(string ImageLocation, string phonnumber)
        {

            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", phonnumber);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Data", phonnumber, Path.GetFileName(ImageLocation));
            if (File.Exists(Image))
            {
                return Image;
            }
            else
            {
                return "false";
            }
        }
        #endregion

        #region mesaj atan kişinin adı
        private void SenderName(string sendername)
        {

            Label lblUserName = new Label();
            lblUserName.Text = sendername;
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Calibri", 16, FontStyle.Bold);
            lblUserName.Location = new Point(60, 10);
            lblUserName.Size = new Size(80,50);
            lblUserName.MouseClick += userPanel_LeftClick;
            if (friendMessagePanel.InvokeRequired)
            {
                friendMessagePanel.Invoke(new Action(() =>
                {
                    friendMessagePanel.Controls.Add(lblUserName);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                friendMessagePanel.Controls.Add(lblUserName);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesajın içeriği
        private void SenderMessageContext(string message)
        {
            Label lblMessageContext = new Label();
            lblMessageContext.Text = message.Length > 10 ? message.Substring(0, 10) : message;
            lblMessageContext.AutoSize = true;
            lblMessageContext.Font = new Font("Calibri", 16, FontStyle.Bold);
            lblMessageContext.Location = new Point(180, 10);
            lblMessageContext.Size = new Size(80, 50);
            lblMessageContext.MouseClick += userPanel_LeftClick;
            if (friendMessagePanel.InvokeRequired)
            {
                friendMessagePanel.Invoke(new Action(() =>
                {
                    friendMessagePanel.Controls.Add(lblMessageContext);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                friendMessagePanel.Controls.Add(lblMessageContext);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesaj sayısı
        private void SenderMessageCount(int count)
        {
            Label lblMessageCount = new Label();
            lblMessageCount.Text = count.ToString()+": Yeni Mesaj";
            lblMessageCount.AutoSize = true;
            lblMessageCount.Font = new Font("Calibri", 16, FontStyle.Bold);
            lblMessageCount.Location = new Point(320, 10);
            lblMessageCount.Size = new Size(80, 50);
            lblMessageCount.Name = "Count";
            lblMessageCount.MouseClick += userPanel_LeftClick;
            if (friendMessagePanel.InvokeRequired)
            {
                friendMessagePanel.Invoke(new Action(() =>
                {
                    friendMessagePanel.Controls.Add(lblMessageCount);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                friendMessagePanel.Controls.Add(lblMessageCount);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesaj gönderme tarihi
        private void SenderMessageDatetime(DateTime time)
        {
            Label lblMessageTime = new Label();
            TimeSpan difference = DateTime.Now - time;
            string formattedTime;
            lblMessageTime.MouseClick += userPanel_LeftClick;
            if (difference.TotalMinutes < 1)
                formattedTime = "Az önce yolladı.";
            else if (difference.TotalMinutes < 60)
                formattedTime = $"{(int)difference.TotalMinutes} dakika önce yolladı.";
            else if (difference.TotalHours < 24)
                formattedTime = $"{(int)difference.TotalHours} saat önce yolladı.";
            else if (difference.TotalDays < 7)
                formattedTime = $"{(int)difference.TotalDays} gün önce yolladı.";
            else
                formattedTime = time.ToString("dd.MM.yyyy HH:mm"); // Eğer 7 günden eskiyse tam tarih göster

            lblMessageTime.Text = formattedTime;
            lblMessageTime.AutoSize = true;
            lblMessageTime.Font = new Font("Calibri", 16, FontStyle.Bold);
            lblMessageTime.Location = new Point(450, 10);
            lblMessageTime.Size = new Size(80, 50);

            if (friendMessagePanel.InvokeRequired)
            {
                friendMessagePanel.Invoke(new Action(() =>
                {
                    friendMessagePanel.Controls.Add(lblMessageTime);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                friendMessagePanel.Controls.Add(lblMessageTime);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion
    }
}
