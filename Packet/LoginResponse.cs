
using LoginServer.ErrorCodeEnum;

namespace LoginServer.Packet
{
    public class LoginResponse
    {
        public ErrorCode ErrorCode { get; set; } = default;
        public string? Token { get; set; } = default;// 토큰 속성 추가
    }
}
