using BizStream.AspNetCore.Components.Metadata;
using BizStream.AspNetCore.Components.OpenGraph;
using BizStream.AspNetCore.Components.Schema;
using BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Resolvers;
using BlogTemplate.Mvc.Seo.Infrastructure;
using BlogTemplate.Mvc.Seo.Infrastructure.Schema;
using BlogTemplate.Mvc.Seo.Mappings;
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

        services.AddAutoMapper(
            typeof( OpenGraphMappingProfile ),
            typeof( MediaUrlResolver<,> )
        );

        services.AddMvc()
            .AddMetadataComponent( options => options.AddProvider( new MetadataComponentProvider() ) )
            .AddOpenGraphComponent( options => options.AddProvider( new OpenGraphComponentProvider() ) )
            .AddSchemaComponent(
                options =>
                {
                    options.AddProvider( new HomeSchemaComponentProvider() );
                    options.AddProvider( new ArticleSchemaComponentProvider() );
                }
            );

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

        return services;
    }
}
