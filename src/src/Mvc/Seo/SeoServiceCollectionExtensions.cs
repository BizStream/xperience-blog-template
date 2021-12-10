using BizStream.AspNetCore.Components.Metadata;
using BizStream.AspNetCore.Components.OpenGraph;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SimpleMvcSitemap;

namespace BlogTemplate.Mvc.Seo;

public static class SeoServiceCollectionExtensions
{
    public static IServiceCollection AddSeoServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddMvc()
            .AddMetadataComponent()
            .AddOpenGraphComponent();

        services.TryAddScoped(
            serviceProvider =>
            {
                var actionContext = serviceProvider.GetRequiredService<IActionContextAccessor>()
                    .ActionContext;

                var factory = serviceProvider.GetRequiredService<IUrlHelperFactory>();
                return factory.GetUrlHelper( actionContext! );
            }
        );

        services.AddTransient<ISitemapProvider, SitemapProvider>();

        services.ConfigureOptions<ConfigureMetadataComponent>();
        services.ConfigureOptions<ConfigureOpenGraphComponent>();

        return services;
    }
}
