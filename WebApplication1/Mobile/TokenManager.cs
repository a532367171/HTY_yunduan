using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApplication1.Mobile
{
    public class TokenManager
    {
        private static string Secret = "szQkFW37ihYnEfgw1huvUMtp53zAA8w4jpQ/m6uaVDpkZdGu+FL8XbWZ+Oxs0w0g/KdjFVAYk9D9MJoXT7MfzQ==";

        

        public static string GenerateToken(string username)
        {
            var claims = new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, "useradmin"),
                    new Claim(ClaimTypes.SerialNumber, "123123123"),
                    new Claim(ClaimTypes.Role, "useradmin1"),
                    new Claim(ClaimTypes.Role, "useradmin2"),
                    new Claim(ClaimTypes.Role, "useradmin3"),
                    new Claim(ClaimTypes.Name, username + "1"),
                    new Claim(ClaimTypes.Name, username + "2"),
                    new Claim(ClaimTypes.Name, username + "3")

               };
            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new ClaimsIdentity(new[] {
            //    new Claim(ClaimTypes.Name,username),
            //}),
            //        Expires = DateTime.Now.AddMinutes(30),

            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            //    };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(30),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        private static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                if (jwtToken == null)
                {
                    return null;
                }
                var symmetricKey = Convert.FromBase64String(Secret);
                var validationParams = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey),
                };
                SecurityToken securityToken;
                var pincipal = tokenHandler.ValidateToken(token, validationParams, out securityToken);
                return pincipal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool ValidateToken(string token)
        {
            var principal = GetPrincipal(token);
            var identity = principal?.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return false;
            }
            if (!identity.IsAuthenticated)
            {
                return false;
            }
            var nameClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
            var username = nameClaim?.Value;
            if (string.IsNullOrEmpty(username))
            {
                return false;
            }

            return true;
        }
    }
}