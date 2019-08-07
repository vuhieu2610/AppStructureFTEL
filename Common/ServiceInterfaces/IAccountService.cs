using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityData;
using EntityData.Models;

namespace Common.ServiceInterfaces
{
    public interface IAccountService
    {
        Task<Token> LoginFacebookAsync(string accessToken);
        Token Login(Login login);
        User Authenticate(Login login);
    }
}
