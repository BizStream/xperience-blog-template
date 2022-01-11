using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Home;

public static class HomeServiceCollectionExtensions
{
    public static IServiceCollection AddHomeServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddAutoMapper( typeof( HomeServiceCollectionExtensions ).Assembly );
        return services;
    }
}
