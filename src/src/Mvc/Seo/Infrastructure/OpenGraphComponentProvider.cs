using AutoMapper;
using BizStream.AspNetCore.Components.OpenGraph.Abstractions;
using CMS.DocumentEngine;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Seo.Infrastructure;

public class OpenGraphComponentProvider : IOpenGraphComponentProvider
{
    private static OpenGraphMeta? Invoke( ViewComponentContext context )
    {
        var serviceProvider = context.ViewContext.HttpContext.RequestServices;

        var pageContextRetriever = serviceProvider.GetRequiredService<IPageDataContextRetriever>();
        if( pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> pageContext ) )
        {
            return MapOpenGraph( pageContext, serviceProvider );
        }

        return null;
    }

    public Task<OpenGraphMeta?> InvokeAsync( ViewComponentContext context )
    {
        ArgumentNullException.ThrowIfNull( context );

        var openGraph = Invoke( context );
        return Task.FromResult( openGraph );
    }

    private static OpenGraphMeta MapOpenGraph( IPageDataContext<TreeNode> context, IServiceProvider serviceProvider )
    {
        var mapper = serviceProvider.GetRequiredService<IMapper>();
        var openGraph = mapper.Map(
            context.Page,
            new OpenGraphMeta { CanonicalUrl = ResolveCanonicalUrl( serviceProvider ) }
        );

        return mapper.Map( context.Metadata, openGraph );
    }

    private static Uri ResolveCanonicalUrl( IServiceProvider serviceProvider )
    {
        string? pageUrl = serviceProvider.GetRequiredService<IUrlHelper>()
            .Kentico()
            .PageCanonicalUrl();

        return new( pageUrl, UriKind.Absolute );
    }
}
