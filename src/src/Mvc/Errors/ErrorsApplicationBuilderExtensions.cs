using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages;
using Microsoft.AspNetCore.Builder;

namespace BlogTemplate.Mvc.Errors;

public static class ErrorsApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBlogTemplateErrors( this IApplicationBuilder app )
    {
        ArgumentNullException.ThrowIfNull( app );

        app.UseExceptionHandler( "/error" );
        app.UseXperienceStatusCodePages();

        return app;
    }
}
