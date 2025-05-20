using ChatApi.BusinessLayer.Concrete;
using ChatApi.DAL;
using FrmLogin.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Xml.Linq;

namespace FrmLogin
{
    public partial class FrmUpdateUser : Form
    {
        public FrmUpdateUser()
        {
            InitializeComponent();
        }

        UserManager _userManager = new UserManager();
        public string Image;
        public bool ImageChanging=false;
        private void imgFrmUserDashboard_Click(object sender, EventArgs e)
        {
            FrmUserDashboard frmUserDashboard = new FrmUserDashboard();
            frmUserDashboard.Show();
            this.Hide();
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

        private  void FrmUpdateUser_Load(object sender, EventArgs e)
        {
            var values =  _userManager.TGetByToken(FrmLogin.userInformation);
            txtUserName.Text = values.Data.UserName;
            txtUserPassword.Text = values.Data.Password;
            txtUserEmail.Text = values.Data.Email;
            txtUserPhoneNumber.Text = values.Data.PhoneNumber;
            Image = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "Image", Path.GetFileName(values.Data.UserImage));
            pcbUserImage.ImageLocation = Image;

        }

        private void pcbUserImage_Click(object sender, EventArgs e)
        {
            ofUserImage.Title = "Resim sec";
            ofUserImage.Filter = "PNG Dosyaları (*.png)|*.png";
            if (ofUserImage.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = Path.GetFileName(ofUserImage.FileName);
                if (selectedFilePath.Length > 100)
                {
                    MessageBox.Show("Seçilen dosya yolunun uzunluğu 100 karakterden fazla olamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    pcbUserImage.ImageLocation = ofUserImage.FileName;
                    Image = ofUserImage.FileName;
                    ImageChanging = true;
                }

            }
            else
            {
                pcbUserImage.ImageLocation = Image;
                ImageChanging = false;
            }
        }

        private async void pcbUpdateUser_Click(object sender, EventArgs e)
        {
            pcbUpdateUser.Visible = false;
            imgFrmUserDashboard.Visible = false;
            pcbUserImage.Enabled = false;
            var userdata = _userManager.TGetByToken(FrmLogin.userInformation);
            var userViewModel = new UsersViewModel
            {
                UserId=userdata.Data.UserId,
                Token= FrmLogin.userInformation,
                UserName = txtUserName.Text,
                Password = txtUserPassword.Text,
                Email = txtUserEmail.Text,
                PhoneNumber = txtUserPhoneNumber.Text,
                UserImage = userdata.Data.UserImage,
                UserStatus=true,      
            };

            var values = await _userManager.TUpdate(userViewModel);

            if (values.Success)
            {
                await FrmLogin._connection.InvokeAsync("UserUpdate", userViewModel);
                if (ImageChanging)
                {
                    string ImageUrl = await FileZilla.addData(Image, userViewModel.PhoneNumber);
                    userViewModel.UserImage = ImageUrl;

                    var Updatevalues = await _userManager.TUpdate(userViewModel);
                    if (Updatevalues.Success)
                    {
                        await FrmLogin._connection.InvokeAsync("UserUpdate", userViewModel);
                        FrmLogin.userInformation = userViewModel.Token;
                        MessageBox.Show("Bilgilerini Güncellendi");
                    }
                    else
                    {
                        MessageBox.Show(values.ErrorMessage);
                    }

                }
                else
                {
                    MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi!", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmLogin.userInformation = userViewModel.Token;
                }

            }
            else
            {
                MessageBox.Show(values.ErrorMessage);
            }
            pcbUpdateUser.Visible = true;
            imgFrmUserDashboard.Visible = true;
            pcbUserImage.Enabled = true;
        }
    }
}
