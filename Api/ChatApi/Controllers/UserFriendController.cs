using ChatApi.BusinessLayer.Concrete;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.EntityFramework;
using ChatApi.EntityLayer;
using ChatApi.Models;
using jwt_deneme.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ChatApi.Controllers
{
    [Authorize]
    public class UserFriendController : Controller
    {
        UserFriendListManager _userFriendListManager = new UserFriendListManager(new EfUserFriendListRepository());
        UserManager _userManager = new UserManager(new EfUsersRepository());

        [HttpGet("[action]/{id}")]
        public IActionResult UserFriendList(int id)
        {
            var values = _userFriendListManager.TGetUserFriendsListWithUserId(id);
            return Ok(values);
        }

        [HttpPost("[action]")]
        public IActionResult AddFriend([FromBody] UserFriendAddViewModel userFriendAddView)
        {
            var userFriendList = new UserFriendList();
            var userdata =  _userManager.TGetByPhoneNumber(userFriendAddView.FriendPhoneNumber);
            if (userdata != null)
            {
                var ExitFriend = _userFriendListManager.TGetByIdWithFriendId(userFriendAddView.UserId, userdata.UserId);
                if (ExitFriend != null)
                {
                    if (ExitFriend.DeleteStatus)
                    {

                        ExitFriend.DeleteStatus = false;
                        _userFriendListManager.TUpdate(ExitFriend);
                        return Ok("Arkadaş Eklendi");
                    }
                    else
                    {
                        return BadRequest("Zaten Arkadaşınız");
                    }  
                }
                else
                {

                    userFriendList.UserId = userFriendAddView.UserId;
                    userFriendList.UserFriendId = userdata.UserId;
                    userFriendList.DeleteStatus = false;
                    _userFriendListManager.TAdd(userFriendList);
                    return Ok("Başarılı bir Şekilde Arkadaş Eklendi");
                }
            }
            else
            {
                return BadRequest("Telefon numarasına ait kullanıcı bulunamadı");
            }
        }

        [HttpPut("[action]")]
        public  IActionResult DeleteUserList([FromBody] FriendDeleteViewModel friendDeleteViewModel)
        {
            var deleteId = _userFriendListManager.TGetByIdAndGetByFriendId(friendDeleteViewModel.UserId,friendDeleteViewModel.UserFriendId);
            
            if (deleteId == null) 
            {
                return NotFound("Arkadaş bulunamadı");
            }
            else
            {
                deleteId.DeleteStatus = true;
                _userFriendListManager.TUpdate(deleteId);
                return Ok("Başarılı bir şekilde silindi");
            }

        }
    }
}
