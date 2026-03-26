using System.Collections.Generic;
using QuantityAppModel;

namespace QuantityAppRepository
{
    public interface IUserRepository
    {
        void CreateUser(UserEntity entity);
        UserEntity GetUserByEmail(string email);
        bool UserExists(string email);
    }
}