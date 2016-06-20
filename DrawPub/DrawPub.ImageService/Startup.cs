using System;
using System.Web.Http;
using DrawPub.ImageService.Providers;
using LightInject;
using LightInject.Interception;
using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace DrawPub.ImageService
{
    public class Startup
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static GoogleOAuth2AuthenticationOptions googleAuthOptions { get; private set; }

        public static ServiceContainer container = new ServiceContainer();
   
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            SwaggerConfig.Register(config);

            ConfigureOAuth(app);
          
            container.RegisterApiControllers();
            container.EnableWebApi(config);
            container.RegisterFrom<CompositionRoot>();

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            //use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(Microsoft.AspNet.Identity.DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/authtoken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
           app.UseOAuthAuthorizationServer(OAuthServerOptions);
           app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            googleAuthOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "523475649756-rr9l3jhan675fco7jig3di1kr86omgoi.apps.googleusercontent.com",
                ClientSecret = "8Hy5xe1NDOOZ6yHtspH9-n6R",
                Provider = new GoogleOAuthProvider()
                

            };

          app.UseGoogleAuthentication(googleAuthOptions);


        }

        private void DefineProxyType(IServiceFactory servicefactory, ProxyDefinition proxyDefinition)
        {
            //container.Register<ILogFactory, LogFactory>();
            //container.Register<ILogBase,log.Logger>();

           

            proxyDefinition.Implement(() => servicefactory.GetInstance<IInterceptor>("LogInterceptor"));

        }
    }
}