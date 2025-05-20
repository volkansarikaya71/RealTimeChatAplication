using ChatApi.EntityLayer;
using DataAccessLayer.Abstract;

namespace ChatApi.DataAccessLayer.Abstract
{
    public interface IUserDal: IGenericDal<User>
    {

        List<int> GetUserFriendsIdListWithUserId(int id);
    }
}
