using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Natmir.Authentication;


namespace Natmir.ImageService.Filters
{
    public class IdentityBasicAuthenticationAttribute : BasicAuthenticationAttribute
    {
        private readonly IAuthenticationService _authenticationService;

        public IdentityBasicAuthenticationAttribute()
        {
            _authenticationService = new AuthenticationService();
        }

        protected override async Task<IPrincipal> AuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (_authenticationService.Authenticate(userName, password))
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "Eddie Admin"),
                    new Claim(ClaimTypes.Role, "Admin"),
                    // new Claim(ClaimTypes.Role, "Delete"),
                };


                ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");
                
                return new ClaimsPrincipal(new[] {identity});
            }

            return null;
            
        }

       
    }
}