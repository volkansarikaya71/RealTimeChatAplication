using ChatApi.DataAccessLayer.Concrete;
using ChatApi.EntityLayer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace jwt_deneme.DAL
{
    public class BuildToken
    {
        public string CreatToken(User user, bool isEmailControl = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Güçlü bir secret key belirleyin (en az 16 karakter olmalı)
            var key = Encoding.UTF8.GetBytes("PhoneNumberAndPasswordControlJwt");

            string userLastOnlineDateClaim = user.UserLastOnlineDate.ToString("yyyy-MM-dd HH:mm:ss") ?? "No Date";
            string userStatusClaim = user.UserStatus ? (user.UserStatus ? "true" : "false"): "false";
            // Kullanıcı bilgilerini Claims olarak ekleyin

            var claims = new List<Claim>
                {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Password", user.Password),
                new Claim("Email", user.Email),
                new Claim("PhoneNumber", user.PhoneNumber.ToString()),
                new Claim("UserImage", user.UserImage),
                new Claim("UserLastOnlineDate", userLastOnlineDateClaim),
                new Claim("UserStatus", userStatusClaim),
                };

            if (isEmailControl)
            {
                claims.Clear(); // Eğer sadece token döndürülecekse, kullanıcı bilgilerini kaldır
         
            }

            // Token oluşturma için gerekli parametreler
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "http://localhost",                      // Issuer (Kimlik doğrulama sunucusu)
                Audience = "http://localhost",                    // Audience (Kullanıcı)
                NotBefore = DateTime.Now,                         // Token'ın geçerli olacağı zaman
                Expires = DateTime.Now.AddHours(6),              // Token'ın süresi
                Subject = new ClaimsIdentity(claims),            // Kullanıcı bilgileri (Claims)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // İmzalama işlemi
            };

            // Token oluşturma
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Token'ı string olarak döndür
            return tokenHandler.WriteToken(token);




        }
    }
}
