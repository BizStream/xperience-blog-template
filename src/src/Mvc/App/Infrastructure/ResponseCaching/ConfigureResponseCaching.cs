using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.App.Infrastructure.ResponseCaching;

public class ConfigureResponseCaching : IConfigureOptions<MvcOptions>, IPostConfigureOptions<StaticFileOptions>
{
    public void Configure( MvcOptions options ) => options.Filters.Add( typeof( GlobalResponseCacheFilter ) );

    public void PostConfigure( string name, StaticFileOptions options ) => options.OnPrepareResponse = OnPrepareResponse( options.OnPrepareResponse );

    private static Action<StaticFileResponseContext> OnPrepareResponse( Action<StaticFileResponseContext>? next )
        => context =>
        {
            var headers = context.Context.Response.GetTypedHeaders();
            headers.CacheControl = new()
            {
                MaxAge = TimeSpan.FromDays( 5 ),
                MustRevalidate = true,
                ProxyRevalidate = true,
                Public = true
            };

            next?.Invoke( context );
        };
}
