using BizStream.Extensions.Kentico.Xperience.Retrievers.Abstractions.Documents;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Documents;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers;
using BlogTemplate.Infrastructure.Kentico.Xperience.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBlogServices( this IServiceCollection services )
        {
            services.AddOptions();

            AddRetrieverServices( services );
            AddAuthorServices( services );
            AddArticleServices( services );
            AddHomeServices( services );

            return services;
        }

        public static IServiceCollection AddArticleServices( this IServiceCollection services )
        {
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<IMetaDataService<Article>, ArticleService>();

            return services;
        }

        public static IServiceCollection AddAuthorServices( this IServiceCollection services )
        {
            services.AddTransient<IAuthorService, AuthorService>();

            return services;
        }

        public static IServiceCollection AddHomeServices( this IServiceCollection services )
        {
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<IMetaDataService<Blog>, BlogService>();

            return services;
        }

        public static IServiceCollection AddRetrieverServices( this IServiceCollection services )
        {
            services.AddTransient<IDocumentRetriever, BlogTemplateDocumentRetriever>();
            services.Configure<DocumentRetrieverOptions>( options => { } );

            return services;
        }
    }
}
