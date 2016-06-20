using DrawPub.Application;
using DrawPub.Domain.RepositoryInterface;
using DrawPub.ImageService.Log;
using DrawPub.Repository;
using LightInject;
using LightInject.Interception;


namespace DrawPub.ImageService
{  
    public class CompositionRoot : ICompositionRoot
    {
        private static IServiceRegistry serviceRegistry;
        public void Compose(IServiceRegistry serviceRegistry)
        {

            serviceRegistry.RegisterAssembly("DrawPub*.dll");
            serviceRegistry.Register<IInterceptor, LogInterceptor>("LogInterceptor");

            serviceRegistry.Intercept(sr => sr.ServiceType == typeof(IUserAppplicationService), DefineProxyType);
            serviceRegistry.Intercept(sr => sr.ServiceType == typeof(IUserRepository), DefineProxyType);
            serviceRegistry.Intercept(sr => sr.ServiceType == typeof(IConnectionProvider), DefineProxyType);
            

        }

        private void DefineProxyType(IServiceFactory servicefactory, ProxyDefinition proxyDefinition)
        {
            //container.Register<ILogFactory, LogFactory>();
            //container.Register<ILogBase,log.Logger>();



            proxyDefinition.Implement(() => servicefactory.GetInstance<IInterceptor>("LogInterceptor"));

        }
    }
}