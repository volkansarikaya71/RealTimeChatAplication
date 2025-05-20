using ChatApi.EntityLayer;
using ChatApi.Models;

namespace ChatApi.BusinessLayer.Abstract
{
    public interface IUserFriendListService : IGenericService<UserFriendList>
    {
        List<FriendAboutViewModel> TGetUserFriendsListWithUserId(int id);

    }
}
