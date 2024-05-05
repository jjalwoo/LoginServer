using System.Xml;

namespace LoginServer.Model
{
    public class Account
    {
        public int Uid { get; set; }
        public string? User_id {  get; set; }
        public string? User_pw { get; set; }
        public DateTime Create_time {  get; set; }
        public DateTime Delete_Time { get; set; }
        public DateTime Last_Login_Time { get; set; }      
    }
}
