namespace BlogTemplate.Mvc.App.Infrastructure;

public static class IApplicationBuilderExtensions
{
    public static IApplicationBuilder UseRequestCancellation( this IApplicationBuilder app )
    {
        ArgumentNullException.ThrowIfNull( app );

        app.UseMiddleware<RequestCancellationMiddleware>();
        return app;
    }
}
