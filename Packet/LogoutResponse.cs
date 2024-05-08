using LoginServer.ErrorCodeEnum;

namespace LoginServer.Packet
{
    public class LogoutResponse
    {
        public ErrorCode ErrorCode { get; set; } = default;
    }
}
