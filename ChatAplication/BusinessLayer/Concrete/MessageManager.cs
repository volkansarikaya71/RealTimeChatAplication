using BusinessLayer.Abstract;
using BusinessLayer.Models;
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
    public class MessageManager:IMessageService
    {
        private readonly HttpClient _httpClient;

        public MessageManager()
        {
            _httpClient = new HttpClient();
        }

        public Task<List<MessageViewModel>> Getlist()
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageViewModel>> TAdd(MessageViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task TDelete(MessageViewModel t)
        {
            throw new NotImplementedException();
        }

        public Task<Result<MessageViewModel>> TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<MessageViewModel>> TGetByIdAndFriendIdAddMessage(int userId,int friendId,string token,string message,string messagetype)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var messageViewModel = new MessageViewModel
            {
                SenderId = userId,
                ReceiverId = friendId,
                GroupId=null,
                SenderMessageStatus=false,
                ReceiverMessageStatus=false,
                MessageReading=false,
                MessageTime=DateTime.Now,
                MessageContext= message,
                MessageType=messagetype,
            };

            var jsonContent = JsonConvert.SerializeObject(messageViewModel);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.PostAsync($"https://localhost:7056/AddMessage", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                // JSON'u Result<MessageViewModel> olarak deserialize ediyoruz
                var responseObject = JsonConvert.DeserializeObject<Result<MessageViewModel>>(jsonResponse);

                // responseObject.Data MessageViewModel'ı içeriyor, bu yüzden onu döndürüyoruz
                return new Result<MessageViewModel>(responseObject.Data);
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new Result<MessageViewModel>(errorMessages); // Hata mesajını döndürüyoruz
            }

        }
        public async Task<bool> TDeleteMessageId(int messageId,int UserId,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/DeleteMessage{messageId},{UserId}",null);

            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<bool> TEditMessageWithByMessageId(int messageId, string message, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/EditMessage{messageId},{message}", null);

            if (responseMessage.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task<List<MessageContex>> GetlistWithFriendId(int receiverId,int senderId,string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await _httpClient.GetAsync($"https://localhost:7056/UserFriendList{receiverId},{senderId}");
            if (responseMessage.IsSuccessStatusCode)
            {

                var content = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini MessageContex listesine dönüştür
                var messageContexList = JsonConvert.DeserializeObject<List<MessageContex>>(content);
                return new List<MessageContex>(messageContexList);
            }
            else
            {
                var errorMessages = await responseMessage.Content.ReadAsStringAsync();
                return new List<MessageContex>();
            }
        }

        public async Task<bool> TUpdateReading(int messageId,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var responseMessage = await _httpClient.PutAsync($"https://localhost:7056/ReadingMessage{messageId}",null);
            if (responseMessage.IsSuccessStatusCode)
            {

                return  true;
            }
            else
            {
                return false;
            }
        }


        public Task<Result<MessageViewModel>> TUpdate(MessageViewModel t)
        {
            throw new NotImplementedException();
        }
    }
}
