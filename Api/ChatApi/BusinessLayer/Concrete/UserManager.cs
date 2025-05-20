using ChatApi.BusinessLayer.Abstract;
using ChatApi.DataAccessLayer.Abstract;
using ChatApi.EntityLayer;

namespace ChatApi.BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _UsersDal;

        public UserManager(IUserDal usersDal)
        {
            _UsersDal = usersDal;
        }

        public void TAdd(User t)
        {
            _UsersDal.Insert(t);
        }

        public void TDelete(User t)
        {
            throw new NotImplementedException();
        }

        public User TGetById(int id)
        {
            return _UsersDal.GetByID(id);
        }

        public List<User> TGetlist()
        {
            return _UsersDal.GetListAll();

        }

        public User TGetByPhoneNumber(string phoneNumber)
        {
            return _UsersDal.GetListAll().FirstOrDefault(u => u.PhoneNumber == phoneNumber);
        }

        public void TUpdate(User t)
        {
            _UsersDal.Update(t);
        }

        public List<int> TGetUserFriendsIdListWithUserId(int id)
        {
            return _UsersDal.GetUserFriendsIdListWithUserId(id);
        }
    }
}
