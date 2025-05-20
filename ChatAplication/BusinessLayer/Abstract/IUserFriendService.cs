using BusinessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserFriendService:IGenericService<AddUserFriendViewModel>
    {
        Task<List<ListUserFriendViewModel>> TGetUserFriendsListWithUserId(ListUserFriendViewModel listUserFriendViewModel);
    }
}
