namespace LoginServer.Packet
{
    public class LoginRequest
    {
        public string? UserID { get; set; } = default;
        public string? Password { get; set; } = default;
    }
}
