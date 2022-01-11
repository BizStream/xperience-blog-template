using BlogTemplate.Mvc.App.Infrastructure.ResponseCaching;
using BlogTemplate.Mvc.Articles;
using BlogTemplate.Mvc.Authors;
using BlogTemplate.Mvc.Errors;
using BlogTemplate.Mvc.Home;
using BlogTemplate.Mvc.PageBuilder;
using BlogTemplate.Mvc.Seo;

namespace BlogTemplate.Mvc.App.Infrastructure;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddBlogTemplateApp( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddArticlesServices();
        services.AddAuthorsServices();
        services.AddErrorsServices();
        services.AddHomeServices();
        services.AddPageBuilderServices();
        services.AddSeoServices();

        services.ConfigureOptions<ConfigureOutputMinification>();
        services.ConfigureOptions<ConfigureResponseCaching>();

        return services;
    }
}
