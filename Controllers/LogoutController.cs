using LoginServer.DB;
using LoginServer.ErrorCodeEnum;
using LoginServer.Packet;
using Microsoft.AspNetCore.Mvc;

namespace LoginServer.Controllers
{
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        IDBRepository _mySqlRepository;

        public LogoutController(IDBRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        [HttpPost]
        public async Task<ErrorCode> PostAsync([FromBody] LogoutRequest logoutRequest)
        {
            LogoutResponse logoutResponse = new();

            var result = await _mySqlRepository.CheckLoging(logoutRequest.UserID!);

            if (result == ErrorCode.Loging)
            {
                logoutResponse = new LogoutResponse() { ErrorCode = result };
                return logoutResponse.ErrorCode;
            }

            if (result == ErrorCode.NotLoging)
            {
                await _mySqlRepository.DeleteToken(logoutRequest.UserID!);
                logoutResponse = new LogoutResponse() { ErrorCode = ErrorCode.LogoutSuccess };
                return logoutResponse.ErrorCode;
            }

            return ErrorCode.Fail;
        }

    }
}