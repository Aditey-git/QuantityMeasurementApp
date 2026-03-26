using System;
using System.Data;
using QuantityAppModel;

namespace QuantityAppRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly QuantityMeasurementDbContext context;
        public UserRepository(QuantityMeasurementDbContext context)
        {
            this.context = context;
        }

        public void CreateUser(UserEntity entity)
        {
            context.Users.Add(entity);
            context.SaveChanges();
        }

        public UserEntity GetUserByEmail(string email)
        {
            return context.Users.FirstOrDefault(u => u.Email == email);
        }


        public bool UserExists(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }
    }
}
