namespace LoginServer.Packet
{
    public class CreateAccountRequest
    {
        public string UserID { get; set; } = default;
        public string Password { get; set; } = default;
    }
}
