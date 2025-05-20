
using BusinessLayer.Concrete;
using BusinessLayer.Models;
using ChatApi.BusinessLayer.Concrete;
using ChatApi.DAL;
using FluentFTP;
using FrmLogin.Models;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using NAudio.Wave;
using System.Data.Common;
using System.Drawing.Imaging;





namespace FrmLogin
{
    public partial class FrmChatHub : Form
    {
        public FrmChatHub()
        {
            InitializeComponent();
        }
        UserManager userManager = new UserManager();
        private bool EmojiStatus = false, MessageEdit = false, SoundAddButton = false, isMuted=false;
        private string[] emojiArray;
        private Button emojiButton;
        MessageManager _messageManager = new MessageManager();
        private List<MessageContex> allFriends = new List<MessageContex>();
        private Panel userpanel, Motherpanel;
        public int lastlocation = 0, addedBreaks, lastMessageId = 0, MessageId, Soundtimer;
        private string FriendImageLocation, outputFileName, SoundFileName;
        private VideoView videoView;
        private WaveInEvent waveIn;
        private WaveFileWriter waveFileWriter;

        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOutEvent waveOut;

        private void imgFrmUserDashboard_Click(object sender, EventArgs e)
        {
            FrmUserDashboard frmUserDashboard = new FrmUserDashboard();
            frmUserDashboard.Show();
            this.Hide();
        }

        private async void FrmChatHub_Load(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);
            int userıd = user.Data.UserId;
            #region Mesaj atılacak kisinin bilgileri
            var values = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
            lblFriendOrGroupName.Text = values.Data.ChatName.Length > 10 ? values.Data.ChatName.Substring(0, 10) : values.Data.ChatName;

            if (!values.Data.ChatStatus)
            {
                lblTime.Text = values.Data.ChatTime.ToString();
            }
            else
            {
                lblTime.Text = "Online";
            }
            pcbFriendOrGroupImage.ImageLocation = await ImageDowload(values.Data.ChatImage, values.Data.PhoneNumber);
            FriendImageLocation = pcbFriendOrGroupImage.ImageLocation;
            #endregion

            #region Giriş yapanları dinleme(Online durumu)
            FrmLogin._connection.On<string>("MessageFromLogin", (message) =>
            {
                int loggedInUserId = int.Parse(message);

                if (loggedInUserId == values.Data.ChatId)
                {
                    if (lblTime.InvokeRequired)
                    {
                        lblTime.Invoke(new Action(() =>
                        {
                            lblTime.Text = "Online";
                        }));
                    }
                    else
                    {
                        lblTime.Text = "Online";
                    }
                }

            });
            #endregion

            #region Çıkış yapanları dinleme (Ofline durumu)
            FrmLogin._connection.On<string>("MessageFromLogOut", (message) =>
            {
                int loggedInUserId = int.Parse(message);

                if (loggedInUserId == values.Data.ChatId)
                {
                    if (lblTime.InvokeRequired)
                    {
                        lblTime.Invoke(new Action(() =>
                        {
                            lblTime.Text = values.Data.ChatTime.ToString();  // UI elemanını güncelliyoruz
                        }));
                    }
                    else
                    {
                        lblTime.Text = values.Data.ChatTime.ToString();
                    }
                }

            });
            #endregion

            #region Emoji tanımlama
            // Emoji sınıfındaki tüm const alanları almak
            var emojiType = typeof(Emoji);
            var fields = emojiType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            // Emojileri bir diziye ekleme
            emojiArray = fields.Select(f => (string)f.GetValue(null)).ToArray();
            foreach (var field in fields)
            {
                string emoji = (string)field.GetValue(null);
                emojiButton = new Button
                {
                    Text = emoji,
                    Width = 50,
                    Height = 50,
                    Font = new Font("Segoe UI Emoji", 20)
                };
                emojiButton.Click += EmojiButton_Click;
                pnlEmoji.Controls.Add(emojiButton);
            }
            #endregion

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



            #region düzenlenen mesaj dinleme
            FrmLogin._connection.On<string, int, int>("ReceiveEditMessage", (message, senderUserId, messageId) =>
            {
                // Mesajı alıp ekranda göstermek için
                Invoke(new Action(() =>
                {
                    foreach (Control control in Motherpanel.Controls)
                    {
                        if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                        {
                            // userpanel'i bulduktan sonra düzenleme işlemi
                            var label = userpanel.Controls.OfType<System.Windows.Forms.Label>().FirstOrDefault();
                            if (label != null)
                            {
                                label.Text = message;
                                MessageReading(messageId);
                                break; // İşlem tamamlandı, döngüyü sonlandır
                            }
                        }
                    }
                }));
            });
            #endregion

            #region okunan mesajları dinleme
            FrmLogin._connection.On<int>("ReadingMessage", (messageId) =>
            {
                // Mesajı alıp ekranda göstermek için
                Invoke(new Action(() =>
                {
                    foreach (Control control in Motherpanel.Controls)
                    {
                        // userpanel'i mesajId'ye göre bul
                        if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                        {
                            // userpanel içinde bulunan tüm PictureBox'ları bul ve ismine göre sil
                            foreach (var pictureBox in userpanel.Controls.OfType<PictureBox>())
                            {
                                if (pictureBox.Name == "pcbMessageReadingme")  // İlgili PictureBox'ı isme göre bul
                                {
                                    System.Drawing.Image image;
                                    image = Properties.Resources.okundu;
                                    pictureBox.Image = image;
                                    break; // İşlem tamamlandı, döngüyü sonlandır
                                }
                            }
                        }
                    }
                }));
            });
            #endregion

            #region Arama İsteğini Dinleme
            FrmLogin._connection.On<int>("callRequest", messageId =>
            {
                if(messageId == userıd)
                { 
                    // Ekranda bir MessageBox göster
                    var result = MessageBox.Show("Birisi seni arıyor. Aramayı kabul etmek ister misin?",
                                                  "Arama İsteği" +userıd.ToString(),
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

                    // Kullanıcı 'Evet' seçerse, çağrıyı kabul et
                    if (result == DialogResult.Yes)
                    {
                        // Aramayı kabul etme işlemi (asenkron)
                        Task.Run(async () =>
                        {
                            await FrmLogin._connection.InvokeAsync("AcceptCall", user.Data.UserId,int.Parse(FrmUserDashboard.chatId));
                            // Kullanıcıya bildirim gönder
                            MessageBox.Show("Arama kabul edildi. Sesli görüşme başlatılıyor.");

                            waveIn = new WaveInEvent
                            {
                                DeviceNumber = 0, // Mikrofon cihazı numarası
                                WaveFormat = new WaveFormat(44100, 1) // Ses formatı (44100 Hz, Mono)
                            };

                            waveIn.DataAvailable += async (sender2, e2) =>
                            {
                                // Mikrofon verisini alıyoruz
                                byte[] audioData = new byte[e2.BytesRecorded];
                                Array.Copy(e2.Buffer, audioData, e2.BytesRecorded);

                                // Veriyi SignalR ile gönderiyoruz
                                await FrmLogin._connection.SendAsync("SendAudioData", audioData, int.Parse(FrmUserDashboard.chatId));
                            };

                            // Mikrofon kaydını başlatıyoruz
                            waveIn.StartRecording();

                        });
                    }
                    else
                    {
                        // Aramayı reddetme işlemi (asenkron)
                        Task.Run(async () =>
                        {
                            await FrmLogin._connection.InvokeAsync("RejectCall", int.Parse(FrmUserDashboard.chatId));
                            // Kullanıcıya bildirim gönder
                            MessageBox.Show("Arama reddedildi.");
                        });
                    }
                }
            });
            #endregion

            FrmLogin._connection.On<byte[]>("ReceiveAudioData", (audioData) =>
            {
                if(!isMuted)
                {
                    PlayAudio(audioData);
                }

            });

            MessageList();
        }

        private void PlayAudio(byte[] audioData)
        {
            // Ses verisini oynatmaya başlıyoruz
            if (bufferedWaveProvider == null)
            {
                bufferedWaveProvider = new BufferedWaveProvider(new WaveFormat(44100, 1)); // Mono, 44100 Hz
                waveOut = new WaveOutEvent();
                waveOut.Init(bufferedWaveProvider);
                waveOut.Play();
            }

            // Gelen ses verisini BufferedWaveProvider'a ekliyoruz
            bufferedWaveProvider.AddSamples(audioData, 0, audioData.Length);
        }

