using ChatApi.BusinessLayer.Concrete;
using ChatApi.DataAccessLayer.EntityFramework;
using ChatApi.EntityLayer;
using ChatApi.Models;
using deneme3.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Text;

namespace ChatApi.Hubs
{
    public class Login : Hub
    {
        private readonly HttpClient _httpClient;
        //private static readonly ConcurrentDictionary<string, string> Users = new ConcurrentDictionary<string, string>();
        private static readonly ConcurrentDictionary<string, (string userId, string token)> Users = new ConcurrentDictionary<string, (string, string)>();
        private readonly ILogger<UserSignInViewModel> _logger;

        public Login(HttpClient httpClient, ILogger<UserSignInViewModel> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        #region kullanici bilgilerini güncelleme
        public async Task<Result<string>> UserUpdate(User user)
        {
            var connectionId = Context.ConnectionId;
            var friendAboutViewModel = new FriendAboutViewModel
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserImage = user.UserImage,
                UserStatus = user.UserStatus,
                UserLastOnlineDate = user.UserLastOnlineDate,
            };
            Users[connectionId] = (user.UserId.ToString(), user.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/api/Users/userGetlistWithFriendIdList/{user.UserId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var friendIds = JsonConvert.DeserializeObject<List<int>>(content);

                foreach (var friendId in friendIds)
                {

                    var friendConnectionId = Users.FirstOrDefault(u => u.Value.userId == friendId.ToString()).Key; // Arkadaşın bağlantı ID'sini alıyoruz


                    if (!string.IsNullOrEmpty(friendConnectionId))
                    {
                        await Clients.Client(friendConnectionId).SendAsync("MessageFromUserAboutUpdate", friendAboutViewModel);
                        _logger.LogInformation($"Mesaj gönderildi: {friendAboutViewModel.UserId} -> {friendId} >>>>> {friendAboutViewModel.UserName}");
                    }
                }
            }

            return new Result<string>("user");
        }
        #endregion

        #region kullanıci giriş işlemi
        // Kullanıcı oturum açtığında çağrılır
        public async Task<Result<string>> LoginUser(string userId, string token)
        {

            var connectionId = Context.ConnectionId;
            //if (Users.Values.Contains(userId))
            if (Users.Values.Any(u => u.userId == userId))
            {
                // Kullanıcı zaten giriş yapmışsa, hata mesajı gönderiyoruz
                await Clients.Caller.SendAsync("ErrorMessage", "Hesabınız Açık bulunmaktadır");
                return new Result<string>("Hesabınız Açık bulunmaktadır");
            }

            Users[connectionId] = (userId, token);

            _logger.LogInformation($"Kullanıcı giriş yaptı:{userId}, ConnectionId: {connectionId}");

            await Clients.Caller.SendAsync("LoginSuccess", userId);
            //await Clients.All.SendAsync("UserJoined", userId);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/api/Users/userGetlistWithFriendIdList/{userId}");

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var friendIds = JsonConvert.DeserializeObject<List<int>>(content);

                foreach (var friendId in friendIds)
                {
                    var friendConnectionId = Users.FirstOrDefault(u => u.Value.userId == friendId.ToString()).Key; // Arkadaşın bağlantı ID'sini alıyoruz


                    if (!string.IsNullOrEmpty(friendConnectionId))
                    {
                        await Clients.Client(friendConnectionId).SendAsync("MessageFromLogin", userId);
                    }
                }
            }

            return new Result<string>(connectionId);
        }
        #endregion

