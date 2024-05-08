using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginServer.Packet;
using Org.BouncyCastle.Tls;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Cryptography;

namespace LoginServer.JWTToken
{
    public class Token
    {
        private readonly IConfiguration _config;

        public Token(IConfiguration config)
        {
            _config = config;
        }

        public string MakeToken(LoginRequest loginRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetValue<string>("Jwt:SecretKey"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, loginRequest.UserID!) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token) ?? string.Empty;

            return tokenString;
        }

        public string MakeRefreshToken()
        {
            var secureRandomBytes = new byte[32];

            var randomNumberGenerator = RandomNumberGenerator.Create();

            randomNumberGenerator.GetBytes(secureRandomBytes);

            var refreshToken = Convert.ToBase64String(secureRandomBytes);

            return refreshToken;
        }

    }
}