using System;
using System.Reflection;
using QuantityAppRepository;
using QuantityAppModel;


namespace QuantityAppService{
    public class AuthenticationServices : IAuthenticationServices
    {
        private IUserRepository userRepository;
        private IPasswordHasherService passwordHasher;
        private readonly JwtTokenServices jwtTokenService;

        public AuthenticationServices(IUserRepository repo, IPasswordHasherService hasher, JwtTokenServices jwtService)
        {
            userRepository = repo;
            passwordHasher = hasher;
            jwtTokenService = jwtService;
        }

        public void RegisterUser(string email, string password)
        {
            if (userRepository.UserExists(email))
            {
                throw new Exception("User Already Exists");
            }

            string salt = passwordHasher.GenerateSalt();

            string hashPassword = passwordHasher.HashPassword(password, salt);

            UserEntity entity = new UserEntity
            {
                Email = email,
                PasswordHash = hashPassword,
                Salt = salt,
                CreatedAt = DateTime.UtcNow
            };

            userRepository.CreateUser(entity);
        }


        public string LoginUser(string email, string password)
        {

            UserEntity StoredUser = userRepository.GetUserByEmail(email);

            if(StoredUser == null)
            {
                return null;
            } 

            bool isValidPassword = passwordHasher.VerifyPassword(password, StoredUser.PasswordHash, StoredUser.Salt);

            if(!isValidPassword) return null;

            string token = jwtTokenService.GenerateToken(email);
            return token;
        }
    }
}