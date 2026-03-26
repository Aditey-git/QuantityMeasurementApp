namespace QuantityAppService
{
    public interface IPasswordHasherService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
        bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt);
    }
}