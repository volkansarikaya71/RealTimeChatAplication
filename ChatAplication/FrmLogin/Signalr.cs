using ChatApi.BusinessLayer.Concrete;
using FrmLogin.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmLogin
{

    public static class Signalr
    {

        public static TaskCompletionSource<bool> loginCompletionSource;


        #region kullanici çıkış işlemi
        public static async Task<string> SignalrLogOut()
        {

            var logoutCompletionSource = new TaskCompletionSource<string>();
            bool isMessageHandled = false;
            try
            {
                // LogoutSuccess mesajını dinliyoruz
                FrmLogin._connection.On<string>("LogoutSuccess", (message) =>
                {
                    if (!isMessageHandled) // Mesaj daha önce işlenmediyse
                    {
                        logoutCompletionSource.SetResult(message);
                        isMessageHandled = true; // Mesaj işlendi olarak işaretle
                    }
                });

                // Sunucuya Logout isteği gönderiyoruz
                await FrmLogin._connection.InvokeAsync("LogoutUser", FrmLogin.userInformation);

                string logoutMessage = await logoutCompletionSource.Task;
                // Logout işlemi başarılı olana kadar bekliyoruz
                await logoutCompletionSource.Task;

                // Kullanıcıyı çıkartıyoruz
                FrmLogin.Users.TryRemove(FrmLogin._connection.ConnectionId, out string userId);

                // Bağlantıyı sonlandırıyoruz
                await FrmLogin._connection.StopAsync();

                // Başarılı olduğuna dair geri dönüş yapıyoruz
                return logoutMessage;
            }
            catch (Exception ex)
            {
                // Hata durumunda kullanıcıya bilgi ver ve false döndür
                return $"Çıkış işlemi sırasında bir hata oluştu: {ex.Message}";
            }
            finally
            {
                // Gereksiz kaynakları serbest bırakmak için ek temizleme işlemleri yapılabilir.
                FrmLogin._connection.Remove("LogoutSuccess"); // Dinleme işlemini kaldır
            }
        }
        #endregion

        #region Kullanicinin bağlantısı koptuğunda

        #endregion

    }

}