        #region Mesajı ekrana bastırma kısmı
        private async void DisplayMessage(string message, int senderUserId, string type, int messageId)
        {
            var riceverabout = await userManager.TGetByIdFriend(senderUserId, FrmLogin.userInformation);
            LastPanelLocation();
            UserPanel(messageId, senderUserId);
            if (type == "Text")
            {
                AddMessage(message, senderUserId);
            }
            if (type == "Image")
            {
                AddImage(senderUserId, message, riceverabout.Data.PhoneNumber);
            }
            if (type == "Winrar")
            {
                AddWinrar(senderUserId, message, riceverabout.Data.PhoneNumber);
            }
            if (type == "Movie")
            {
                AddMovie(senderUserId, message, riceverabout.Data.PhoneNumber, messageId);
            }
            if (type == "Sound")
            {
                AddSound(senderUserId, message, riceverabout.Data.PhoneNumber, messageId);
            }
            FriendImage();
            MessageDateTime(DateTime.Now.ToString(), senderUserId.ToString());
            MessageReading(messageId);
            await FrmLogin._connection.InvokeAsync("MessageReading", messageId, riceverabout.Data.ChatId);
        }
        #endregion

        #region MessageList Sayfa ilk açıldığında gelceklerin formatı
        public async void MessageList()
        {

            #region veritabanından çekme işlemi
            var values = userManager.TGetByToken(FrmLogin.userInformation);
            if (allFriends.Count == 0)  // Eğer allFriends boşsa, veritabanından çek
            {
                allFriends = await _messageManager.GetlistWithFriendId(values.Data.UserId, int.Parse(FrmUserDashboard.chatId), values.Data.Token);

            }
            #endregion

            #region Ana Panel
            Motherpanel = new Panel();
            Motherpanel.Size = new Size(936, 350);
            Motherpanel.Location = new Point(0, 135);
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


            var riceverabout = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
            foreach (var item in allFriends)
            {

                UserPanel(item.MessageId, item.SenderId);
                if (item.SenderId.ToString() != FrmUserDashboard.chatId)
                {
                    if (item.MessageType == "Text")
                    {
                        AddMessage(item.MessageContext, item.SenderId);
                        EditMessage(item.SenderId, item.MessageId);
                    }
                    if (item.MessageType == "Image")
                    {
                        AddImage(item.SenderId, item.MessageContext, values.Data.PhoneNumber);
                    }
                    if (item.MessageType == "Winrar")
                    {
                        AddWinrar(item.SenderId, item.MessageContext, values.Data.PhoneNumber);
                    }
                    if (item.MessageType == "Movie")
                    {
                        AddMovie(item.SenderId, item.MessageContext, values.Data.PhoneNumber, item.MessageId);
                    }
                    if (item.MessageType == "Sound")
                    {
                        AddSound(item.SenderId, item.MessageContext, values.Data.PhoneNumber, item.MessageId);
                    }
                    DeleteMessage(item.SenderId, item.MessageId);
                    MessageReadingme(item.MessageReading);
                    MessageDateTime(item.MessageTime.ToString(), item.SenderId.ToString());

                }
                else
                {
                    if (item.MessageType == "Text")
                    {
                        AddMessage(item.MessageContext, item.SenderId);
                    }
                    if (item.MessageType == "Image")
                    {
                        AddImage(item.SenderId, item.MessageContext, riceverabout.Data.PhoneNumber);
                    }
                    if (item.MessageType == "Winrar")
                    {
                        AddWinrar(item.SenderId, item.MessageContext, riceverabout.Data.PhoneNumber);
                    }
                    if (item.MessageType == "Movie")
                    {
                        AddMovie(item.SenderId, item.MessageContext, riceverabout.Data.PhoneNumber, item.MessageId);
                    }
                    if (item.MessageType == "Sound")
                    {
                        AddSound(item.SenderId, item.MessageContext, riceverabout.Data.PhoneNumber, item.MessageId);
                    }
                    FriendImage();
                    MessageDateTime(item.MessageTime.ToString(), item.SenderId.ToString());
                    MessageReading(item.MessageId);
                    await FrmLogin._connection.InvokeAsync("MessageReading", item.MessageId, riceverabout.Data.ChatId);
                }

                lastlocation += userpanel.Height + 10;

            }


            #region Panel Scrol Kontrol 
            if (Motherpanel.VerticalScroll.Visible)
            {
                // Eğer scroll görünürse, userpanel'in boyutunu yeniden düzenle
                foreach (Control control in Motherpanel.Controls)
                {
                    control.Width = 916;  // Scroll açıldığında boyutları 768'e ayarla
                }
            }
            #endregion

        }
        #endregion

        #region Resim var mı diye Kontrol Et Yoksa İndir
        public async Task<string> ImageDowload(string ImageLocation, string PhoneNumber)
        {
            var reiceverabout = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Data/{reiceverabout.Data.PhoneNumber}");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + $"Data/{reiceverabout.Data.PhoneNumber}", Path.GetFileName(ImageLocation));
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

        #region Emoji ekleme Kısmı
        private void emoji_img_Click(object sender, EventArgs e)
        {
            if (EmojiStatus)
            {
                pnlEmoji.Visible = true;
                EmojiStatus = false;
            }
            else
            {
                pnlEmoji.Visible = false;
                EmojiStatus = true;
            }
        }
        #endregion

        #region emojibutonunu tetikleme
        private void EmojiButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                txtAddMessage.Text += clickedButton.Text;
            }
        }
        #endregion

        #region Mesaj gönderme
        private async void pcbAddMessage_Click(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);
            #region normal mesaj atma
            if (!MessageEdit)
            {

                var values = await _messageManager.TGetByIdAndFriendIdAddMessage(user.Data.UserId, int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation, txtAddMessage.Text, "Text");
                if (values.Success)
                {
                    LastPanelLocation();

                    UserPanel(values.Data.MessageId, user.Data.UserId);
                    AddMessage(txtAddMessage.Text, user.Data.UserId);
                    MessageReadingme(false);
                    MessageDateTime(DateTime.Now.ToString(), user.Data.UserId.ToString());
                    Motherpanel.ScrollControlIntoView(userpanel);
                    DeleteMessage(user.Data.UserId, values.Data.MessageId);
                    EditMessage(user.Data.UserId, values.Data.MessageId);
                    txtAddMessage.Clear();
                    await FrmLogin._connection.InvokeAsync("SendMessage", values.Data.ReceiverId, values.Data.MessageContext, user.Data.UserId, "Text", values.Data.MessageId);
                }
                else
                {
                    MessageBox.Show(values.ErrorMessage);
                }
            }
            #endregion

