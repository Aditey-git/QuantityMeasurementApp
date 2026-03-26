using System;

namespace QuantityAppService
{
    public interface IAuthenticationServices
    {
        void RegisterUser(string email, string password);
        string LoginUser(string email, string password);
    }
}