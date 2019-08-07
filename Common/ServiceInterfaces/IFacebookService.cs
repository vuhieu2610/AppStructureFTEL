using EntityData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.ServiceInterfaces
{
    public interface IFacebookService
    {
        Task<User> GetUserFromFacebookAsync(string facebookToken);
        Task<User> GetLoginInfoFromFacebookAsync(string facebookToken);
    }
}
