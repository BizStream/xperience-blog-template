using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using BlogTemplate.Mvc.Infrastructure.Xperience.PageRetrievers;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Scheduler.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBlogTemplateKenticoMvc( this IServiceCollection services, IHostEnvironment environment )
    {
        if( services is null )
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

        AddPageRetrievers( services );
        return services;
    }

    public static IServiceCollection AddPageRetrievers( this IServiceCollection services )
    {
        if( services is null )
        {
            throw new ArgumentNullException( nameof( services ) );
        }

        services.AddTransient<IArticlePageRetriever, ArticlePageRetriever>();
        services.AddTransient<IAuthorPageRetriever, AuthorPageRetriever>();
        services.AddTransient<IHomePageRetriever, HomePageRetriever>();
        services.AddTransient<ISitemapPageRetriever, SitemapPageRetriever>();

        return services;
    }
}
