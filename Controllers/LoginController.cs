using Microsoft.AspNetCore.Mvc;
using LoginServer.Packet;
using LoginServer.DB;
using LoginServer.ErrorCodeEnum;

namespace LoginServer.Controllers
{
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IDBRepository _mySqlRepository;        

        // DI
        public LoginController(IDBRepository mySQLRepository)
        {
            _mySqlRepository = mySQLRepository;           
        }

        [HttpGet]
        public void GetAsync(LoginRequest loginRequest)
        {

        }

        [HttpPost]
        public async Task<ErrorCode> PostAsync([FromBody] LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

            if (loginRequest.Password == "" || loginRequest.UserID == "")
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return loginResponse.ErrorCode;
            }

            if (loginRequest.UserID.IndexOf(" ") != (int)ErrorCode.WhiteSpace)
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return loginResponse.ErrorCode;
            }

            var result = await _mySqlRepository.CheckLogin(loginRequest.UserID, loginRequest.Password);

            if(result != ErrorCode.Suceess)
            {
                loginResponse.ErrorCode = ErrorCode.Fail;
                return loginResponse.ErrorCode;
            }
            else
            {
                loginResponse.ErrorCode = ErrorCode.Suceess;
                return loginResponse.ErrorCode;
            }
        }
    }
}
