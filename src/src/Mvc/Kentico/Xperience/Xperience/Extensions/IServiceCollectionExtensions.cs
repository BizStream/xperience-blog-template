using System;
using System.Linq;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Documents;
using BizStream.Extensions.Kentico.Xperience.StaticWebAssetsStorage;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Scheduler.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogTemplate.Mvc.Kentico.Xperience.Extensions
{

    public static class IServiceCollectionExtensions
    {

        public static IServiceCollection AddBlogKentico( this IServiceCollection services, IHostEnvironment environment )
        {
            if( services == null )
            {
                throw new ArgumentNullException( nameof( services ) );
            }

            var kentico = services.AddKentico(
                features =>
                {
                    features.UsePageBuilder();
                    features.UsePageRouting(
                        new PageRoutingOptions
                        {
                            EnableAlternativeUrls = true
                        }
                    );

                    features.UseScheduler();
                }
            );

            kentico.SetAdminCookiesSameSiteNone();
            if( environment.IsDevelopment() )
            {
                kentico.DisableVirtualContextSecurityForLocalhost();
            }

            // configure how documents are queried
            services.AddOptions<DocumentRetrieverOptions>()
                .ConfigureCurrentSite()
                .ConfigurePreviewMode();

            // add RCL bundles (requires `StaticWebAssetsStorageModule` to be registered)
            services.AddOptions<PageBuilderBundlesOptions>()
                .ConfigureRCLBundle( typeof( IServiceCollectionExtensions ).Assembly, "dist\\PageBuilder" );

            DecorateMemoryCacheWithPreviewSupport( services );
            return services;
        }

        private static void DecorateMemoryCacheWithPreviewSupport( IServiceCollection services )
        {
            var cache = services.FirstOrDefault(
                descriptor => descriptor.ServiceType == typeof( IMemoryCache )
            );

            if( cache == null )
            {
                throw new ArgumentException( $"Cannot decorate '{nameof( IMemoryCache )}': there is not an implementation registered to the Service Collection." );
            }

            var cacheType = cache.ImplementationType;
            var cacheImplementation = ServiceDescriptor.Describe( cacheType, cacheType, cache.Lifetime );

            // replace the original descriptor with the concrete IMemoryCache implementation
            services.Remove( cache );
            services.Add( cacheImplementation );

            services.AddScoped<IMemoryCache>(
                provider => new MemoryCacheWithPreviewSupport(
                    ( IMemoryCache )provider.GetRequiredService( cacheType ),
                    provider.GetRequiredService<IHttpContextAccessor>()
                )
            );
        }

    }

}
