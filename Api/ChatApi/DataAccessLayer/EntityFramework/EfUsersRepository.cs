using ChatApi.DataAccessLayer.Abstract;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.Repositories;
using ChatApi.EntityLayer;



namespace ChatApi.DataAccessLayer.EntityFramework
{
    public class EfUsersRepository: GenericRepository<User>, IUserDal
    {
        public List<int> GetUserFriendsIdListWithUserId(int id)
        {
            using (var c = new Context())
            {
                // Arkadaşların UserFriendId'lerini alıyoruz
                var friendIds = c.userFriendLists
                                  .Where(x => x.UserId == id && x.DeleteStatus == false)
                                  .Select(x => x.UserFriendId)  // Arkadaş ID'lerini alıyoruz
                                  .Where(friendId => c.Users.Any(user => user.UserId == friendId && user.UserStatus)) // User tablosunda UserStatus'u true olanları seç
                                  .ToList();
                return friendIds;
            }

        }
    }
}
