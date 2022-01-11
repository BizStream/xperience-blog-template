using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages;
using BlogTemplate.Mvc.Errors.Mappings;
using BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Converters;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Errors;

public static class ErrorsServiceCollectionExtensions
{
    public static IServiceCollection AddErrorsServices( this IServiceCollection services )
    {
        ArgumentNullException.ThrowIfNull( services );

        services.AddAutoMapper( typeof( ErrorMappingProfile ), typeof( HtmlContentConverter ) );
        services.AddXperienceStatusCodePages();

        return services;
    }
}
