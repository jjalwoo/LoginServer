using MySql.Data.MySqlClient;
using LoginServer.ErrorCodeEnum;

namespace LoginServer.DB
{
    public interface IDBRepository
    {
        Task<ErrorCode> CheckLogin(string userID, string password);
        Task CreateAccount(string userID, string passWord);
        Task<ErrorCode> CheckDuplicationID(string userID);
        string SHA256Hash(string password);
    }
}
