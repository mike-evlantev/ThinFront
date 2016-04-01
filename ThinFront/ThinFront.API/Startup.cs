using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using SimpleInjector.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ThinFront.API.Infrastructure;
using ThinFront.Core.Domain;
using ThinFront.Core.Infrastructure;
using ThinFront.Core.Repository;
using ThinFront.Data.Infrastructure;
using ThinFront.Data.Repository;

[assembly: OwinStartup(typeof(ThinFront.API.Startup))]

namespace ThinFront.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureSimpleInjector(app);

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container)
            };
            WebApiConfig.Register(config);

            ConfigureOAuth(app, container);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app, Container container)
        {
            Func<IAuthorizationRepository> authRepositoryFactory = container.GetInstance<IAuthorizationRepository>;
            // Configure Authentication
            var authenticationOptions = new OAuthBearerAuthenticationOptions();
            // Configure Authorziation
            var authorizationOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                // Telling ASP.NET the route (address for ticket booth)
                TokenEndpointPath = new PathString("/api/token"),
                // Token is only good for 1 day
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                // 
                Provider = new ThinFrontAuthorizationServerProvider(authRepositoryFactory)
            };

            app.UseOAuthAuthorizationServer(authorizationOptions);
            app.UseOAuthBearerAuthentication(authenticationOptions);
        }

        public Container ConfigureSimpleInjector(IAppBuilder app)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Infrastructure
            container.Register<IAuthorizationRepository, AuthorizationRepository>();
            container.Register<IDatabaseFactory, DatabaseFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>();
            container.Register<IUserStore<ThinFrontUser, int>, UserStore>(Lifestyle.Scoped);

            // Simple Injector takes interfaces and maps them to classes
            // in this case SI takes interface repos and maps them to EF repos
            // Repositories
            container.Register<IAddressRepository, AddressRepository>();
            container.Register<IAddressTypeRepository, AddressTypeRepository>();
            //container.Register<ICustomerRepository, CustomerRepository>();
            container.Register<IInventoryRepository, InventoryRepository>();
            container.Register<IOrderRepository, OrderRepository>();
            container.Register<IOrderItemRepository, OrderItemRepository>();
            container.Register<IProductRepository, ProductRepository>();
            container.Register<IProductCategoryRepository, ProductCategoryRepository>();
            container.Register<IProductSubcategoryRepository, ProductSubcategoryRepository>();
            container.Register<IPromotionRepository, PromotionRepository>();
            container.Register<IPromotionalProductRepository, PromotionalProductRepository>();
            //container.Register<IResellerRepository, ResellerRepository>();
            container.Register<IResellerProductCategoryRepository, ResellerProductCategoryRepository>();
            container.Register<IRoleRepository, RoleRepository>();
            //container.Register<ISupplierRepository, SupplierRepository>();
            container.Register<IThinFrontUserRepository, ThinFrontUserRepository>();
            container.Register<IResellerProductRepository, ResellerProductRepository>();

            // Services
            // 3rd party API implementation should be decoupled (needs a service and separate class)

            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next();
                }
            });

            container.Verify();

            return container;
        }
    }
}