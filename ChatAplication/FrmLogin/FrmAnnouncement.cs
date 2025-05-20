using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmAnnouncement : Form
    {
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private Timer timer;

        public FrmAnnouncement()
        {
            InitializeComponent();
            Core.Initialize(); // LibVLC'yi başlat

            // LibVLC Nesnesini Başlat
            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            // VideoView ile MediaPlayer'ı Bağla
            vwmovie.MediaPlayer = _mediaPlayer;


            #region Timer
            timer = new Timer();
            timer.Interval = 5000; // 5 saniye (5000 ms)
            timer.Tick += Timer_Tick; // Tick olayına fonksiyon bağla
            timer.Start(); // Timer'ı başlat
            #endregion
            #region Formu Ekranın Orta-Üstünde Açma
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new System.Drawing.Point(
                (Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, // Ekranın ortası
                0);
            #endregion
        }

        private void FrmAnnouncement_Load(object sender, EventArgs e)
        {
            string videoPath = Path.Combine(Application.StartupPath, "Data", "haydoBildirim.mp4");
            _mediaPlayer.Media = new Media(_libVLC, new Uri(videoPath));
            _mediaPlayer.Play(); // Doğrudan Play metodu çağrılır
            lblUserName.Text = FrmUserDashboard.userName+ ": Kullanıcısından Yeni Mesaj";

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop(); // Timer'ı durdur
            this.Close(); // Formu kapat
        }
    }
}
