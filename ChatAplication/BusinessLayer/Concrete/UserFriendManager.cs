using BusinessLayer.Abstract;
using BusinessLayer.Models;
using ChatApi.DAL;
using ChatApi.Models;
using FrmLogin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserFriendManager : IUserFriendService
    {
        private readonly HttpClient _httpClient;

        public UserFriendManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<AddUserFriendViewModel>> Getlist()
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:7056/UserFriendList/1");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<AddUserFriendViewModel>>(jsonString);
                return values;
            }
            else
            {
                // Hata durumunda uygun yanıtı döndür
                throw new Exception("API isteği başarısız oldu.");
            }
        }


        public async Task<Result<AddUserFriendViewModel>> TAdd(AddUserFriendViewModel userFriendViewModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userFriendViewModel.Token);
            var jsonContent = JsonConvert.SerializeObject(userFriendViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"https://localhost:7056/AddFriend", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();
                return new Result<AddUserFriendViewModel>(userFriendViewModel); // Başarı mesajını döndürüyoruz
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new Result<AddUserFriendViewModel>(errorMessages); // Hata mesajını döndürüyoruz
            }
        }

        public Task TDelete(AddUserFriendViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<AddUserFriendViewModel>> TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<AddUserFriendViewModel>> TUpdate(AddUserFriendViewModel addUserFriendViewModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", addUserFriendViewModel.Token);

            var delete = new FriendDeleteViewModel
            {
                UserId = addUserFriendViewModel.UserId,
                UserFriendId = addUserFriendViewModel.UserFriendId
            };

            var jsonContent = JsonConvert.SerializeObject(delete);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/DeleteUserList", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();
                return new Result<AddUserFriendViewModel>(addUserFriendViewModel); // Başarı mesajını döndürüyoruz
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new Result<AddUserFriendViewModel>(errorMessages); // Hata mesajını döndürüyoruz
            }
        }

        public async Task<List<ListUserFriendViewModel>> TGetUserFriendsListWithUserId(ListUserFriendViewModel listUserFriendViewModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", listUserFriendViewModel.Token);
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/UserFriendList/{listUserFriendViewModel.UserId}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<ListUserFriendViewModel>>(responseData);
                var sortedFriends = result.OrderByDescending(f => f.UserStatus).ThenBy(f => f.UserName).ToList();
                return sortedFriends;
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                throw new Exception($"API çağrısı başarısız oldu: {errorMessages}");
            }
        }
    }
}
