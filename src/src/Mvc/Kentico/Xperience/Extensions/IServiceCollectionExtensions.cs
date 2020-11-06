using System;
using BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers;
using CMS.Base;
using Kentico.Content.Web.Mvc;
using Kentico.Content.Web.Mvc.Routing;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Scheduler.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
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
                .PostConfigure<ISiteService>(
                    ( options, siteService ) => options.SiteID = siteService.CurrentSite.SiteID
                );

            return services;
        }

    }

}
