using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityData;
using EntityData.Models;

namespace Common.ServiceInterfaces
{
    public interface IJwtService
    {
        Token CreateAccessToken(User user);
    }
}