        #region kullanici Çıkış işlemi
        public async Task<Result<string>> LogoutUser(string token)
        {
            var connectionId = Context.ConnectionId;

            // Kullanıcı çıkış yaparsa, Users dictionary'sine kullanıcı bilgilerini ekliyoruz
            if (Users.ContainsKey(connectionId))
            {
                // Kullanıcı zaten mevcutsa, çıkış işlemini yapıyoruz
                var userId = Users[connectionId].userId;

                // Kullanıcıyı dictionary'den çıkartıyoruz
                Users.TryRemove(connectionId, out _);

                _logger.LogInformation($"Kullanıcı çıkış yaptı: {userId}, ConnectionId: {connectionId}");

                // Çıkış yapan kullanıcıyı bilgilendiriyoruz
                await Clients.Caller.SendAsync("LogoutSuccess", "Çıkış işlemi başarılı");


                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/api/Users/userGetlistWithFriendIdList/{userId}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    var friendIds = JsonConvert.DeserializeObject<List<int>>(content);


                    foreach (var friendId in friendIds)
                    {
                        var friendConnectionId = Users.FirstOrDefault(u => u.Value.userId == friendId.ToString()).Key;

                        if (!string.IsNullOrEmpty(friendConnectionId))
                        {
                            await Clients.Client(friendConnectionId).SendAsync("MessageFromLogOut", userId);
                        }
                    }
                }
                else
                {
                    _logger.LogError("Arkadaş listesi alınırken hata oluştu.");
                }
            }
            else
            {

                await Clients.Caller.SendAsync("ErrorMessage", "Henüz giriş yapmadınız veya bağlantınız kesildi.");
            }

            return new Result<string>(connectionId);
        }

        #endregion

        #region Kullanicinin bağlantısı koptuğunda

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;

            // Kullanıcı bağlantısı koparsa ve kullanıcı zaten varsa, bilgileri alıyoruz
            if (Users.TryRemove(connectionId, out var userId))
            {
                _logger.LogInformation($"Kullanıcı bağlantısı kesildi: {userId.userId}, ConnectionId: {connectionId}");

                // Çıkış işlemi yapılmış gibi, kullanıcıyı bilgilendiriyoruz
                await Clients.Caller.SendAsync("LogoutSuccess", "Çıkış işlemi başarılı");


                var contentuser = new StringContent(JsonConvert.SerializeObject(userId.token), Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userId.token); // token kullanılıyor
                var responseMessageQuitUser = await _httpClient.PutAsync($"https://localhost:7056/api/Users/LoginOut", contentuser);

                if (responseMessageQuitUser.IsSuccessStatusCode)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userId.token); // token kullanılıyor
                    var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/api/Users/userGetlistWithFriendIdList/{userId.userId}");
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var content = await responseMessage.Content.ReadAsStringAsync();
                        var friendIds = JsonConvert.DeserializeObject<List<int>>(content);

