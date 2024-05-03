using System.Xml;

namespace LoginServer.Model
{
    public class Account
    {
        public int Uid { get; set; }
        public string? UserId { get; set; } = default;
        public string? UserPw { get; set; } = default;
        public DateTime CreateTime {  get; set; }
        public DateTime DeleteTime { get; set; }
        public DateTime LastLoginTime { get; set; }      
    }
}