using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace WebApplication1.Mobile
{

    public class ClassLogin
    {

        //HMACSHA256 hmac = new HMACSHA256();
        //string key = Convert.ToBase64String(hmac.Key);

        private static string Secret = "szQkFW37ihYnEfgw1huvUMtp53zAA8w4jpQ/m6uaVDpkZdGu+FL8XbWZ+Oxs0w0g/KdjFVAYk9D9MJoXT7MfzQ==";
        public static string GenerateToken(string username)
        {

            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name,username),
        }),
                Expires = DateTime.Now.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }
    }
}