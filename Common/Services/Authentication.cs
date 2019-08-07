
using Common.ServiceInterfaces;
using EntityData;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppOutSideAPI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public User Authenticate(Login login)
        {
            User user = null;
            if (login.Username == "admin" && login.Password == "1234")
            {   // call stored procedure for checking
                user = new User
                {
                    Email = "pdaogu@gmail.com",
                    Username = "pdaogu",
                    Role = "Admin"
                };
            }
            return user;
        }
        public bool AuthenticateFb(User user)
        {
            var valid = false;
            if (user.Username == "pdaogu" && user.Email == "1234")
            {    // call stored procedure for checking
                valid = true;
            }
            return true;    // return valid
        }
    }
}
