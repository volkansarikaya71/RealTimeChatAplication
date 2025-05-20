using ChatApi.BusinessLayer.Concrete;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        UserManager _userManager = new UserManager();
        public static string userInformation;
        public static HubConnection _connection;

        public static TaskCompletionSource<bool> loginCompletionSource;
        public static ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();

        public static string UserId;


        private void txtUserPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pcbUserPasswordShoworHide_Click(object sender, EventArgs e)
        {
            if (txtUserPassword.UseSystemPasswordChar == false)
            {
                txtUserPassword.UseSystemPasswordChar = true;
                pcbUserPasswordShoworHide.Image = Properties.Resources.haydoClose;
                txtUserPassword.PasswordChar = '*';
            }
            else
            {
                txtUserPassword.UseSystemPasswordChar = false;
                pcbUserPasswordShoworHide.Image = Properties.Resources.haydoShow;
                txtUserPassword.PasswordChar = '\0';
            }
        }

        private void lnkUserCreate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmCreateUser frmCreateUser = new FrmCreateUser();
            frmCreateUser.Show();
            this.Hide();
        }

        private void pcbUserCreate_Click(object sender, EventArgs e)
        {
            FrmCreateUser frmCreateUser = new FrmCreateUser();
            frmCreateUser.Show();
            this.Hide();
        }

        private async void pcbLogin_Click(object sender, EventArgs e)
        {
            pcbLogin.Visible = false;
            var values = await _userManager.TGetByPhoneAndPassword(txtUserPhoneNumber.Text, txtUserPassword.Text);
            if (values.Success)
            {
                var tokenGetById = _userManager.TGetByToken(values.Data.Token);

                loginCompletionSource = new TaskCompletionSource<bool>();
                UserId = tokenGetById.Data.UserId.ToString();
                await _connection.InvokeAsync("LoginUser", tokenGetById.Data.UserId.ToString(), values.Data.Token);

                // LoginSuccess veya ErrorMessage yanıtını bekle
                bool loginResult = await loginCompletionSource.Task;

                if (loginResult)
                {
                    userInformation = values.Data.Token;
                    FrmUserDashboard frmUserDashboard = new FrmUserDashboard();
                    frmUserDashboard.Show();
                    this.Hide();
                }

            }
            else
            {
                MessageBox.Show(values.ErrorMessage);
                pcbLogin.Visible = true;
            }
        }

        private void lnkUserUpdate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmForgotPassword frmForgotPassword = new FrmForgotPassword();
            frmForgotPassword.Show();
            this.Hide();
        }

        private void pcbUserUpdate_Click(object sender, EventArgs e)
        {
            FrmForgotPassword frmForgotPassword = new FrmForgotPassword();
            frmForgotPassword.Show();
            this.Hide();
        }


        private async void FrmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                if (_connection == null)
                {
                    // Bağlantıyı oluşturuyoruz
                    _connection = new HubConnectionBuilder()
                        .WithUrl("https://localhost:7056/login")
                        .WithAutomaticReconnect() // Otomatik yeniden bağlantı
                        .Build();
                    _connection.On<string>("LoginSuccess", (userId) =>
                    {
                        if (userId == UserId) // Doğrulama yapılabilir
                        {
                            var connectionId = _connection.ConnectionId;
                            Users[connectionId] = userId;
                            //MessageBox.Show($"Giriş başarılı! Connection ID: {Users[connectionId]}");
                            loginCompletionSource?.SetResult(true);
                        }
                    });

                    _connection.On<string>("ErrorMessage", (errorMessage) =>
                    {
                        MessageBox.Show($"Hata: {errorMessage}");
                        loginCompletionSource?.SetResult(false);
                    });
                }

                if (_connection.State == HubConnectionState.Disconnected)
                {
                    await TryConnectAsync();

                }
                else
                {
                    this.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bağlantı hatası: {ex.Message}");
            }

        }

        private async Task TryConnectAsync()
        {
            try
            {
                await _connection.StartAsync();
                //MessageBox.Show($"Connection ID: {_connection.ConnectionId}");
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                var result = MessageBox.Show($"Bağlantı hatası: {ex.Message}. Tekrar denemek ister misiniz?",
                                             "Bağlantı Hatası",
                                             MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    await TryConnectAsync(); // Yeniden dene
                }
                else
                {
                    Application.Exit();
                }
            }

        }
    }
}
