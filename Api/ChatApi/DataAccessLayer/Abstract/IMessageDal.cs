using ChatApi.EntityLayer;
using ChatApi.Models;
using DataAccessLayer.Abstract;

namespace ChatApi.DataAccessLayer.Abstract
{
    public interface IMessageDal : IGenericDal<Message>
    {
        List<ChatMessageCountAndAbout> GetUserFriendMessageCount(int id);
    }
}
