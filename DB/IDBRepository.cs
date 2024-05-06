using MySql.Data.MySqlClient;
using LoginServer.ErrorCodeEnum;

namespace LoginServer.DB
{
    public interface IDBRepository
    {
        Task<ErrorCode> CheckLogin(string userID, string password);
        Task<ErrorCode> CreateAccount(string userID, string passWord);
        Task<ErrorCode> CheckDuplicationID(string userID);
        Task SaveToken(string userID, string tokenStr);
        Task<ErrorCode> CheckLoging(string userID);
        Task DeleteToken(string userID);
        // string SHA256Hash(string password);
    }
}
