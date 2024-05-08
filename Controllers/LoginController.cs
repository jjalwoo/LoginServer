using Microsoft.AspNetCore.Mvc;
using LoginServer.Packet;
using LoginServer.DB;
using LoginServer.ErrorCodeEnum;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LoginServer.JWTToken;
using LoginServer.Redis;


namespace LoginServer.Controllers
{
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IDBRepository _mySqlRepository;
        private readonly IConfiguration _config;
        private readonly Token _tokenGenerator;
        private readonly Redis.Redis _redis;


        // DI
        public LoginController(IDBRepository mySQLRepository, IConfiguration config, Redis.Redis redis)
        {
            _mySqlRepository = mySQLRepository;
            _config = config;
            _tokenGenerator = new Token(config);
            _redis = redis;
        }

        public string GetClientIP()
        {
            IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addr = ipEntry.AddressList;
            string ClientIp = addr[addr.Length - 1].ToString();

            return ClientIp;
        }

        [HttpGet]
        public void GetAsync(LoginRequest loginRequest)
        {

        }

        [HttpPost]
        [AllowAnonymous] // 인증 무시
        public async Task<IActionResult> PostAsync([FromBody] LoginRequest loginRequest)
        {
            var clientIp = GetClientIP();
            Console.WriteLine($"IP주소: {clientIp} 에서 로그인 시도중입니다!");

            LoginResponse loginResponse = new LoginResponse();

            if (loginRequest.Password == null || loginRequest.UserID == null)
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return BadRequest(loginResponse);
            }

            // check 
            if (loginRequest.UserID!.IndexOf(" ") != (int)ErrorCode.WhiteSpace)
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return BadRequest(loginResponse);
            }

            var result = await _mySqlRepository.CheckLogin(loginRequest.UserID, loginRequest.Password);

            if (result != ErrorCode.Succeess)
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return BadRequest(loginResponse);
            }

            try
            {
                string tokenString = _tokenGenerator.MakeToken(loginRequest);
                await _mySqlRepository.SaveToken(loginRequest.UserID, tokenString);

                string refreshToken = _tokenGenerator.MakeRefreshToken();
                _redis.SetKeyValue(loginRequest.UserID, refreshToken);
                return Ok(new { Token = tokenString, RefreshToken = refreshToken });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating token: {ex.Message}");
                loginResponse.ErrorCode = ErrorCode.Fail;
                return BadRequest(loginResponse);
            }
        }
    }
}