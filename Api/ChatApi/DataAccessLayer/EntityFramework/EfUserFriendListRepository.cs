using ChatApi.DataAccessLayer.Abstract;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.Repositories;
using ChatApi.EntityLayer;
using ChatApi.Models;
using Microsoft.EntityFrameworkCore;


namespace ChatApi.DataAccessLayer.EntityFramework
{
    public class EfUserFriendListRepository : GenericRepository<UserFriendList>, IUserFriendListDal
    {


        public List<FriendAboutViewModel> GetUserFriendsListWithUserId(int id)
        {
            using (var c = new Context())
            {
                // Arkadaşların UserFriendId'lerini alıyoruz
                var friendIds = c.userFriendLists
                                  .Where(x => x.UserId == id && x.DeleteStatus == false)
                                  .Select(x => x.UserFriendId)  // Arkadaş ID'lerini alıyoruz
                                  .ToList();

                // Arkadaş bilgilerini UserFriendId üzerinden alıyoruz
                var result = friendIds.Select(friendId =>
                {
                    // UserFriendId ile ilişkili User bilgilerini almak
                    var user = c.Users.FirstOrDefault(u => u.UserId == friendId); // User'ı id üzerinden alıyoruz

                    if (user != null)
                    {
                        return new FriendAboutViewModel
                        {
                            UserId = user.UserId,              // Arkadaşın UserId'si
                            UserName = user.UserName,      // Arkadaşın adı
                            UserImage = user.UserImage,    // Arkadaşın resmi
                            UserStatus = user.UserStatus,  // Arkadaşın durumu
                            UserLastOnlineDate = user.UserLastOnlineDate, // Arkadaşın son çevrimiçi olduğu tarih
                            UserPhoneNumber=user.PhoneNumber
                        };
                    }
                    return null; // Eğer user bulunamazsa, null döndürüyoruz
                }).Where(f => f != null).ToList(); // null değerleri filtreliyoruz

                return result;
            }
        }
    }
}
