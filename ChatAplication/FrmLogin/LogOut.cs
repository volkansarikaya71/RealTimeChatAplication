using ChatApi.BusinessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{

    public  class LogOut
    {


        public static async Task FormClosing(object sender, FormClosingEventArgs e)
        {
            UserManager _userManager = new UserManager();

            DialogResult result = MessageBox.Show("Uygulamayı kapatmak istiyor musunuz?", "Uygulama Kapatma", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                // Kapatma işlemini iptal et
                e.Cancel = true;
            }
            else
            {
                await PerformLogout();
            }
        }

        public static async Task PerformLogout()
        {
            UserManager _userManager = new UserManager();
            var values = await _userManager.LogOut(FrmLogin.userInformation);
            if(values)
            {
                string resultMessage = await Signalr.SignalrLogOut();
                if (resultMessage == "Çıkış işlemi başarılı") // Sunucudan dönen başarılı mesaj
                {
                    MessageBox.Show(resultMessage);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(resultMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
    }
}
