using MySql.Data.MySqlClient;
using LoginServer.ErrorCodeEnum;

namespace LoginServer.DB
{
    public interface IDBRepository
    {
        Task<ErrorCode> CheckLogin(string userID, string passWord);
        Task<ErrorCode> CheckDuplicationID(string userID);
        Task CreateAccount(string username, string password);
    }
}
