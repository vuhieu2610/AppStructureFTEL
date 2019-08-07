using Common.ServiceInterfaces;
using EntityData.Models;
using System.Threading.Tasks;

namespace AppOutSideAPI.Services
{
    public class LoginService
    {
        private readonly IFacebookService _facebookService;
        private readonly IJwtService _JwtService;
        private readonly IAuthenticateService _authService;

        public LoginService(IFacebookService facebookService, IJwtService jwtService, IAuthenticateService authService)
        {
            _facebookService = facebookService;
            _JwtService = jwtService;
            _authService = authService;
        }

        public async Task<Token> LoginFacebookAsync(string fbToken)
        {
            var facebookUser = await _facebookService.GetLoginInfoFromFacebookAsync(fbToken);

            Token token = null;
            if (facebookUser != null)
            {
                if (_authService.AuthenticateFb(facebookUser))
                { // authentication
                    token = _JwtService.CreateAccessToken(facebookUser);
                }
            }

            return token;
        }

        public Token Login(Login login)
        {
            var user = _authService.Authenticate(login);
            Token token = null;
            if (user != null)
            {
                token = _JwtService.CreateAccessToken(user);
            }
            return token;
        }
    }
}
