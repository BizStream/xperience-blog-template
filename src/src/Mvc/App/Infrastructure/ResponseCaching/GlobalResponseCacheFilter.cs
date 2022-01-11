using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace BlogTemplate.Mvc.App.Infrastructure.ResponseCaching;

/// <summary> A global ActionFilter that ensure variance of ResponseCache entries via action parameters. </summary>
public class GlobalResponseCacheFilter : IFilterFactory
{
    #region Fields
    private static readonly string[] DefaultVaryByHeaders = new[]
    {
        HeaderNames.AcceptEncoding,
        HeaderNames.AcceptLanguage,
        HeaderNames.ContentLanguage
    };

    private static readonly string[] DefaultVaryByQueryKeys = Array.Empty<string>();

    private readonly ResponseCacheAttribute responseCacheAttribute;
    #endregion

    public GlobalResponseCacheFilter( )
        => responseCacheAttribute = new ResponseCacheAttribute
        {
            Duration = 30,
            Location = ResponseCacheLocation.Any,
            VaryByHeader = string.Join( ",", DefaultVaryByHeaders ),
            VaryByQueryKeys = DefaultVaryByQueryKeys
        };

    #region Properties
    public int Duration { get => responseCacheAttribute.Duration; set => responseCacheAttribute.Duration = value; }
    public ResponseCacheLocation Location { get => responseCacheAttribute.Location; set => responseCacheAttribute.Location = value; }
    public bool IsReusable => true;
    #endregion

    public IFilterMetadata CreateInstance( IServiceProvider serviceProvider )
        => new GlobalResponseCacheActionFilter(
            ( IActionFilter )responseCacheAttribute.CreateInstance( serviceProvider ) );

    private class GlobalResponseCacheActionFilter : IActionFilter
    {
        #region Field
        private readonly IActionFilter filter;
        #endregion

        public GlobalResponseCacheActionFilter( IActionFilter filter )
            => this.filter = filter;

        public void OnActionExecuted( ActionExecutedContext context )
            => filter.OnActionExecuted( context );

        public void OnActionExecuting( ActionExecutingContext context )
        {
            // execute base ResponseCacheFilter
            filter.OnActionExecuting( context );

            // Ensure variance by action parameters bound to headers/query params
            var responseCaching = context.HttpContext.Features.Get<IResponseCachingFeature?>();
            if( responseCaching is not null )
            {
                var varyByKeyParameters = context.ActionDescriptor.Parameters
                    ?.Where( parameter => parameter.BindingInfo?.BindingSource == BindingSource.Query )
                    .Select( parameter => parameter.BindingInfo!.BinderModelName ?? parameter.Name );

                if( varyByKeyParameters?.Any() is true )
                {
                    var varyByKeys = responseCaching.VaryByQueryKeys
                        ?? Enumerable.Empty<string>();

                    responseCaching.VaryByQueryKeys = varyByKeys
                        .Concat( varyByKeyParameters )
                        .Distinct()
                        .ToArray();

                    context.HttpContext.Features.Set( responseCaching );
                }

                var varyByHeaderParameters = context.ActionDescriptor.Parameters
                    ?.Where( parameter => parameter.BindingInfo?.BindingSource == BindingSource.Header )
                    .Select( parameter => parameter.BindingInfo!.BinderModelName ?? parameter.Name );

                if( varyByHeaderParameters?.Any() is true )
                {
                    var varyByHeaders = context.HttpContext.Response.Headers.TryGetValue( HeaderNames.Vary, out var values )
                        ? values
                        : Enumerable.Empty<string>();

                    context.HttpContext.Response.Headers[ HeaderNames.Vary ] = new StringValues(
                        varyByHeaders.Concat( varyByHeaderParameters )
                            .Distinct()
                            .ToArray()
                    );
                }
            }
        }
    }
}
