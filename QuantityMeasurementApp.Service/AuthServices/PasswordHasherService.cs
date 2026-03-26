using System.Security.Cryptography;
using System.Text;

namespace QuantityAppService
{
    public class PasswordHasherService : IPasswordHasherService
    {
        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            RandomNumberGenerator.Fill(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string HashPassword(string password,string salt)
        {
            string passwordWithSalt = password + salt;

            byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordWithSalt);

            using SHA256 algo = SHA256.Create();

            byte[] hashBytes = algo.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }


        public bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            string enteredPasswordWithSalt = enteredPassword + storedSalt;

            byte[] enteredPasswordWithSaltBytes = Encoding.UTF8.GetBytes(enteredPasswordWithSalt);

            using SHA256 algo = SHA256.Create();

            byte[] hashBytes = algo.ComputeHash(enteredPasswordWithSaltBytes);

            string givenPasswordHash = Convert.ToBase64String(hashBytes);

            if(givenPasswordHash == storedHash)
            {
                return true;
            }

            return false;
        }
    }
}