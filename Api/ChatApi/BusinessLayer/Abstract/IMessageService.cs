using ChatApi.EntityLayer;
using ChatApi.Models;

namespace ChatApi.BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        List<ChatMessageCountAndAbout> TGetUserFriendMessageCount(int id);
    }
}
