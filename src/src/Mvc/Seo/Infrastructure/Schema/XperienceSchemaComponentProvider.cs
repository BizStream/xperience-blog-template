using BizStream.AspNetCore.Components.Schema.Abstractions;
using CMS.DocumentEngine;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Schema.NET;

namespace BlogTemplate.Mvc.Seo.Infrastructure.Schema;

public abstract class XperienceSchemaComponentProvider<TNode> : ISchemaComponentProvider
    where TNode : TreeNode, new()
{
    public async Task<IThing?> InvokeAsync( ViewComponentContext context )
    {
        ArgumentNullException.ThrowIfNull( context );

        var serviceProvider = context.ViewContext.HttpContext.RequestServices;

        var pageContextRetriever = serviceProvider.GetRequiredService<IPageDataContextRetriever>();
        if( pageContextRetriever.TryRetrieve<TNode>( out var pageContext ) )
        {
            return await InvokeAsync( context, pageContext );
        }

        return null;
    }

    protected abstract Task<IThing?> InvokeAsync( ViewComponentContext context, IPageDataContext<TNode> pageContext );
}
