using BusinessLayer.Models;
using ChatApi.BusinessLayer.Concrete;
using FrmLogin.Models;
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
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FrmLogin
{
    public partial class FrmCreateUser : Form
    {
        public FrmCreateUser()
        {
            InitializeComponent();
        }

        UserManager _userManager = new UserManager();

        private void pxbUserAddImage_Click(object sender, EventArgs e)
        {
            ofAddUserImage.Title = "Resim sec";
            ofAddUserImage.Filter = "PNG Dosyaları (*.png)|*.png";
            if (ofAddUserImage.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = Path.GetFileName(ofAddUserImage.FileName);
                if (selectedFilePath.Length > 100)
                {
                    MessageBox.Show("Seçilen dosya yolunun uzunluğu 100 karakterden fazla olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pxbUserAddImage.ImageLocation = ofAddUserImage.FileName;
                }

            }
        }

        private void txtUserPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void imgFrmUserLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private async void pcbCreateUser_Click(object sender, EventArgs e)
        {
            pcbCreateUser.Visible = false;
            imgFrmUserLogin.Visible = false;
            pxbUserAddImage.Enabled = false;
            var userViewModel = new UsersViewModel
            {
                UserName = txtUserName.Text,
                Password = txtUserPassword.Text,
                Email = txtUserEmail.Text,
                PhoneNumber = txtUserPhoneNumber.Text,
                UserImage = ofAddUserImage.FileName,
                UserStatus=false,
            };

            var values = await _userManager.TAdd(userViewModel);

            if (values.Success)
            {
                var dialogResult = MessageBox.Show("Kullanıcı başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.OK)
                {

                    FrmLogin frmLogin = new FrmLogin();
                    frmLogin.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show(values.ErrorMessage);
                pcbCreateUser.Visible = true;
                imgFrmUserLogin.Visible = true;
            }
            
        }

        private void pcbUserPasswordShoworHide_Click(object sender, EventArgs e)
        {
            if (txtUserPassword.UseSystemPasswordChar == false)
            {
                txtUserPassword.UseSystemPasswordChar = true;
                pcbUserPasswordShoworHide.Image = Properties.Resources.goster;
                txtUserPassword.PasswordChar = '*';
            }
            else
            {
                txtUserPassword.UseSystemPasswordChar = false;
                pcbUserPasswordShoworHide.Image = Properties.Resources.gizle;
                txtUserPassword.PasswordChar = '\0';
            }
        }

    }
}