                        // Arkadaşlarını bilgilendiriyoruz
                        foreach (var friendId in friendIds)
                        {
                            var friendConnectionId = Users.FirstOrDefault(u => u.Value.userId == friendId.ToString()).Key; // Arkadaşın bağlantı ID'sini alıyoruz

                            if (!string.IsNullOrEmpty(friendConnectionId))
                            {
                                await Clients.Client(friendConnectionId).SendAsync("MessageFromLogOut", userId.userId);
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError("Arkadaş listesi alınırken hata oluştu.");
                    }
                }
                else
                {
                    _logger.LogError("Sunucuya ulaşılamadı.");
                }
            }
            else
            {
                // Eğer kullanıcı zaten çıkış yaptıysa veya bağlantısı kopmuşsa, hata mesajı gönderiyoruz
                await Clients.Caller.SendAsync("ErrorMessage", "Henüz giriş yapmadınız veya bağlantınız kesildi.");
            }

            // Temizlik işlemi için base metodu çağırıyoruz
            await base.OnDisconnectedAsync(exception);
        }
        #endregion

        #region Mesaj gönderme işlemi
        public async Task SendMessage(int recipientUserId, string message, int senderUserId, string type, int messageId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == recipientUserId.ToString()).Key;
            // Eğer alıcıya ait bir bağlantı ID'si varsa, mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya mesaj gönder
                await Clients.Client(recipientConnectionId).SendAsync("ReceiveMessage", message, senderUserId, type, messageId);
                _logger.LogInformation($"Mesaj gönderildi: {recipientUserId} -> {message}, Sender: {senderUserId}, Type: {type}, MessageId: {messageId}");
            }

        }
        #endregion

        #region Mesaj düzenleme işlemi
        public async Task SendEditMessage(int recipientUserId, string message, int senderUserId, int messageId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == recipientUserId.ToString()).Key;
            // Eğer alıcıya ait bir bağlantı ID'si varsa, mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya mesaj gönder
                await Clients.Client(recipientConnectionId).SendAsync("ReceiveEditMessage", message, senderUserId, messageId);
                _logger.LogInformation($"Mesaj düzenlenip gönderildi: {recipientUserId} -> {message}, Sender: {senderUserId}, MessageId: {messageId}");
            }

        }
        #endregion

        #region Mesaj okundu bildirimi
        public async Task MessageReading(int messageId, int recipientUserId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == recipientUserId.ToString()).Key;
            // Eğer alıcıya ait bir bağlantı ID'si varsa, mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya mesaj gönder
                await Clients.Client(recipientConnectionId).SendAsync("ReadingMessage", messageId);
                _logger.LogInformation($"Mesaj okudu gönderildi: {recipientUserId} , MessageId: {messageId}");
            }

        }
        #endregion

        #region Ekran Paylaşımı okundu bildirimi
        public async Task SendScreenCapture(byte[] imageBytes, int recipientUserId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == recipientUserId.ToString()).Key;
            // Eğer alıcıya ait bir bağlantı ID'si varsa, mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya mesaj gönder
                await Clients.Client(recipientConnectionId).SendAsync("ReceiveScreenCapture", imageBytes);
                _logger.LogInformation($"Ekran paylasımı yapıyor: {recipientUserId} ");
            }

        }
        #endregion

        #region Aranacak Kullanıcıyı Arama İsteği at
        public async Task CallUser(int UserId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == UserId.ToString()).Key;
            // Eğer alıcıya ait bir bağlantı ID'si varsa, mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya mesaj gönder
                await Clients.Client(recipientConnectionId).SendAsync("callRequest", UserId);
                _logger.LogInformation($"Konuşma isteği gönderdi : {UserId} ");
            }
        }
        #endregion

        #region Arama İsteğini Kabul Etme (Sesli Görüşmeye Yönlendirme)
        public async Task AcceptCall(int UserId, int CallerId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == UserId.ToString()).Key;
            var callerConnectionId = Users.FirstOrDefault(u => u.Value.userId == CallerId.ToString()).Key;

            // Eğer alıcıya ait bir bağlantı ID'si varsa, kabul mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Arama kabul edildiği için karşı tarafa sesli görüşme başlatılması için yönlendirme mesajı gönderiliyor
                await Clients.Client(recipientConnectionId).SendAsync("callAccepted");
                await Clients.Client(recipientConnectionId).SendAsync("startVoiceCall");
                // Arayan kişiye de sesli görüşme başlatma mesajı gönder
                if (!string.IsNullOrEmpty(callerConnectionId))
                {
                    await Clients.Client(callerConnectionId).SendAsync("callAccepted", UserId);
                    await Clients.Client(callerConnectionId).SendAsync("startVoiceCall");
                }

                _logger.LogInformation($"Konuşma isteği kabul edildi ve sesli görüşme başlatıldı: {UserId} ve {CallerId}");
            }
        }
        #endregion

        #region Arama İsteğini Reddetme
        public async Task RejectCall(int UserId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == UserId.ToString()).Key;

            // Eğer alıcıya ait bir bağlantı ID'si varsa, reddetme mesajı gönderiyoruz
            if (!string.IsNullOrEmpty(recipientConnectionId))
            {
                // Alıcıya arama reddedildi mesajı gönder
                await Clients.Client(recipientConnectionId).SendAsync("callRejected", UserId);
                _logger.LogInformation($"Konuşma isteği reddedildi: {UserId}");
            }
        }
        #endregion

        #region Sesli iletişim
        public async Task SendAudioData(byte[] audioData, int reiceverId)
        {
            // Alıcı kullanıcının bağlantı ID'sini almak için kullanıcı ID'sini kontrol ediyoruz
            var recipientConnectionId = Users.FirstOrDefault(u => u.Value.userId == reiceverId.ToString()).Key;
            await Clients.Client(recipientConnectionId).SendAsync("ReceiveAudioData", audioData);
        }
        #endregion
    }


}



