using ChatApi.BusinessLayer.Abstract;
using ChatApi.DataAccessLayer.Abstract;
using ChatApi.EntityLayer;
using ChatApi.Models;

namespace ChatApi.BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void TDelete(Message t)
        {
            _messageDal.Delete(t);
        }

        public Message TGetById(int id)
        {
            return _messageDal.GetByID(id);
        }

        public Message TGetBy(int id)
        {
            return _messageDal.GetByID(id);
        }

        public List<Message> TGetlist()
        {
            throw new NotImplementedException();
        }

        public void TUpdate(Message t)
        {
            _messageDal.Update(t);
        }

        public List<Message> TGetBySenderIdAndReceiverId(int receiverId,int senderId)
        {
           return _messageDal.GetListAll().Where(x => (x.ReceiverId == receiverId && x.SenderId == senderId && x.ReceiverMessageStatus == false )|| (x.ReceiverId==senderId && x.SenderId==receiverId && x.SenderMessageStatus == false)).ToList();
        }

        public void TAdd(Message t)
        {
            _messageDal.Insert(t);
        }

        public List<ChatMessageCountAndAbout> TGetUserFriendMessageCount(int id)
        {
            return _messageDal.GetUserFriendMessageCount(id);
        }
    }
}
