using ChatApi.BusinessLayer.Abstract;
using ChatApi.DataAccessLayer.Abstract;
using ChatApi.EntityLayer;
using ChatApi.Models;
using System.Linq;

namespace ChatApi.BusinessLayer.Concrete
{
    public class UserFriendListManager:IUserFriendListService
    {
        IUserFriendListDal _userFriendListDal;

        public UserFriendListManager(IUserFriendListDal userFriendListDal)
        {
            _userFriendListDal = userFriendListDal;
        }

        public void TAdd(UserFriendList t)
        {
            _userFriendListDal.Insert(t);
        }

        public void TDelete(UserFriendList t)
        {
            throw new NotImplementedException();
        }

        public UserFriendList TGetByIdWithFriendId(int userId, int friendId)
        {
            return _userFriendListDal.GetListAll().Where(x => x.UserId == userId && x.UserFriendId == friendId).FirstOrDefault();
        }

        public UserFriendList TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserFriendList> TGetlist()
        {
            return _userFriendListDal.GetListAll();
        }

        public UserFriendList TGetByIdAndGetByFriendId(int userId,int friendId)
        {
            return _userFriendListDal.GetListAll().Where(x => x.UserId == userId && x.UserFriendId == friendId).FirstOrDefault();
        }


        public void TUpdate(UserFriendList t)
        {
            _userFriendListDal.Update(t);
        }

        public List<FriendAboutViewModel> TGetUserFriendsListWithUserId(int id)
        {
            return _userFriendListDal.GetUserFriendsListWithUserId(id);
        }


    }
}
