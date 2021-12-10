using BizStream.Kentico.Xperience.AspNetCore.StaticWebAssetsStorage;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.PageBuilder;

public static class PageBuilderServiceCollectionExtensions
{
    public static IServiceCollection AddPageBuilderServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddStaticWebAssetsStorage()
            .ConfigureOptions<ConfigurePageBuilderBundles>();

        return services;
    }

}
