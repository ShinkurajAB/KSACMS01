using SwitchCMS.Model;
using SwitchCMS.Model.Authentication;
using SwitchCMS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace SwitchCMS.APIServices.Authentication
{
    public interface IJWTService
    {
        string GenerateAccessToken(UserAccount user);
        string GenerateRefreshToken(string expiredToken, string password, string browserDetails);
        string GenerateRefreshToken(int userId, string email, string password, string browserDetails);
        bool IsRefreshTokenValid(int userId, string email, string password, string browserDetails, string refreshToken);
        UserAccount GetUserDetails(string token);
    }
    public class JWTService : IJWTService
    {
        private const string secret = "76166a08f52b4b9c8094b52de4b19cc3b4e86a66cd8e4a368673568437b6e289fcd6b2c8fdae4f57aaba265d00899c11";
        private const int DayHours = 24;
        private const int TokenAccessLifeHours = DayHours;
        private const int TokenRefreshLifeHours = DayHours * 30; // 30 Days
        private const string NoEmail = "NoEmail";

        private SymmetricSecurityKey GetSecurityKey()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            return new SymmetricSecurityKey(key);
        }

        private List<Claim> GetClaims(UserAccount user)
        {
            List<Claim> result = new List<Claim>
            {
                new Claim(UserClaimTypes.Id, user.User.ID.ToString()),
                new Claim(UserClaimTypes.Email, user.User.UserName ?? string.Empty),
                new Claim(UserClaimTypes.Name, user.User.UserName ?? string.Empty),
                new Claim(UserClaimTypes.Role, Enum.GetName(user.User.Role)!)
            };
            result.AddRange(user.Claims.Select(x => new Claim(x.UserType!, x.UserValue!)));
            return result;
        }

        private List<Claim> GetClaims(JwtSecurityToken token)
        {
            List<Claim> result = new List<Claim>();
            Claim? item;
            item = token.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Id);
            if (item != null) result.Add(item);
            item = token.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Email);
            if (item != null) result.Add(item);
            item = token.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Name);
            if (item != null) result.Add(item);
            item = token.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.Role);
            if (item != null) result.Add(item);
            item = token.Claims.FirstOrDefault(x => x.Type == UserClaimTypes.RefreshTokenHash);
            if (item != null) result.Add(item);
            return result;
        }

        private List<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var claims = GetClaims(jwtToken);
            return claims;
        }

        private List<Claim> GetClaimsFromExpiredToken(string expiredToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(expiredToken);
            var claims = GetClaims(jwtToken);
            return claims;
        }

        public string GenerateAccessToken(UserAccount user)
        {
            List<Claim> claims = GetClaims(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TokenAccessLifeHours),
                SigningCredentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string expiredToken, string password, string browserDetails)
        {
            var expClaims = GetClaimsFromExpiredToken(expiredToken);
            string email = expClaims.SingleOrDefault(x => x.Type == UserClaimTypes.Email)!.Value ?? string.Empty;
            if (string.IsNullOrWhiteSpace(browserDetails)) browserDetails = Guid.NewGuid().ToString();
            string refreshTokenHash = CreateHash(browserDetails + email + password);
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimTypes.Id, expClaims.Single(x => x.Type == UserClaimTypes.Id).Value),
                new Claim(UserClaimTypes.Email, email),
                new Claim(UserClaimTypes.RefreshTokenHash, refreshTokenHash)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TokenRefreshLifeHours),
                SigningCredentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(int userId, string email, string password, string browserDetails)
        {
            if (string.IsNullOrWhiteSpace(browserDetails)) browserDetails = Guid.NewGuid().ToString();
            string refreshTokenHash = CreateHash(browserDetails + email + password);
            List<Claim> claims = new List<Claim>
            {
                new Claim(UserClaimTypes.Id, userId.ToString()),
                new Claim(UserClaimTypes.Email, email ?? string.Empty),
                new Claim(UserClaimTypes.RefreshTokenHash, refreshTokenHash)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(TokenRefreshLifeHours),
                SigningCredentials = new SigningCredentials(GetSecurityKey(), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool IsRefreshTokenValid(int userId, string email, string password, string browserDetails, string refreshToken)
        {
            bool IsValid = true;
            string refreshTokenHash = CreateHash(browserDetails + email + password);
            var claims = GetClaims(refreshToken);
            if (claims.FirstOrDefault(x => x.Type == UserClaimTypes.Id)?.Value != userId.ToString()) IsValid = false;
            if (claims.FirstOrDefault(x => x.Type == UserClaimTypes.Email)?.Value != email) IsValid = false;
            if (claims.FirstOrDefault(x => x.Type == UserClaimTypes.RefreshTokenHash)?.Value != refreshTokenHash) IsValid = false;
            return IsValid;
        }

        public UserAccount GetUserDetails(string token)
        {
            UserAccount user = null;
            try
            {
                var claims = GetClaims(token);
                user = new UserAccount()
                {
                    User = new OUSR(),
                    Claims = null
                };
                user.User.ID = int.Parse(claims.First(x => x.Type == UserClaimTypes.Id).Value);
                user.User.UserName = claims.FirstOrDefault(x => x.Type == UserClaimTypes.Email)?.Value;
                if (string.IsNullOrWhiteSpace(user.User.UserName)) user.User.UserName = NoEmail;
                user.User.Role = Enum.Parse<Roles>(claims.FirstOrDefault(x => x.Type == UserClaimTypes.Role)?.Value ?? nameof(Roles.Guest));
                user.Claims = claims.Select(x => new UserClaimModel()
                {
                    ID = 0,
                    UserID = user.User.ID,
                    UserType = x.Type,
                    UserValue = x.Value
                }).ToList();
                user.Claims.RemoveAll(x => x.UserType == UserClaimTypes.Email || x.UserValue == UserClaimTypes.Role);
                user.Claims.Add(new UserClaimModel() { ID = 0, UserID = user.User.ID, UserType = ClaimTypes.Email, UserValue = user.User.UserName });
                user.Claims.Add(new UserClaimModel() { ID = 0, UserID = user.User.ID, UserType = ClaimTypes.Role, UserValue = Enum.GetName(user.User.Role) });
            }
            catch { }
            return user;
        }

        public static string CreateHash(string value)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
