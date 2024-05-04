using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace LoginServer.ServiceRepository
{
    public class Security
    {
        private SHA256 _sha;

        public Security()
        {
            _sha = new SHA256Managed();
        }

        public string SHA245Hash(string password)
        {
            byte[] hash = _sha.ComputeHash(Encoding.ASCII.GetBytes(password));

            StringBuilder stringBuilder = new StringBuilder();

            foreach (byte b in hash)
            {
                stringBuilder.AppendFormat("{0:x2}", b);
            }
            return stringBuilder.ToString();
        }
    }
}