using LoginServer.ErrorCodeEnum;

namespace LoginServer.Packet
{
    public class LoginResponse
    {
        public ErrorCode ErrorCode { get; set; } = default;
    }
}
