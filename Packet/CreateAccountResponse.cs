using LoginServer.ErrorCodeEnum;

namespace LoginServer.Packet
{
    public class CreateAccountResponse
    {
        public ErrorCode ErrorCode { get; set; } = default;       
    }
}
