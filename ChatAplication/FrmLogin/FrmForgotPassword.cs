using ChatApi.BusinessLayer.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmForgotPassword : Form
    {
        public FrmForgotPassword()
        {
            InitializeComponent();
        }

        UserManager _userManager = new UserManager();

        private void imgFrmUserLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private async void pcbAddUserToken_Click(object sender, EventArgs e)
        {
            var values = await _userManager.TGetByEmail(txtUserEmail.Text, " ", " ");
            if (values.Success)
            {
                lblUserpassword.Visible = true;
                lblUserToken.Visible = true;
                txtUserPassword.Visible = true;
                txtUserToken.Visible = true;
                pcbUserPasswordUpdate.Visible = true;
                pcbUserPasswordShoworHide.Visible = true;
                lblUserMail.Visible = false;
                txtUserEmail.Visible = false;
                pcbAddUserToken.Visible = false;
            }
            else
            {
                MessageBox.Show(values.ErrorMessage);
            }
        }

        private async void pcbUserPasswordUpdate_Click(object sender, EventArgs e)
        {
            var values = await _userManager.TGetByEmail(txtUserEmail.Text, txtUserToken.Text, txtUserPassword.Text);
            MessageBox.Show(values.ErrorMessage);
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
