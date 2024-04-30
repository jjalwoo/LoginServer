using LoginServer.DB;
using LoginServer.ErrorCodeEnum;
using LoginServer.Packet;
using Microsoft.AspNetCore.Mvc;

namespace LoginServer.Controllers
{
    [Route("[controller]")]
    public class CreateAccountController : Controller
    {
        private IDBRepository _mySqlRepository;

        public CreateAccountController(IDBRepository mySqlRepository)
        {
            _mySqlRepository = mySqlRepository;
        }

        [HttpPost]
        public async Task<ErrorCode> PostCreateAccount([FromBody] CreateAccountRequest createAccountPacket)
        {
            // var createAccountResponse = new CreateAccountResponse();
            CreateAccountResponse createAccountResponse = new();

            if (createAccountPacket.Password == "" || createAccountPacket.UserID == "")
            {
                createAccountResponse.ErrorCode = ErrorCode.Fail;
                return createAccountResponse.ErrorCode;
            }

            if (createAccountPacket.UserID.IndexOf(" ") != (int)ErrorCode.WhiteSpace)
            {
                createAccountResponse.ErrorCode = ErrorCode.Fail;
                return createAccountResponse.ErrorCode;
            }

            var check = await _mySqlRepository.CheckDuplicationID(createAccountPacket.UserID);

            if (check == ErrorCode.NotDuplication) // 에러코드
            {
                await _mySqlRepository.CreateAccount(createAccountPacket.UserID, createAccountPacket.Password);
                createAccountResponse.ErrorCode = ErrorCode.Suceess;
                return createAccountResponse.ErrorCode;
            }
            else
            {
                createAccountResponse.ErrorCode = ErrorCode.Duplication;
                return createAccountResponse.ErrorCode;
            }
        }
    }
}