using ChatApi.EntityLayer;

namespace ChatApi.BusinessLayer.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        List<int> TGetUserFriendsIdListWithUserId(int id);
    }
}
