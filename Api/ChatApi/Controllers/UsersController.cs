using ChatApi.BusinessLayer.Concrete;
using ChatApi.DataAccessLayer.Abstract;
using ChatApi.DataAccessLayer.Concrete;
using ChatApi.DataAccessLayer.EntityFramework;
using ChatApi.EntityLayer;
using ChatApi.Models;
using deneme3.Models;
using jwt_deneme.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using System.Security.Claims;

namespace ChatApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : Controller
    {
        UserManager _userManager = new UserManager(new EfUsersRepository());

        //[HttpGet("[action]")]
        //public IActionResult UserList()
        //{
        //    var values = _userManager.TGetlist();
        //    return Ok(values);
        //}


        [HttpPost("[action]")]
        [AllowAnonymous]
        public IActionResult AddUser(User user)
        {
            using var c = new Context();
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                return BadRequest(new Result<User>(errorMessages));
            }
            else
            {
                bool isPhoneNumberExists = c.Users.Any(x => x.PhoneNumber == user.PhoneNumber);
                bool isEmailExists = c.Users.Any(x => x.Email == user.Email);
                if (isPhoneNumberExists || isEmailExists)
                {
                    var errorMessages = new List<string>();
                    if (isPhoneNumberExists)
                    {
                        errorMessages.Add("Bu telefon numarası zaten kayıtlı.");
                    }

                    if (isEmailExists)
                    {
                        errorMessages.Add("Bu e-posta adresi zaten kayıtlı.");
                    }
                    return BadRequest(new Result<User>(errorMessages));
                }
                else
                {
                    _userManager.TAdd(user);
                    return Ok(new Result<User>(user));

                }
            }

        }
        [AllowAnonymous]
        [HttpGet("[action]/{id}")]
        public IActionResult UserGetById(int id)
        {
            var GetuserId = _userManager.TGetById(id);
            var chataboutViewModel = new ChataboutViewModel
            {
                ChatId =id,
                ChatImage= GetuserId.UserImage,
                ChatName=GetuserId.UserName,
                ChatTime=GetuserId.UserLastOnlineDate,
                ChatStatus=GetuserId.UserStatus,
                PhoneNumber=GetuserId.PhoneNumber,
            };
            if (GetuserId == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }
            else
            {
                return Ok(chataboutViewModel);
            }
        }


        [AllowAnonymous]
        [HttpPut("[action]")]
        public IActionResult LoginControl([FromBody] UserSignInViewModel userSignInViewModel)
        {
            using var c = new Context();
            var Getuservalue = c.Users.FirstOrDefault(u => u.PhoneNumber == userSignInViewModel.PhoneNumber && u.Password == userSignInViewModel.Password);

            if (Getuservalue == null)
            {
                return NotFound("Böyle Bir Kullanıcı Bulunamadı");
            }
            else
            {
                string token;
                Getuservalue.UserStatus = true;
                Getuservalue.UserLastOnlineDate = DateTime.Now;

                do
                {
                    token = new BuildToken().CreatToken(Getuservalue, isEmailControl: false); // Yeni Token oluştur
                }
                while (c.Users.Any(u => u.Token == token)); // Eğer Token başka bir kullanıcıda varsa yeniden üret
                {
                    Getuservalue.Token = token; // Kullanıcının Token alanını güncelle
                    _userManager.TUpdate(Getuservalue);
                }
                return Ok(new
                {
                    token
                }); // token'ı  gönder
            }
        }


        [AllowAnonymous]
        [HttpPut("[action]")]
        public IActionResult EmailControl([FromBody] string Email)
        {
            using var c = new Context();
            var GetEmail = c.Users.FirstOrDefault(u => u.Email == Email);
            if (GetEmail == null)
            {
                return NotFound("Böyle Bir E-posta Adresi Bulunamadı");
            }
            else
            {
                string token;

                do
                {
                    token = new BuildToken().CreatToken(GetEmail, isEmailControl: true); // Yeni Token oluştur
                }
                while (c.Users.Any(u => u.Token == token)); // Eğer Token başka bir kullanıcıda varsa yeniden üret

                GetEmail.Token = token; // Kullanıcının Token alanını güncelle

                c.SaveChanges();

                #region Kullanicinin Eposta Adresine Token Atma
                MailMessage mesaj = new MailMessage();
                SmtpClient istemci = new SmtpClient();
                istemci.Credentials = new System.Net.NetworkCredential("vsdeneme71@gmail.com", "ejhh jtis uklo jbqi");
                istemci.Port = 587;
                istemci.Host = "smtp.gmail.com";
                istemci.EnableSsl = true;
                mesaj.To.Add(Email);
                mesaj.From = new MailAddress("vsdeneme71@gmail.com");
                mesaj.Subject = "SecurityCode";
                mesaj.Body = $"Keyiniz : {token}";
                istemci.Send(mesaj);
                #endregion

                // Sadece token'ı geri gönder
                return Ok(new
                {
                    token
                });
            }
        }

        [AllowAnonymous]
        [HttpPut("[action]")]
        public IActionResult TokenControlAndUpdatePassword([FromBody] TokenControlViewModel tokenControlViewModel)
        {
            using var c = new Context();

            var GetToken = c.Users.FirstOrDefault(u => u.Token == tokenControlViewModel.Token);
            if (GetToken == null)
            {
                return NotFound("Keyleriniz Eşleşmedi Lütfen Kontrol Ediniz.");
            }
            else
            {
                GetToken.Password = tokenControlViewModel.Password;
                c.SaveChanges();

                return Ok(new
                {
                    message = "Şifreniz Başarılı Bir şekilde Güncellenmiştir.",

                });
            }
        }


        [HttpGet("[action]/{userId}")]
        public IActionResult userGetlistWithFriendIdList(int userId)
        {

            var values = _userManager.TGetUserFriendsIdListWithUserId(userId);

            if (values.Count==0)
            {
                return BadRequest("Arkadaş listen yok");
            }
            else
            {
                return Ok(values);
            }
        }

       


        //[HttpDelete("[action]/{id}")]
        //public IActionResult DeleteUserList(int id)
        //{

        //    var deluserid = _userManager.TGetById(id);
        //    if (deluserid == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        _userManager.TDelete(deluserid);
        //        return Ok();
        //    }
        //}

        [HttpPut("[action]")]
        public IActionResult UpdateUserList(User user)
        {
            using var c = new Context();
            var UpdateUser = c.Find<User>(user.UserId);
            if (UpdateUser == null)
            {
                return NotFound("Böyle Bir Kullanıcı Yok");
            }
            else
            {
                UpdateUser.UserName = user.UserName;
                UpdateUser.Password = user.Password;
                UpdateUser.Email = user.Email;
                UpdateUser.PhoneNumber = user.PhoneNumber;
                UpdateUser.UserImage = user.UserImage;


                string token;

                do
                {
                    token = new BuildToken().CreatToken(UpdateUser, isEmailControl: false); // Yeni Token oluştur
                }
                while (c.Users.Any(u => u.Token == token)); // Eğer Token başka bir kullanıcıda varsa yeniden üret
                
                    UpdateUser.Token = token; // Kullanıcının Token alanını güncelle
                    c.Update(UpdateUser);
                    c.SaveChanges();

                
                return Ok(new
                {
                    token
                }); // token'ı  gönder
            }
        }


        [HttpPut("[action]")]
        public IActionResult LoginOut([FromBody]string token)
        {
            using var c = new Context();
            var Getuservalue = c.Users.FirstOrDefault(u => u.Token== token);

            if (Getuservalue == null)
            {
                return NotFound("Böyle Bir Kullanıcı Bulunamadı");
            }
            else
            {
                Getuservalue.UserStatus = false;
                Getuservalue.UserLastOnlineDate = DateTime.Now;
                c.Update(Getuservalue);
                c.SaveChanges();
                return Ok("Başarılı bir şekilde Çıkış Yapıldı.");
            }
        }




    }
}
