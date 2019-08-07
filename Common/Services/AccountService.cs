//using Common.Base;
using Common.ServiceInterfaces;
using EntityData;
using EntityData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Services
{
    public class AccountService : IAccountService
    {
        private readonly IFacebookService _facebookService;
        private readonly IJwtService _jwtService;

        public AccountService(IFacebookService facebookService, IJwtService jwtService)
        {
            _facebookService = facebookService;
            _jwtService = jwtService;
        }

        public async Task<Token> LoginFacebookAsync(string fbToken)
        {
            var facebookUser = await _facebookService.GetLoginInfoFromFacebookAsync(fbToken);

            Token token = null;
            if (facebookUser != null)
            {
                if (AuthenticateFb(facebookUser))
                { // authentication
                    token = _jwtService.CreateAccessToken(facebookUser);
                }
            }

            return token;
        }

        public Token Login(Login login)
        {
            var user = Authenticate(login);
            if (user == null)
            {
                return null;
            }
            return _jwtService.CreateAccessToken(user);
        }

        public User Authenticate(Login login)
        {
            User user = null;
            if (login.Username == "admin" && login.Password == "1234")
            {
                user = new User
                {
                    Email = "pdaogu@gmail.com",
                    Username = "pdaogu",
                    Role = "Admin"
                };
            }
            return user;
        }
        private bool AuthenticateFb(User user)
        {
            var valid = false;
            if (user.Username == "pdaogu" && user.Email == "1234")
            {
                valid = true;
            }
            return true;    // return valid
        }
    }
}

