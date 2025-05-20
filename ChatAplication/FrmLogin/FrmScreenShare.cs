using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmScreenShare : Form
    {
        public FrmScreenShare()
        {
            InitializeComponent();
        }

        private async void FrmScreenShare_Load(object sender, EventArgs e)
        {
            #region Ekran Paylaşımı

            // SignalR üzerinden gelen veriyi işleme
            FrmLogin._connection.On<byte[]>("ReceiveScreenCapture", (imageBytes) =>
            {
                // UI thread'ine erişim için Invoke kullanmamız gerekiyor
                if (pcbscreen.InvokeRequired)
                {
                    pcbscreen.Invoke(new Action(() =>
                    {
                        // Gelen byte dizisini bir stream'e çeviriyoruz
                        using (var stream = new MemoryStream(imageBytes))
                        {
                            var bitmap = new Bitmap(stream);

                            // PictureBox'ın SizeMode'unu ayarlıyoruz
                            pcbscreen.SizeMode = PictureBoxSizeMode.Zoom; // Veya PictureBoxSizeMode.Zoom

                            // Bitmap'i PictureBox'a yüklüyoruz
                            pcbscreen.Image = new Bitmap(bitmap, new Size(pcbscreen.Width, pcbscreen.Height));  // Resmi yeniden boyutlandırıp yerleştiriyoruz
                        }
                    }));
                }
                else
                {
                    // Eğer UI thread'inde çalışıyorsak direkt olarak işlemi yapabiliriz
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        var bitmap = new Bitmap(stream);

                        // PictureBox'ın SizeMode'unu ayarlıyoruz
                        pcbscreen.SizeMode = PictureBoxSizeMode.Zoom; // Veya PictureBoxSizeMode.Zoom

                        // Bitmap'i PictureBox'a yüklüyoruz
                        pcbscreen.Image = new Bitmap(bitmap, new Size(pcbscreen.Width, pcbscreen.Height));  // Resmi yeniden boyutlandırıp yerleştiriyoruz
                    }
                }
            });
            #endregion
            if (FrmChatHub.s1)
            {
                #region ekran paylasımını başlat 
                while (FrmChatHub.s1)
                {
                    // Ekran görüntüsünü al
                    Bitmap screenshot = CaptureScreen();

                    // Görüntüyü JPEG formatında sıkıştır
                    byte[] imageBytes = ConvertToJpeg(screenshot, 90);  // %90 kalite ile JPEG sıkıştırma

                    // SignalR ile sunucuya veri gönder
                    await FrmLogin._connection.InvokeAsync("SendScreenCapture", imageBytes, int.Parse(FrmUserDashboard.chatId));

                    // 1 saniye bekleyerek sürekli veri gönder
                    await Task.Delay(500); // 1 saniye arayla veri gönder
                }
                #endregion
            }


        }
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



        private void button1_Click(object sender, EventArgs e)
        {
            FrmChatHub.s1 = false;
            //this.Close();
        }
    }
}
