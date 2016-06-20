using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DrawPub.Application;
using DrawPub.Domain;
using DrawPub.Exceptions;
using Microsoft.Owin.Security.OAuth;


namespace DrawPub.ImageService
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

       

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            bool isValidUser = false;

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var UserApplciationService = Startup.container.GetInstance<IUserAppplicationService>();

            //TODO
            User user=new User(0);

            try
            {
                user = await UserApplciationService.AuthenticateUserAsync(context.UserName, context.Password);
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("Name", user.Name));
                foreach (var role in user.Roles)
                {
                    identity.AddClaim(new Claim("role", role));
                }

                context.Validated(identity);
            }
            catch (AuthenticationException ex)
            {
                context.SetError("invalid_grant", ex.Message);
            }
           
        }
    }


}