            #region atılan mesajı düzenleyip tekrar yollama
            else
            {
                if (string.IsNullOrWhiteSpace(txtAddMessage.Text))
                {
                    MessageBox.Show("En az Bir karekter girmeniz gerekmektedir");
                }
                else
                {
                    bool status = await _messageManager.TEditMessageWithByMessageId(MessageId, txtAddMessage.Text, FrmLogin.userInformation);
                    if (status)
                    {
                        foreach (Control control in Motherpanel.Controls)
                        {
                            if (control is Panel userpanel && (string)userpanel.Name == MessageId.ToString())
                            {
                                // userpanel'i bulduktan sonra düzenleme işlemi
                                var label = userpanel.Controls.OfType<System.Windows.Forms.Label>().FirstOrDefault();
                                if (label != null)
                                {
                                    label.Text = txtAddMessage.Text;
                                    await FrmLogin._connection.InvokeAsync("SendEditMessage", int.Parse(FrmUserDashboard.chatId), txtAddMessage.Text, user.Data.UserId, MessageId);
                                    txtAddMessage.Clear();
                                    break; // İşlem tamamlandı, döngüyü sonlandır
                                }
                            }
                        }
                    }
                }

            }
            #endregion
        }
        #endregion

        #region mesajcontext
        private void AddMessage(string message, int sender)
        {
            string text = InsertLineBreaks(message, 30);
            System.Windows.Forms.Label lblUserName = new System.Windows.Forms.Label();
            lblUserName.Text = text;
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Calibri", 16, FontStyle.Bold);

            if (sender.ToString() != FrmUserDashboard.chatId)
            {
                lblUserName.Location = new Point(430, 10);
                lblUserName.Size = new Size(350, addedBreaks * 80);
            }
            else
            {
                lblUserName.Location = new Point(70, 10);
                lblUserName.Size = new Size(350, addedBreaks * 80);
            }

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
        }
        #endregion

        #region Mesaj Gönderme tarihi
        private void MessageDateTime(string dateTime, string senderId)
        {

            System.Windows.Forms.Label lblUserName = new System.Windows.Forms.Label();
            lblUserName.Text = dateTime;
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Calibri", 16, FontStyle.Bold);

            if (senderId.ToString() != FrmUserDashboard.chatId)
            {
                lblUserName.Location = new Point(220, 10);
            }
            else
            {
                lblUserName.Location = new Point(450, 10);
            }



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
        }
        #endregion

        #region görüldü mü 
        private void MessageReadingme(bool messagestatus)
        {

            PictureBox pcbMessageReadingme = new PictureBox();
            System.Drawing.Image image;
            pcbMessageReadingme.Location = new Point(860, 10);
            pcbMessageReadingme.Size = new Size(50, 30);
            pcbMessageReadingme.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbMessageReadingme.Name = "pcbMessageReadingme";


            if (messagestatus)
            {
                image = Properties.Resources.okundu;
                pcbMessageReadingme.Image = image;
            }
            else
            {
                image = Properties.Resources.okunmadi;
                pcbMessageReadingme.Image = image;
            }


            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(pcbMessageReadingme);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbMessageReadingme);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region Resim gönderme 
        private async void pcbAddImage_Click(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);


            ofdData.Title = "Resim sec";
            ofdData.Filter = "PNG Dosyaları (*.png)|*.png";
            if (ofdData.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = ofdData.FileName;

                FileInfo fileInfo = new FileInfo(ofdData.FileName);
                long fileSizeInBytes = fileInfo.Length;
                long maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB (1024 KB * 1024 byte * 10 MB)

                if (fileSizeInBytes > maxFileSizeInBytes)
                {
                    MessageBox.Show("Seçilen dosya boyutu 10 MB'ı geçemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (selectedFilePath.Length > 100)
                    {
                        MessageBox.Show("Seçilen dosya yolunun uzunluğu 100 karakterden fazla olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var Reievervalues = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                        string ImageUrl = await FileZilla.addData(ofdData.FileName, Reievervalues.Data.PhoneNumber);
                        if (!string.IsNullOrEmpty(ImageUrl))
                        {
                            var reiceverabout = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                            var values = await _messageManager.TGetByIdAndFriendIdAddMessage(user.Data.UserId, int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation, ImageUrl, "Image");
                            if (values.Success)
                            {
                                #region ftpye eklenen datanın ismini alma
                                string cleanLocation = ImageUrl.Replace("ftp://", "");
                                string dataName = cleanLocation.Split('/').Last();
                                #endregion
                                string targetDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, $"Data/{user.Data.PhoneNumber}");
                                string targetPath = Path.Combine(targetDirectory, Path.GetFileName(dataName));

                                try
                                {
                                    // Dosya hedef dizine kopyalanıyor
                                    File.Copy(selectedFilePath, targetPath, overwrite: true);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Dosya kopyalama hatası: " + ex.Message);
                                }

                                MessageBox.Show("Resim Başarıyla Eklendi");
                                LastPanelLocation();
                                UserPanel(values.Data.MessageId, user.Data.UserId);
                                AddImage(user.Data.UserId, ImageUrl, user.Data.PhoneNumber);
                                MessageReadingme(false);
                                MessageDateTime(DateTime.Now.ToString(), user.Data.UserId.ToString());
                                Motherpanel.ScrollControlIntoView(userpanel);
                                DeleteMessage(user.Data.UserId, values.Data.MessageId);
                                await FrmLogin._connection.InvokeAsync("SendMessage", values.Data.ReceiverId, values.Data.MessageContext, user.Data.UserId, "Image", values.Data.MessageId);

                            }
                            else
                            {
                                MessageBox.Show("Hata");
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Konuştugun kişinin Resimini Göster
        private void FriendImage()
        {

            PictureBox pcbFriendImage = new PictureBox();
            pcbFriendImage.Location = new Point(0, 10);
            pcbFriendImage.Size = new Size(50, 30);
            pcbFriendImage.SizeMode = PictureBoxSizeMode.StretchImage;

            pcbFriendImage.ImageLocation = FriendImageLocation;

            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(pcbFriendImage);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbFriendImage);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesajlajtıgın kişiye attığın resim veya aldığın resim
        private void AddImage(int senderId, string ImageLocation, string senderPhoneNumber)
        {
            PictureBox pcbImageShow = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbImageShow.Location = new Point(150, 0);
            }
            else
            {
                pcbImageShow.Location = new Point(500, 0);
            }

            pcbImageShow.Size = new Size(200, 200);
            pcbImageShow.SizeMode = PictureBoxSizeMode.StretchImage;

            #region resim indirme ikonu

            string Exit = DataControl(ImageLocation, senderPhoneNumber);
            if (Exit == "false")
            {
                System.Drawing.Image ImageIcon;
                ImageIcon = Properties.Resources.Image;
                pcbImageShow.Image = ImageIcon;
                PictureBox pcbImageDowloadIcon = new PictureBox();
                if (senderId.ToString() == FrmUserDashboard.chatId)
                {
                    pcbImageDowloadIcon.Location = new Point(pcbImageShow.Left - 50, 140);
                }
                else
                {
                    pcbImageDowloadIcon.Location = new Point(pcbImageShow.Right + 10, 140);
                }

                pcbImageDowloadIcon.Size = new Size(50, 50);
                pcbImageDowloadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

                System.Drawing.Image dowloadImageIcon;
                dowloadImageIcon = Properties.Resources.ImageDowload;
                pcbImageDowloadIcon.Image = dowloadImageIcon;
                #endregion

                pcbImageDowloadIcon.Click += async (s, e) =>
                {
                    try
                    {
                        pcbImageDowloadIcon.Visible = false;
                        bool dowloadExit = await FileZilla.dowloadData(ImageLocation, senderPhoneNumber);
                        if (dowloadExit)
                        {
                            pcbImageDowloadIcon.Visible = false;
                            //MessageBox.Show("İndirme tamamlandı!");
                            string Imagelocation = DataControl(ImageLocation, senderPhoneNumber);
                            pcbImageShow.ImageLocation = Imagelocation;
                        }
                    }
                    catch (Exception ex)
                    {
                        pcbImageDowloadIcon.Visible = true;
                        MessageBox.Show($"İndirme sırasında bir hata oluştu: {ex.Message}");
                    }
                };

                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbImageDowloadIcon);
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbImageDowloadIcon);
                }

            }
            else
            {
                pcbImageShow.ImageLocation = Exit;
            }


            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(pcbImageShow);
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbImageShow);
            }
        }
        #endregion

        #region mesajlajtıgın kişiye attığın winrar veya aldığın
        private void AddWinrar(int senderId, string ImageLocation, string phoneNumber)
        {

            PictureBox pcbWinrarShow = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbWinrarShow.Location = new Point(250, 0);
            }
            else
            {
                pcbWinrarShow.Location = new Point(500, 0);
            }

            pcbWinrarShow.Size = new Size(200, 200);
            pcbWinrarShow.SizeMode = PictureBoxSizeMode.StretchImage;
            //pcbWinrarShow.ImageLocation = await ImageDowload(ImageLocation);
            System.Drawing.Image image;
            image = Properties.Resources.rar;
            pcbWinrarShow.Image = image;


            #region winrar ismini label ekleme kısmı
            string WinrarName = Path.GetFileName(ImageLocation);
            string text = InsertLineBreaks(WinrarName, 30);
            System.Windows.Forms.Label lblWinrarName = new System.Windows.Forms.Label();
            lblWinrarName.Text = text;
            lblWinrarName.AutoSize = true;
            lblWinrarName.Font = new Font("Calibri", 16, FontStyle.Bold);

            if (senderId.ToString() != FrmUserDashboard.chatId)
            {
                lblWinrarName.Location = new Point(500, 180);
                lblWinrarName.Size = new Size(350, addedBreaks * 80);
            }
            else
            {
                lblWinrarName.Location = new Point(300, 180);
                lblWinrarName.Size = new Size(350, addedBreaks * 80);
            }
            #endregion

            #region winrar indirme ikonu
            string Exit = DataControl(ImageLocation, phoneNumber);
            if (Exit == "false")
            {
                PictureBox pcbWinrarDowloadIcon = new PictureBox();
                if (senderId.ToString() == FrmUserDashboard.chatId)
                {
                    pcbWinrarDowloadIcon.Location = new Point(pcbWinrarShow.Left - 50, 150);
                }
                else
                {
                    pcbWinrarDowloadIcon.Location = new Point(pcbWinrarShow.Right + 10, 150);
                }

                pcbWinrarDowloadIcon.Size = new Size(50, 50);
                pcbWinrarDowloadIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                //pcbWinrarShow.ImageLocation = await ImageDowload(ImageLocation);
                System.Drawing.Image dowloadIcon;
                dowloadIcon = Properties.Resources.winrardowload;
                pcbWinrarDowloadIcon.Image = dowloadIcon;

                pcbWinrarDowloadIcon.Click += async (downloadSender, downloadEvent) =>
                {
                    try
                    {
                        pcbWinrarDowloadIcon.Visible = false;
                        bool dowloadExit = await FileZilla.dowloadData(ImageLocation, phoneNumber);
                        if (dowloadExit)
                        {
                            //MessageBox.Show("İndirme tamamlandı!");
                            string Imagelocation = DataControl(ImageLocation, phoneNumber);
                            pcbWinrarShow.Name = Imagelocation;

                            pcbWinrarShow.Click += (showSender, showEvent) =>
                            {
                                string directoryPath = Path.GetDirectoryName(Imagelocation); // Dosyanın bulunduğu dizini al
                                System.Diagnostics.Process.Start("explorer.exe", directoryPath); // Explorer'ı bu dizine yönlendir
                            };

                        }
                    }
                    catch (Exception ex)
                    {
                        pcbWinrarDowloadIcon.Visible = true;
                        MessageBox.Show($"İndirme sırasında bir hata oluştu: {ex.Message}");
                    }
                };

                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbWinrarDowloadIcon);
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbWinrarDowloadIcon);
                }
            }
            else
            {
                pcbWinrarShow.Name = Exit;

                pcbWinrarShow.Click += (showSender, showEvent) =>
                {
                    string directoryPath = Path.GetDirectoryName(Exit); // Dosyanın bulunduğu dizini al
                    System.Diagnostics.Process.Start("explorer.exe", directoryPath); // Explorer'ı bu dizine yönlendir
                };

            }
            #endregion

            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(lblWinrarName);
                    userpanel.Controls.Add(pcbWinrarShow);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                userpanel.Controls.Add(lblWinrarName);
                userpanel.Controls.Add(pcbWinrarShow);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region mesajlajtıgın kişiye attığın video veya aldığın video
        private void AddMovie(int senderId, string ImageLocation, string senderPhoneNumber, int messageId)
        {
            PictureBox pcbMovieShow = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbMovieShow.Location = new Point(150, 0);
            }
            else
            {
                pcbMovieShow.Location = new Point(500, 0);
            }

            pcbMovieShow.Size = new Size(200, 200);
            pcbMovieShow.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbMovieShow.Name = "pcbMovieShow";

            #region video indirme ikonu

            string Exit = DataControl(ImageLocation, senderPhoneNumber);
            if (Exit == "false")
            {
                System.Drawing.Image MovieIcon;
                MovieIcon = Properties.Resources.Movie;
                pcbMovieShow.Image = MovieIcon;
                PictureBox pcbMovieDowloadIcon = new PictureBox();
                if (senderId.ToString() == FrmUserDashboard.chatId)
                {
                    pcbMovieDowloadIcon.Location = new Point(pcbMovieShow.Left - 50, 140);
                }
                else
                {
                    pcbMovieDowloadIcon.Location = new Point(pcbMovieShow.Right + 10, 140);
                }

                pcbMovieDowloadIcon.Size = new Size(50, 50);
                pcbMovieDowloadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

                System.Drawing.Image dowloadImageIcon;
                dowloadImageIcon = Properties.Resources.dowloadMovie;
                pcbMovieDowloadIcon.Image = dowloadImageIcon;
                #endregion

                pcbMovieDowloadIcon.Click += async (s, e) =>
                {
                    try
                    {
                        pcbMovieDowloadIcon.Visible = false;
                        bool dowloadExit = await FileZilla.dowloadData(ImageLocation, senderPhoneNumber);
                        if (dowloadExit)
                        {
                            pcbMovieDowloadIcon.Visible = false;
                            //MessageBox.Show("İndirme tamamlandı!");
                            string MovieLocation = DataControl(ImageLocation, senderPhoneNumber);

                            #region ıdlı paneli bulup içindeki picbox silip yerine vlc ekleme
                            foreach (Control control in Motherpanel.Controls)
                            {
                                // userpanel'i mesajId'ye göre bul
                                if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                                {
                                    // userpanel içinde bulunan tüm PictureBox'ları bul ve ismine göre sil
                                    foreach (var pictureBox in userpanel.Controls.OfType<PictureBox>())
                                    {
                                        if (pictureBox.Name == "pcbMovieShow")  // İlgili PictureBox'ı isme göre bul
                                        {
                                            // PictureBox'ı sil
                                            userpanel.Controls.Remove(pictureBox);
                                            string libVlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                                            Core.Initialize(libVlcPath);
                                            var libVLC = new LibVLC(libVlcPath);
                                            var mediaPlayer = new MediaPlayer(libVLC)
                                            {
                                                Media = new Media(libVLC, new Uri(MovieLocation))
                                            };

                                            // Video görüntüsünü ekleme
                                            var videoView = new VideoView
                                            {
                                                MediaPlayer = mediaPlayer,
                                                Dock = DockStyle.None,
                                                Size = new Size(180, 180),
                                                Visible = true,
                                            };

                                            if (senderId.ToString() == FrmUserDashboard.chatId)
                                            {
                                                videoView.Location = new Point(150, 0);
                                            }
                                            else
                                            {
                                                videoView.Location = new Point(500, 0);
                                            }

                                            #region play butonu
                                            PictureBox PcbPlayButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };

                                            System.Drawing.Image PcbPlayButtonImage;
                                            PcbPlayButtonImage = Properties.Resources.playButton;
                                            PcbPlayButton.Image = PcbPlayButtonImage;

                                            PcbPlayButton.Click += (playclick, playEventArgs) =>
                                            {
                                                mediaPlayer.Play();
                                            };
                                            #endregion

                                            #region pause button
                                            PictureBox pcbPauseButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left + 50, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };

                                            System.Drawing.Image PcbPauseButtonImage;
                                            PcbPauseButtonImage = Properties.Resources.pauseButton;
                                            pcbPauseButton.Image = PcbPauseButtonImage;

                                            pcbPauseButton.Click += (pcbPauseclick, PcbPauseButtonEventArgs) =>
                                            {
                                                mediaPlayer.Pause();
                                            };
                                            #endregion

                                            #region stop button
                                            PictureBox pcbStopButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left + 100, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };
                                            System.Drawing.Image PcbStopButtonImage;
                                            PcbStopButtonImage = Properties.Resources.stopButton;
                                            pcbStopButton.Image = PcbStopButtonImage;

                                            pcbStopButton.Click += (PcbStopButtonclick, PcbStopButtonEventArgs) =>
                                            {
                                                mediaPlayer.Stop();
                                            };
                                            #endregion

                                            #region FullScreen Butonu
                                            PictureBox pcbFullScreenButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left + 150, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };
                                            System.Drawing.Image PcbFullScreenButtonImage;
                                            bool statusFullScreen = false;
                                            PcbFullScreenButtonImage = Properties.Resources.fullScreenButton;
                                            pcbFullScreenButton.Image = PcbFullScreenButtonImage;

                                            pcbFullScreenButton.Click += (PcbFullScreenButtonclick, PcbFullScreenButtonEventArgs) =>
                                            {
                                                // Video'yu tam ekran yapma:
                                                if (!statusFullScreen)
                                                {
                                                    videoView.Dock = DockStyle.Fill;  // Video view'i formun tüm alanına yay
                                                    statusFullScreen = true;
                                                }
                                                else
                                                {
                                                    videoView.Dock = DockStyle.None;
                                                    statusFullScreen = false;
                                                }

                                            };
                                            #endregion

                                            if (userpanel.InvokeRequired)
                                            {
                                                userpanel.Invoke(new Action(() =>
                                                {
                                                    userpanel.Controls.Add(PcbPlayButton);
                                                    userpanel.Controls.Add(pcbPauseButton);
                                                    userpanel.Controls.Add(pcbStopButton);
                                                    userpanel.Controls.Add(pcbFullScreenButton);
                                                    userpanel.Controls.Add(videoView);

                                                }));
                                            }
                                            else
                                            {
                                                userpanel.Controls.Add(pcbFullScreenButton);
                                                userpanel.Controls.Add(PcbPlayButton);
                                                userpanel.Controls.Add(pcbPauseButton);
                                                userpanel.Controls.Add(pcbStopButton);
                                                userpanel.Controls.Add(videoView);

                                            }
                                            break; // Sadece bir tane PictureBox silmek istiyoruz, işlemi sonlandır
                                        }
                                    }
                                    break; // İşlem tamamlandı, döngüyü sonlandır
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        pcbMovieDowloadIcon.Visible = true;
                        MessageBox.Show($"İndirme sırasında bir hata oluştu: {ex.Message}");
                    }
                };

                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbMovieDowloadIcon);
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbMovieDowloadIcon);
                }

            }
            else
            {
                vlcPlayer(Exit, senderId);
            }


            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {

                    userpanel.Controls.Add(pcbMovieShow);
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbMovieShow);
            }
        }
        #endregion

        #region mesajlajtıgın kişiye attığın seskaydı veya aldığın seskaydı
        private void AddSound(int senderId, string ImageLocation, string senderPhoneNumber, int messageId)
        {

            PictureBox pcbSoundShow = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbSoundShow.Location = new Point(150, 0);
            }
            else
            {
                pcbSoundShow.Location = new Point(500, 0);
            }

            pcbSoundShow.Size = new Size(100, 100);
            pcbSoundShow.SizeMode = PictureBoxSizeMode.StretchImage;
            pcbSoundShow.Name = "pcbSoundShow";

            #region video indirme ikonu

            string Exit = DataControl(ImageLocation, senderPhoneNumber);
            if (Exit == "false")
            {
                System.Drawing.Image SoundIcon;
                SoundIcon = Properties.Resources.Sound;
                pcbSoundShow.Image = SoundIcon;
                PictureBox pcbSoundDowloadIcon = new PictureBox();
                if (senderId.ToString() == FrmUserDashboard.chatId)
                {
                    pcbSoundDowloadIcon.Location = new Point(pcbSoundShow.Left - 50, pcbSoundShow.Bottom - 50);
                }
                else
                {
                    pcbSoundDowloadIcon.Location = new Point(pcbSoundShow.Right + 10, pcbSoundShow.Bottom - 50);
                }

                pcbSoundDowloadIcon.Size = new Size(50, 50);
                pcbSoundDowloadIcon.SizeMode = PictureBoxSizeMode.StretchImage;

                System.Drawing.Image dowloadSoundIcon;
                dowloadSoundIcon = Properties.Resources.soundDowload;
                pcbSoundDowloadIcon.Image = dowloadSoundIcon;
                #endregion

                pcbSoundDowloadIcon.Click += async (s, e) =>
                {
                    try
                    {
                        pcbSoundDowloadIcon.Visible = false;
                        bool dowloadExit = await FileZilla.dowloadData(ImageLocation, senderPhoneNumber);
                        if (dowloadExit)
                        {
                            pcbSoundDowloadIcon.Visible = false;
                            //MessageBox.Show("İndirme tamamlandı!");
                            string MovieLocation = DataControl(ImageLocation, senderPhoneNumber);

                            #region ıdlı paneli bulup içindeki picbox silip yerine vlc ekleme
                            foreach (Control control in Motherpanel.Controls)
                            {
                                // userpanel'i mesajId'ye göre bul
                                if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                                {
                                    // userpanel içinde bulunan tüm PictureBox'ları bul ve ismine göre sil
                                    foreach (var pictureBox in userpanel.Controls.OfType<PictureBox>())
                                    {
                                        if (pictureBox.Name == "pcbSoundShow")  // İlgili PictureBox'ı isme göre bul
                                        {
                                            // PictureBox'ı sil
                                            userpanel.Controls.Remove(pictureBox);
                                            string libVlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                                            Core.Initialize(libVlcPath);
                                            var libVLC = new LibVLC(libVlcPath);
                                            var mediaPlayer = new MediaPlayer(libVLC)
                                            {
                                                Media = new Media(libVLC, new Uri(MovieLocation))
                                            };

                                            // Video görüntüsünü ekleme
                                            var videoView = new VideoView
                                            {
                                                MediaPlayer = mediaPlayer,
                                                Dock = DockStyle.None,
                                                Size = new Size(180, 60),
                                                Visible = true,
                                            };

                                            if (senderId.ToString() == FrmUserDashboard.chatId)
                                            {
                                                videoView.Location = new Point(150, 0);
                                            }
                                            else
                                            {
                                                videoView.Location = new Point(500, 0);
                                            }

                                            #region play butonu
                                            PictureBox PcbPlayButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };

                                            System.Drawing.Image PcbPlayButtonImage;
                                            PcbPlayButtonImage = Properties.Resources.playButton;
                                            PcbPlayButton.Image = PcbPlayButtonImage;

                                            PcbPlayButton.Click += (playclick, playEventArgs) =>
                                            {
                                                mediaPlayer.Play();
                                            };
                                            #endregion

                                            #region pause button
                                            PictureBox pcbPauseButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left + 50, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };

                                            System.Drawing.Image PcbPauseButtonImage;
                                            PcbPauseButtonImage = Properties.Resources.pauseButton;
                                            pcbPauseButton.Image = PcbPauseButtonImage;

                                            pcbPauseButton.Click += (pcbPauseclick, PcbPauseButtonEventArgs) =>
                                            {
                                                mediaPlayer.Pause();
                                            };
                                            #endregion

                                            #region stop button
                                            PictureBox pcbStopButton = new PictureBox
                                            {
                                                Location = new Point(videoView.Left + 100, videoView.Bottom + 10),
                                                Size = new Size(50, 20),
                                                SizeMode = PictureBoxSizeMode.StretchImage,
                                                BackColor = Color.Transparent
                                            };
                                            System.Drawing.Image PcbStopButtonImage;
                                            PcbStopButtonImage = Properties.Resources.stopButton;
                                            pcbStopButton.Image = PcbStopButtonImage;

                                            pcbStopButton.Click += (PcbStopButtonclick, PcbStopButtonEventArgs) =>
                                            {
                                                mediaPlayer.Stop();
                                            };
                                            #endregion

                                            if (userpanel.InvokeRequired)
                                            {
                                                userpanel.Invoke(new Action(() =>
                                                {
                                                    userpanel.Controls.Add(PcbPlayButton);
                                                    userpanel.Controls.Add(pcbPauseButton);
                                                    userpanel.Controls.Add(pcbStopButton);
                                                    userpanel.Controls.Add(videoView);

                                                }));
                                            }
                                            else
                                            {
                                                userpanel.Controls.Add(PcbPlayButton);
                                                userpanel.Controls.Add(pcbPauseButton);
                                                userpanel.Controls.Add(pcbStopButton);
                                                userpanel.Controls.Add(videoView);

                                            }
                                            break; // Sadece bir tane PictureBox silmek istiyoruz, işlemi sonlandır
                                        }
                                    }
                                    break; // İşlem tamamlandı, döngüyü sonlandır
                                }
                            }
                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        pcbSoundDowloadIcon.Visible = true;
                        MessageBox.Show($"İndirme sırasında bir hata oluştu: {ex.Message}");
                    }
                };

                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(pcbSoundDowloadIcon);
                    }));
                }
                else
                {
                    userpanel.Controls.Add(pcbSoundDowloadIcon);
                }

            }
            else
            {
                string libVlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                Core.Initialize(libVlcPath);
                var libVLC = new LibVLC(libVlcPath);
                var mediaPlayer = new MediaPlayer(libVLC)
                {
                    Media = new Media(libVLC, new Uri(Exit))
                };

                // Video görüntüsünü ekleme
                var videoView = new VideoView
                {
                    MediaPlayer = mediaPlayer,
                    Dock = DockStyle.None,
                    Size = new Size(180, 60),
                    Visible = true,
                };

                if (senderId.ToString() == FrmUserDashboard.chatId)
                {
                    videoView.Location = new Point(150, 0);
                }
                else
                {
                    videoView.Location = new Point(500, 0);
                }

                #region play butonu
                PictureBox PcbPlayButton = new PictureBox
                {
                    Location = new Point(videoView.Left, videoView.Bottom + 10),
                    Size = new Size(50, 20),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };

                System.Drawing.Image PcbPlayButtonImage;
                PcbPlayButtonImage = Properties.Resources.playButton;
                PcbPlayButton.Image = PcbPlayButtonImage;

                PcbPlayButton.Click += (playclick, playEventArgs) =>
                {
                    mediaPlayer.Play();
                };
                #endregion

                #region pause button
                PictureBox pcbPauseButton = new PictureBox
                {
                    Location = new Point(videoView.Left + 50, videoView.Bottom + 10),
                    Size = new Size(50, 20),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };

                System.Drawing.Image PcbPauseButtonImage;
                PcbPauseButtonImage = Properties.Resources.pauseButton;
                pcbPauseButton.Image = PcbPauseButtonImage;

                pcbPauseButton.Click += (pcbPauseclick, PcbPauseButtonEventArgs) =>
                {
                    mediaPlayer.Pause();
                };
                #endregion

                #region stop button
                PictureBox pcbStopButton = new PictureBox
                {
                    Location = new Point(videoView.Left + 100, videoView.Bottom + 10),
                    Size = new Size(50, 20),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                System.Drawing.Image PcbStopButtonImage;
                PcbStopButtonImage = Properties.Resources.stopButton;
                pcbStopButton.Image = PcbStopButtonImage;

                pcbStopButton.Click += (PcbStopButtonclick, PcbStopButtonEventArgs) =>
                {
                    mediaPlayer.Stop();
                };
                #endregion

                if (userpanel.InvokeRequired)
                {
                    userpanel.Invoke(new Action(() =>
                    {
                        userpanel.Controls.Add(PcbPlayButton);
                        userpanel.Controls.Add(pcbPauseButton);
                        userpanel.Controls.Add(pcbStopButton);
                        userpanel.Controls.Add(videoView);

                    }));
                }
                else
                {
                    userpanel.Controls.Add(PcbPlayButton);
                    userpanel.Controls.Add(pcbPauseButton);
                    userpanel.Controls.Add(pcbStopButton);
                    userpanel.Controls.Add(videoView);
                }
            }


            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {

                    userpanel.Controls.Add(pcbSoundShow);
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbSoundShow);
            }
        }
        #endregion

        #region Winrar Ekleme kısmı
        private async void pcbAddWinrar_Click(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);
            ofdData.Filter = "Winrar Files|*.rar;*.zip;";
            ofdData.Title = "Select a winrar File";

            if (ofdData.ShowDialog() == DialogResult.OK)
            {
                //string winrarPath = Path.GetFileName(ofdData.FileName);
                string winrarPath = ofdData.FileName;
                FileInfo fileInfo = new FileInfo(winrarPath);
                long fileSizeInBytes = fileInfo.Length;
                long maxFileSize = 10 * 1024 * 1024;//10 mb gecerse 
                if (winrarPath.Length > 100)
                {
                    MessageBox.Show("Seçilen dosya yolunun uzunluğu 100 karakterden fazla olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (fileSizeInBytes > maxFileSize)
                {
                    MessageBox.Show("Seçilen video dosyası 10 MB'tan büyük. Lütfen daha küçük bir dosya seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string ImageUrl = await FileZilla.addData(ofdData.FileName, user.Data.PhoneNumber);
                    if (!string.IsNullOrEmpty(ImageUrl))
                    {
                        var values = await _messageManager.TGetByIdAndFriendIdAddMessage(user.Data.UserId, int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation, ImageUrl, "Winrar");

                        if (values.Success)
                        {
                            #region ftpye eklenen datanın ismini alma
                            string cleanLocation = ImageUrl.Replace("ftp://", "");
                            string dataName = cleanLocation.Split('/').Last();
                            #endregion
                            string targetDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, $"Data/{user.Data.PhoneNumber}");
                            string targetPath = Path.Combine(targetDirectory, Path.GetFileName(dataName));

                            try
                            {
                                // Dosya hedef dizine kopyalanıyor
                                File.Copy(winrarPath, targetPath, overwrite: true); // 'overwrite: true' mevcut dosyayı üstlenmesi için
                                //MessageBox.Show("Dosya başarıyla kopyalandı: " + targetPath);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Dosya kopyalama hatası: " + ex.Message);
                            }

                            MessageBox.Show("Winrar Başarıyla Eklendi");
                            LastPanelLocation();
                            UserPanel(values.Data.MessageId, user.Data.UserId);
                            AddWinrar(user.Data.UserId, ImageUrl, user.Data.PhoneNumber);
                            MessageReadingme(false);
                            MessageDateTime(DateTime.Now.ToString(), user.Data.UserId.ToString());
                            Motherpanel.ScrollControlIntoView(userpanel);
                            DeleteMessage(user.Data.UserId, values.Data.MessageId);
                            await FrmLogin._connection.InvokeAsync("SendMessage", values.Data.ReceiverId, values.Data.MessageContext, user.Data.UserId, "Winrar", values.Data.MessageId);
                        }
                        else
                        {
                            MessageBox.Show("Hata");
                        }
                    }
                }
            }
        }
        #endregion

        #region karekter ayıracı 400 karekter sonra bosluk bırak
        private string InsertLineBreaks(string text, int characterLimit)
        {
            addedBreaks = 1;
            for (int i = characterLimit; i < text.Length; i += characterLimit)
            {
                text = text.Insert(i, Environment.NewLine);
                i++;
                addedBreaks++;
            }

            return text;
        }
        #endregion

        #region Video Gönderme
        private async void pcbAddMovie_Click(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);

            ofdData.Title = "Video sec";
            ofdData.Filter = "Video Dosyaları (*.mp4;*.avi;*.mov)|*.mp4;*.avi;*.mov";
            if (ofdData.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = ofdData.FileName;
                FileInfo fileInfo = new FileInfo(ofdData.FileName);
                long fileSizeInBytes = fileInfo.Length;
                long maxFileSizeInBytes = 10 * 1024 * 1024; // 10 MB (1024 KB * 1024 byte * 10 MB)

                if (fileSizeInBytes > maxFileSizeInBytes)
                {
                    MessageBox.Show("Seçilen dosya boyutu 10 MB'ı geçemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (selectedFilePath.Length > 100)
                    {
                        MessageBox.Show("Seçilen dosya yolunun uzunluğu 100 karakterden fazla olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        var Reievervalues = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                        string ImageUrl = await FileZilla.addData(ofdData.FileName, Reievervalues.Data.PhoneNumber);
                        if (!string.IsNullOrEmpty(ImageUrl))
                        {
                            var reiceverabout = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                            var values = await _messageManager.TGetByIdAndFriendIdAddMessage(user.Data.UserId, int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation, ImageUrl, "Movie");
                            if (values.Success)
                            {
                                #region ftpye eklenen datanın ismini alma
                                string cleanLocation = ImageUrl.Replace("ftp://", "");
                                string dataName = cleanLocation.Split('/').Last();
                                #endregion
                                string targetDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, $"Data/{user.Data.PhoneNumber}");
                                string targetPath = Path.Combine(targetDirectory, Path.GetFileName(dataName));

                                try
                                {
                                    // Dosya hedef dizine kopyalanıyor
                                    File.Copy(selectedFilePath, targetPath, overwrite: true);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Dosya kopyalama hatası: " + ex.Message);
                                }

                                MessageBox.Show("Video Başarıyla Eklendi");
                                LastPanelLocation();
                                UserPanel(values.Data.MessageId, user.Data.UserId);
                                AddMovie(user.Data.UserId, ImageUrl, user.Data.PhoneNumber, values.Data.MessageId);
                                MessageReadingme(false);
                                MessageDateTime(DateTime.Now.ToString(), user.Data.UserId.ToString());
                                Motherpanel.ScrollControlIntoView(userpanel);
                                DeleteMessage(user.Data.UserId, values.Data.MessageId);
                                await FrmLogin._connection.InvokeAsync("SendMessage", values.Data.ReceiverId, values.Data.MessageContext, user.Data.UserId, "Movie", values.Data.MessageId);
                            }
                            else
                            {
                                MessageBox.Show("Hata");
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Ekran Paylaşımı
        #region ekran paylasımını başlat 
        public static bool s1 = true;
        private async void pcbScrenShareAdd_Click(object sender, EventArgs e)
        {
            s1 = true;
            FrmScreenShare frmScreenShare = new FrmScreenShare();
            frmScreenShare.ShowDialog();
        }
        #endregion

        #region alınan ekran görüntüsü sıkıştırma
        public static byte[] ConvertToJpeg(Bitmap image, int quality)
        {
            using (var ms = new MemoryStream())
            {
                // JPEG formatında sıkıştırmak için EncoderParameters kullanıyoruz
                var encoder = System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders()
                    .FirstOrDefault(codec => codec.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);

                if (encoder != null)
                {
                    var encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                    encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                    // JPEG formatında sıkıştırıyoruz
                    image.Save(ms, encoder, encoderParams);
                }
                return ms.ToArray();
            }
        }
        #endregion

        #region Ekran görüntüsü almak
        public static Bitmap CaptureScreen()
        {
            // Ekranın tümünü yakalayın
            Rectangle bounds = Screen.GetBounds(Point.Empty);  // Ekran boyutları (tam ekran)

            // Ekran çözünürlüğüne uygun bitmap oluşturuyoruz
            Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Ekran görüntüsünü tam ekran çözünürlüğe alıyoruz
                g.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
            }

            // 144p çözünürlüğe boyutlandırma (256x144)
            int targetWidth = 256;  // 144p genişlik
            int targetHeight = 256; // 144p yükseklik
            Bitmap resizedBitmap = new Bitmap(bitmap, new Size(targetWidth, targetHeight));  // Resmi yeniden boyutlandırıyoruz

            return resizedBitmap;  // 144p çözünürlüğünde resmi döndürüyoruz
        }
        #endregion

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            FrmScreenShare frmScreenShare = new FrmScreenShare();
            frmScreenShare.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(isMuted)
            {
                isMuted = false;
            }
            else
            {
                isMuted = true;
            }
        }

        private async void pcbRealTimeSound_Click(object sender, EventArgs e)
        {
            await FrmLogin._connection.InvokeAsync("CallUser",int.Parse(FrmUserDashboard.chatId));
        }
        #region user paneli yani   kullanici paneli
        public void UserPanel(int messageId, int SenderId)
        {
            lastMessageId++;
            userpanel = new Panel();

            if (Motherpanel.AutoScrollPosition.Y != 0)
            {
                userpanel.Size = new Size(936, 0);  // Eğer scroll varsa, 916 yapıyoruz
                userpanel.MaximumSize = new Size(936, 0); // Genişlik sınırlaması
            }
            else
            {
                userpanel.MaximumSize = new Size(936, 0); // Genişlik sınırlaması
                userpanel.Size = new Size(936, 0);  // Scroll yoksa 936 yapıyoruz
            }

            userpanel.Location = new Point(0, lastlocation);
            userpanel.BorderStyle = BorderStyle.FixedSingle;
            userpanel.AutoSize = true;

            userpanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            userpanel.Name = messageId.ToString();

            if (SenderId.ToString() != FrmUserDashboard.chatId)
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
        }
        #endregion

        #region Timer zaman tutucu
        private void timer_Tick(object sender, EventArgs e)
        {
            Soundtimer++;
            int minutes = Soundtimer / 60;
            int seconds = Soundtimer % 60;
            lbl_timer.Text = minutes + ":" + seconds; // Örneğin 02:05 formatında
            if (Soundtimer == 300)
            {
                MessageBox.Show("5 dk dan fazla ses kaydi alamazsiniz anlayışınız için teşekkür ederiz.");
                pcbSoundAdd_Click(sender, e);
            }
        }
        #endregion

        #region Ses Kaydedici butonu
        private async void pcbSoundAdd_Click(object sender, EventArgs e)
        {
            var user = userManager.TGetByToken(FrmLogin.userInformation);
            if (SoundAddButton == false)
            {
                pcbSoundAdd.BackColor = Color.Red;
                lbl_timer.Visible = true;
                Soundtimer = 0;
                timer.Start();

                SoundFileName = "recond_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".wav";
                outputFileName = Path.Combine(System.Windows.Forms.Application.StartupPath, "Data", user.Data.PhoneNumber, SoundFileName);

                if (waveIn == null)
                {
                    waveIn = new WaveInEvent();
                    waveIn.DeviceNumber = 0; // Varsayılan mikrofonu kullan
                    waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1kHz, mono

                    waveFileWriter = new WaveFileWriter(outputFileName, waveIn.WaveFormat);

                    waveIn.DataAvailable += (s, a) =>
                    {
                        waveFileWriter.Write(a.Buffer, 0, a.BytesRecorded);
                    };

                    waveIn.RecordingStopped += (s, a) =>
                    {
                        waveFileWriter.Dispose();
                        waveFileWriter = null;
                        waveIn.Dispose();
                        waveIn = null;
                    };

                    waveIn.StartRecording();
                    //MessageBox.Show("Kayda başlandı.");
                }
                SoundAddButton = true;
            }
            else
            {
                pcbSoundAdd.BackColor = Color.DarkGray;
                lbl_timer.Visible = false;
                timer.Stop();

                if (waveIn != null)
                {
                    waveIn.StopRecording();

                    // Kullanıcıya dosyayı kaydetmek isteyip istemediğini sor
                    DialogResult result = MessageBox.Show("Kayıt tamamlandı. Dosyayı kaydetmek istiyor musunuz?", "Kayıt Tamamlandı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        var Reievervalues = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                        string SoundUrl = await FileZilla.addData(outputFileName, Reievervalues.Data.PhoneNumber);
                        if (!string.IsNullOrEmpty(SoundUrl))
                        {
                            var reiceverabout = await userManager.TGetByIdFriend(int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation);
                            var values = await _messageManager.TGetByIdAndFriendIdAddMessage(user.Data.UserId, int.Parse(FrmUserDashboard.chatId), FrmLogin.userInformation, SoundUrl, "Sound");
                            if (values.Success)
                            {
                                #region ftpye eklenen datanın ismini alma
                                string cleanLocation = SoundUrl.Replace("ftp://", "");
                                string dataName = cleanLocation.Split('/').Last();
                                #endregion
                                string targetDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, $"Data/{user.Data.PhoneNumber}");
                                string targetPath = Path.Combine(targetDirectory, Path.GetFileName(dataName));

                                MessageBox.Show("Ses Kaydı Başarıyla Eklendi");
                                LastPanelLocation();
                                UserPanel(values.Data.MessageId, user.Data.UserId);
                                AddSound(user.Data.UserId, SoundUrl, user.Data.PhoneNumber, values.Data.MessageId);
                                MessageReadingme(false);
                                MessageDateTime(DateTime.Now.ToString(), user.Data.UserId.ToString());
                                Motherpanel.ScrollControlIntoView(userpanel);
                                DeleteMessage(user.Data.UserId, values.Data.MessageId);
                                await FrmLogin._connection.InvokeAsync("SendMessage", values.Data.ReceiverId, values.Data.MessageContext, user.Data.UserId, "Sound", values.Data.MessageId);
                            }
                            else
                            {
                                MessageBox.Show("Hata");
                            }
                        }
                    }
                    else
                    {
                        // Kullanıcı kaydetmek istemiyor, dosyayı sil
                        if (System.IO.File.Exists(outputFileName))
                        {
                            System.IO.File.Delete(outputFileName);
                        }
                        MessageBox.Show("Kayıt silindi.");
                    }

                }
                SoundAddButton = false;

            }

        }
        #endregion

        #region son eklenen panelin locationu
        public void LastPanelLocation()
        {
            if (Motherpanel.Controls.Count > 0)
            {
                // Son eklenen panel (son kontrol)
                Panel lastPanel = (Panel)Motherpanel.Controls[Motherpanel.Controls.Count - 1];

                // Son eklenen panelin konumu
                Point lastPanelLocation = lastPanel.Location;
                lastlocation = lastPanelLocation.Y;
                lastlocation += userpanel.Height + 10;
            }
            else
            {
                lastlocation = 0;
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

        #region Mesaj Silme
        private void DeleteMessage(int senderId, int messageId)
        {
            PictureBox pcbDeleteImage = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbDeleteImage.Location = new Point(60, 10);
            }
            else
            {
                pcbDeleteImage.Location = new Point(810, 10);
            }

            pcbDeleteImage.Size = new Size(50, 30);
            pcbDeleteImage.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Image deleteIcon;
            deleteIcon = Properties.Resources.sil;
            pcbDeleteImage.Image = deleteIcon;
            #region delete butonuna tıklanma olayı
            pcbDeleteImage.Click += async (s, e) =>
            {
                DialogResult result = MessageBox.Show("Mesajı silmek istediğinizden emin misiniz?",
                                                      "Mesaj Silme",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {

                    bool isDeleted = await _messageManager.TDeleteMessageId(messageId, senderId, FrmLogin.userInformation); // Örneğin bir veritabanı fonksiyonu

                    if (isDeleted)
                    {
                        foreach (Control control in Motherpanel.Controls)
                        {
                            if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                            {
                                // userpanel'i bulduktan sonra silme işlemi
                                if (userpanel.InvokeRequired)
                                {
                                    userpanel.Invoke(new Action(() =>
                                    {
                                        Motherpanel.Controls.Remove(userpanel);
                                        userpanel.Dispose();  // Hafıza temizliği için dispose çağrısı
                                    }));
                                }
                                else
                                {
                                    Motherpanel.Controls.Remove(userpanel);
                                    userpanel.Dispose();  // Hafıza temizliği için dispose çağrısı
                                }
                                break; // İşlem tamamlandı, döngüyü sonlandır
                            }
                        }
                    }
                }


            };
            #endregion
            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(pcbDeleteImage);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbDeleteImage);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region Mesaj düzenleme
        private void EditMessage(int senderId, int messageId)
        {
            PictureBox pcbEditImage = new PictureBox();
            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                pcbEditImage.Location = new Point(100, 10);
            }
            else
            {
                pcbEditImage.Location = new Point(760, 10);
            }

            pcbEditImage.Size = new Size(50, 30);
            pcbEditImage.SizeMode = PictureBoxSizeMode.StretchImage;
            System.Drawing.Image editIcon;
            editIcon = Properties.Resources.yaziyi_duzelt;
            pcbEditImage.Image = editIcon;
            #region edit butonuna tıklanma olayı
            pcbEditImage.Click += (s, e) =>
            {
                foreach (Control control in Motherpanel.Controls)
                {
                    if (control is Panel userpanel && (string)userpanel.Name == messageId.ToString())
                    {
                        // userpanel'i bulduktan sonra düzenleme işlemi
                        var label = userpanel.Controls.OfType<System.Windows.Forms.Label>().FirstOrDefault();
                        if (label != null)
                        {
                            txtAddMessage.Text = label.Text;
                            MessageId = messageId;
                            MessageEdit = true;
                            break; // İşlem tamamlandı, döngüyü sonlandır
                        }

                    }
                }
            };
            #endregion
            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(pcbEditImage);  // UI işlemi ana iş parçacığından yapılır
                }));
            }
            else
            {
                userpanel.Controls.Add(pcbEditImage);  // Eğer ana iş parçacığındaysanız direkt işlem yapabilirsiniz
            }
        }
        #endregion

        #region vlc media player
        public void vlcPlayer(string movielocation, int senderId)
        {

            string libVlcPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            Core.Initialize(libVlcPath);
            var libVLC = new LibVLC(libVlcPath);
            var mediaPlayer = new MediaPlayer(libVLC)
            {
                Media = new Media(libVLC, new Uri(movielocation))
            };

            // Video görüntüsünü ekleme
            var videoView = new VideoView
            {
                MediaPlayer = mediaPlayer,
                Dock = DockStyle.None,

                Size = new Size(180, 180),
                Visible = true,
            };

            if (senderId.ToString() == FrmUserDashboard.chatId)
            {
                videoView.Location = new Point(150, 0);
            }
            else
            {
                videoView.Location = new Point(500, 0);
            }

            #region play butonu
            PictureBox PcbPlayButton = new PictureBox
            {
                Location = new Point(videoView.Left, videoView.Bottom + 10),
                Size = new Size(50, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            System.Drawing.Image PcbPlayButtonImage;
            PcbPlayButtonImage = Properties.Resources.playButton;
            PcbPlayButton.Image = PcbPlayButtonImage;

            PcbPlayButton.Click += (playclick, playEventArgs) =>
            {
                mediaPlayer.Play();
            };
            #endregion

            #region pause button
            PictureBox pcbPauseButton = new PictureBox
            {
                Location = new Point(videoView.Left + 50, videoView.Bottom + 10),
                Size = new Size(50, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };

            System.Drawing.Image PcbPauseButtonImage;
            PcbPauseButtonImage = Properties.Resources.pauseButton;
            pcbPauseButton.Image = PcbPauseButtonImage;

            pcbPauseButton.Click += (pcbPauseclick, PcbPauseButtonEventArgs) =>
            {
                mediaPlayer.Pause();
            };
            #endregion

            #region stop button
            PictureBox pcbStopButton = new PictureBox
            {
                Location = new Point(videoView.Left + 100, videoView.Bottom + 10),
                Size = new Size(50, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            System.Drawing.Image PcbStopButtonImage;
            PcbStopButtonImage = Properties.Resources.stopButton;
            pcbStopButton.Image = PcbStopButtonImage;

            pcbStopButton.Click += (PcbStopButtonclick, PcbStopButtonEventArgs) =>
            {
                mediaPlayer.Stop();
            };
            #endregion

            #region FullScreen Butonu
            PictureBox pcbFullScreenButton = new PictureBox
            {
                Location = new Point(videoView.Left + 150, videoView.Bottom + 10),
                Size = new Size(50, 20),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent
            };
            System.Drawing.Image PcbFullScreenButtonImage;
            bool statusFullScreen = false;
            PcbFullScreenButtonImage = Properties.Resources.fullScreenButton;
            pcbFullScreenButton.Image = PcbFullScreenButtonImage;

            pcbFullScreenButton.Click += (PcbFullScreenButtonclick, PcbFullScreenButtonEventArgs) =>
            {
                // Video'yu tam ekran yapma:
                if (!statusFullScreen)
                {
                    videoView.Dock = DockStyle.Fill;  // Video view'i formun tüm alanına yay
                    statusFullScreen = true;
                }
                else
                {
                    videoView.Dock = DockStyle.None;
                    statusFullScreen = false;
                }

            };
            #endregion

            if (userpanel.InvokeRequired)
            {
                userpanel.Invoke(new Action(() =>
                {
                    userpanel.Controls.Add(PcbPlayButton);
                    userpanel.Controls.Add(pcbPauseButton);
                    userpanel.Controls.Add(pcbStopButton);
                    userpanel.Controls.Add(pcbFullScreenButton);
                    userpanel.Controls.Add(videoView);

                }));
            }
            else
            {
                userpanel.Controls.Add(pcbFullScreenButton);
                userpanel.Controls.Add(PcbPlayButton);
                userpanel.Controls.Add(pcbPauseButton);
                userpanel.Controls.Add(pcbStopButton);
                userpanel.Controls.Add(videoView);

            }
        }
        #endregion

        #region gelen mesajları okudum
        public async void MessageReading(int messageId)
        {
            var values = await _messageManager.TUpdateReading(messageId, FrmLogin.userInformation);
        }
        #endregion
    }
}





