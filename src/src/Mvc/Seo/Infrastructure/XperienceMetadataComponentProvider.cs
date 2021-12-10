using BizStream.AspNetCore.Components.Metadata.Abstractions;
using CMS.DocumentEngine;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Seo.Infrastructure;

public class XperienceMetadataComponentProvider : IMetadataComponentProvider
{
    private static Meta? Invoke( ViewComponentContext context )
    {
        ArgumentNullException.ThrowIfNull( context );

        var serviceProvider = context.ViewContext.HttpContext.RequestServices;

        var pageContextRetriever = serviceProvider.GetRequiredService<IPageDataContextRetriever>();
        if( pageContextRetriever.TryRetrieve( out IPageDataContext<TreeNode> pageContext ) )
        {
            var pageMeta = pageContext.Metadata;
            return new()
            {
                CanonicalUrl = new(
                    serviceProvider.GetRequiredService<IUrlHelper>()
                        .Kentico()
                        .PageCanonicalUrl()
                ),
                Description = pageMeta.Description,
                Keywords = pageMeta.Keywords.Split( ',', StringSplitOptions.RemoveEmptyEntries )
                    .Select( keyword => keyword.Trim() ),
                Title = pageMeta.Title
            };
        }

        return null;
    }

    public Task<Meta?> InvokeAsync( ViewComponentContext context )
    {
        var meta = Invoke( context );
        return Task.FromResult( meta );
    }
}
