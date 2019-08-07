using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.ServiceInterfaces
{
    public interface ILoginService
    {
        Task<Token> LoginFacebookAsync(string accessToken);
        Token Login(Login login);
    }
}
