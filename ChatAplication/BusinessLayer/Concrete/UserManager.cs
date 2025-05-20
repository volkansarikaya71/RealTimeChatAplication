using BusinessLayer.Models;
using BusinessLayer.Abstract;
using FrmLogin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Security.Authentication.ExtendedProtection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using deneme3.Models;
using System.IO;
using ChatApi.DAL;

namespace ChatApi.BusinessLayer.Concrete
{
    public class UserManager : IUsersService
    {
        private readonly HttpClient _httpClient;

        public UserManager()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<UsersViewModel>> Getlist()
        {
            var responseMessage = await _httpClient.GetAsync("https://localhost:7056/api/Users/UserList");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonString = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UsersViewModel>>(jsonString);
                return values;
            }
            else
            {
                // Hata durumunda uygun yanıtı döndür
                throw new Exception("API isteği başarısız oldu.");
            }
        }

        public async Task<Result<UsersViewModel>> TAdd(UsersViewModel usersViewModel)
        {
            string ImageUrl = await FileZilla.addData(usersViewModel.UserImage, usersViewModel.PhoneNumber);
            usersViewModel.UserImage = ImageUrl;

            var jsonContent = JsonConvert.SerializeObject(usersViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"https://localhost:7056/api/Users/AddUser", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();
                return new Result<UsersViewModel>(usersViewModel); // Başarı mesajını döndürüyoruz
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new Result<UsersViewModel>(errorMessages); // Hata mesajını döndürüyoruz
            }
        }

        public Task TDelete(UsersViewModel t)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Result<ChataboutViewModel>> TGetByIdFriend(int id,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/api/Users/UserGetById/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<ChataboutViewModel>(content);
                return new Result<ChataboutViewModel>(user);
            }
            else
            {
                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                return new Result<ChataboutViewModel>(errorMessage); // Hata durumunda hata mesajını döndürüyoruz
            }

        }

        public async Task<Result<UsersViewModel>> TGetByEmail(string Email, string token, string password)
        {
            if (token == " ")
            {
                var jsonContent = JsonConvert.SerializeObject(Email);
                // Modeli JSON formatına çeviriyoruz
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                // PUT isteğini gönderiyoruz
                var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/api/Users/EmailControl", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    UsersViewModel usersViewModel = new UsersViewModel
                    {
                        Token = token
                    };
                    return new Result<UsersViewModel>(usersViewModel);
                }
                else
                {
                    var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                    return new Result<UsersViewModel>(errorMessage); // Hata durumunda hata mesajını döndürüyoruz
                }
            }
            else
            {
                UsersViewModel usersViewModel = new UsersViewModel
                {
                    Token = token,
                    Password = password
                };

                // Modeli JSON formatına çeviriyoruz
                var content = new StringContent(JsonConvert.SerializeObject(usersViewModel), Encoding.UTF8, "application/json");

                // PUT isteğini gönderiyoruz
                var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/api/Users/TokenControlAndUpdatePassword", content);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return new Result<UsersViewModel>("Şifreniz Başarılı Bir Şekilde Güncellendi");
                }
                else
                {
                    var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                    return new Result<UsersViewModel>(errorMessage); // Hata durumunda hata mesajını döndürüyoruz
                }
            }

        }

        public async Task<Result<UsersViewModel>> TGetByPhoneAndPassword(string userPhoneNumber, string userPassword)
        {
            UsersViewModel usersViewModel = new UsersViewModel
            {
                PhoneNumber = userPhoneNumber,
                Password = userPassword,
                UserLastOnlineDate = DateTime.Now,
                UserStatus=true,
            };


            // İçeriği JSON'a dönüştürün
            var content = new StringContent(JsonConvert.SerializeObject(usersViewModel), Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/api/Users/LoginControl", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                string token = responseObject?.token;
                usersViewModel.Token = token;

                return new Result<UsersViewModel>(usersViewModel);
            }
            else
            {
                var errorMessage = await responseMessage.Content.ReadAsStringAsync();
                return new Result<UsersViewModel>(errorMessage); // Hata durumunda hata mesajını döndürüyoruz
            }
        }

        public  Result<UsersViewModel> TGetByToken(string Token)
        {

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(Token) as JwtSecurityToken;

            var userId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
            var userName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserName")?.Value;
            var password = jsonToken?.Claims.FirstOrDefault(c => c.Type == "Password")?.Value;
            var email = jsonToken?.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            var phoneNumber = jsonToken?.Claims.FirstOrDefault(c => c.Type == "PhoneNumber")?.Value;
            var userImage = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserImage")?.Value;
            var userLastOnlineDate = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserLastOnlineDate")?.Value;
            var userStatus = jsonToken?.Claims.FirstOrDefault(c => c.Type == "UserStatus")?.Value;

            var claims = jsonToken.Claims;

            var userViewModel = new UsersViewModel
            {
                UserId = int.Parse(userId),
                UserName = userName,
                Password = password,
                Email = email,
                PhoneNumber = phoneNumber,
                UserImage = userImage,
                UserStatus =bool.Parse(userStatus),
                UserLastOnlineDate= DateTime.Parse(userLastOnlineDate)

            };

            return new Result<UsersViewModel>(userViewModel);

        }

        public async Task<Result<UsersViewModel>> TUpdate(UsersViewModel usersViewModel)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", usersViewModel.Token);
            var jsonContent = JsonConvert.SerializeObject(usersViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/api/Users/UpdateUserList", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = await responseMessage.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseData);
                string token = jsonResponse?.token;
                usersViewModel.Token = token;
                return new Result<UsersViewModel>(usersViewModel); // Başarı mesajını döndürüyoruz
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new Result<UsersViewModel>(errorMessages); // Hata mesajını döndürüyoruz
            }

        }

        public async Task<bool> LogOut(string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var content = new StringContent(JsonConvert.SerializeObject(Token), Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/api/Users/LoginOut", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                return true;  
            }
            else
            {
                return false;
            }
        }

        public Task<Result<UsersViewModel>> TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ChatMessageCountAndAbout>> TGetByIdFriendMessageList(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/AllMessageShow{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<List<ChatMessageCountAndAbout>>(content);
                return new List<ChatMessageCountAndAbout>(user);
            }
            else
            {
                return new List<ChatMessageCountAndAbout>(); // Hata durumunda hata mesajını döndürüyoruz
            }

        }



    }
}
