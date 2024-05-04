using LoginServer.DB;
using LoginServer.ErrorCodeEnum;
using LoginServer.Packet;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace LoginServer.Controllers
{
    [Route("[controller]")]
    public class CreateAccountController : Controller
    {
        IDBRepository _mySqlRepository;

        public CreateAccountController(IDBRepository mySqlRepository)
        {          
            _mySqlRepository = mySqlRepository;          
        }

        [HttpPost]
        public async Task<ErrorCode> PostCreateAccount([FromBody]CreateAccountRequest createAccountPacket)
        {

            // var createAccountResponse = new CreateAccountResponse();
            CreateAccountResponse createAccountResponse = new() { ErrorCode = ErrorCode.Fail };  // new         
            //createAccountResponse.ErrorCode = ErrorCode.Fail;

            if (createAccountPacket.Password == "" ||  createAccountPacket.UserID == "")
            {
                return createAccountResponse.ErrorCode;
            }

            if(createAccountPacket.UserID!.IndexOf(" ") != (int)ErrorCode.WhiteSpace)
            {
                return createAccountResponse.ErrorCode;
            }

            var check = await _mySqlRepository.CheckDuplicationID(createAccountPacket.UserID);

            if (check == ErrorCode.Duplication) // 에러코드
            {
                return createAccountResponse.ErrorCode;
            }
            
          
           var result = await _mySqlRepository.CreateAccount(createAccountPacket.UserID, createAccountPacket.Password!);

            if(result == ErrorCode.Fail)
            {
                return createAccountResponse.ErrorCode;
            }           

            return result;
        }
    }
}
