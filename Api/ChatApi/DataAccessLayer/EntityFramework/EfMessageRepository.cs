using ChatApi.DataAccessLayer.Abstract;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.Repositories;
using ChatApi.EntityLayer;
using ChatApi.Models;

namespace ChatApi.DataAccessLayer.EntityFramework
{
    public class EfMessageRepository : GenericRepository<Message>, IMessageDal
    {
        public List<ChatMessageCountAndAbout> GetUserFriendMessageCount(int id)
        {
            using (Context _context = new Context())
            {
                //var messages = _context.Set<Message>()
                //    .Where(m => m.ReceiverId == id && !m.MessageReading)
                //    .Join(_context.Set<User>(),
                //          message => message.SenderId,
                //          user => user.UserId,
                //          (message, sender) => new ChatMessageCountAndAbout
                //          {
                //              SenderId = message.SenderId,
                //              MessageContext = message.MessageContext,
                //              MessageTime = message.MessageTime,
                //              SenderName = sender.UserName,
                //              SenderImage = sender.UserImage,
                //              MessageType=message.MessageType,
                //              PhoneNumber=sender.PhoneNumber,
                //          })
                //    .OrderByDescending(m => m.MessageTime)
                //    .ToList();

                //return messages;
                    var messages = _context.Set<Message>()
                        .Where(m => m.ReceiverId == id && !m.MessageReading) // Okunmamış mesajlar
                        .Join(_context.Set<User>(),
                              message => message.SenderId,
                              user => user.UserId,
                              (message, sender) => new
                              {
                                  message.SenderId,
                                  message.MessageContext,
                                  message.MessageTime,
                                  message.MessageType,
                                  sender.UserName,
                                  sender.UserImage,
                                  sender.PhoneNumber,
                                  message.MessageId // En eski mesajı bulmak için MessageId'yi alıyoruz
                              })
                        .OrderBy(m => m.MessageId) // En eski mesajı almak için MessageId’ye göre sıralama
                        .GroupBy(m => m.SenderId) // Gönderen kişiye göre gruplama
                        .Select(group => new ChatMessageCountAndAbout
                        {
                            SenderId = group.Key,
                            MessageContext = group.First().MessageContext, // İlk mesaj içeriği (ID en düşük olan)
                            MessageTime = group.Max(m => m.MessageTime), // En yeni mesaj zamanı
                            MessageType = group.First().MessageType, // İlk mesajın türü
                            SenderName = group.First().UserName,
                            SenderImage = group.First().UserImage,
                            PhoneNumber = group.First().PhoneNumber,
                            MessageCount = group.Count() // Aynı kişiden gelen okunmamış mesaj sayısı
                        })
                        .OrderByDescending(m => m.MessageTime) // Son mesajı en üstte göstermek için
                        .ToList();

                    return messages;
                }
        }

    }
}
