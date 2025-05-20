using ChatApi.EntityLayer;
using ChatApi.Models;
using DataAccessLayer.Abstract;

namespace ChatApi.DataAccessLayer.Abstract
{
    public interface IUserFriendListDal : IGenericDal<UserFriendList>
    {
        List<FriendAboutViewModel> GetUserFriendsListWithUserId(int id);

    }
}
