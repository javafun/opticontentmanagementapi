using System.Web.Mvc;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EpiContentMgmtDemo.Business.Rendering;
using EPiServer.Web.Mvc;
using EPiServer.Web.Mvc.Html;
using EPiServer.ContentManagementApi;

namespace EpiContentMgmtDemo.Business.Initialization
{
    [InitializableModule]
    public class DependencyResolverInitialization : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            //Implementations for custom interfaces can be registered here.

            context.ConfigurationComplete += (o, e) =>
            {
                //Register custom implementations that should be used in favour of the default implementations
                context.Services.AddTransient<IContentRenderer, ErrorHandlingContentRenderer>()
                    .AddTransient<ContentAreaRenderer, AlloyContentAreaRenderer>();

                // Example how to allow anonymous calls
                context.Services.Configure<ContentManagementApiOptions>(c =>
                {
                    // Our default OAuth package does not support scope for now so that it should be disabled if you use our OAuth
                    c.SetDisableScopeValidation(true);

                    // this can be any values
                    c.SetRequiredRole(string.Empty);

                    // just in case you use another authorization server that supports scope
                    // c.AddAllowedScope("your_scope_name");

                    // enable flatten model. It is disabled by default (false)
                    c.SetFlattenPropertyModel(true);
                });
            };
        }

        public void Initialize(InitializationEngine context)
        {
            DependencyResolver.SetResolver(new ServiceLocatorDependencyResolver(context.Locate.Advanced));
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}
