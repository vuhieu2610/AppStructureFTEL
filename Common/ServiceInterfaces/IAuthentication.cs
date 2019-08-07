using EntityData;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.ServiceInterfaces
{
    public interface IAuthenticateService
    {
        User Authenticate(Login login);
        bool AuthenticateFb(User user);
    }
}
