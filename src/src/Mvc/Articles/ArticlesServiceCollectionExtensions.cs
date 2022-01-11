using BlogTemplate.Infrastructure.Xperience.AutoMapper.Resolvers;
using BlogTemplate.Mvc.Articles.Abstractions;
using BlogTemplate.Mvc.Articles.Infrastructure;
using BlogTemplate.Mvc.Articles.Mappings;
using BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Converters;
using BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Resolvers;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Articles;

public static class ArticlesServiceCollectionExtensions
{
    public static IServiceCollection AddArticlesServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddAutoMapper(
            typeof( ArticleMappingProfile ),
            typeof( HtmlContentConverter ),
            typeof( DocumentPublishedDateResolver ),
            typeof( PageUrlResolver<,> )
        );

        AddRecentArticlesComponent( services );
        return services;
    }

    public static IServiceCollection AddRecentArticlesComponent( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddTransient<IRecentArticlesComponentProvider, RecentArticlesComponentProvider>();
        return services;
    }
}
