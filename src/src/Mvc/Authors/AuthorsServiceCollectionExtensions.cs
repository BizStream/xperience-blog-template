using BlogTemplate.Mvc.Authors.Abstractions;
using BlogTemplate.Mvc.Authors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Authors;

public static class AuthorsServiceCollectionExtensions
{
    public static IServiceCollection AddAuthorsServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddAutoMapper( typeof( AuthorsServiceCollectionExtensions ).Assembly );
        services.AddAuthorComponent();
        return services;
    }

    public static IServiceCollection AddAuthorComponent( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddTransient<IAuthorComponentProvider, AuthorComponentProvider>();
        return services;
    }
}
