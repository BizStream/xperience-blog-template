using AutoMapper;
using CMS.DocumentEngine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;

public abstract class XperienceController : Controller
{
    [NonAction]
    protected virtual IActionResult PageView<TNode, TViewModel>( string? viewName = null )
        where TNode : TreeNode, new()
        where TViewModel : class
    {
        if( !TryRetrievePageContext( out IPageDataContext<TNode> data ) )
        {
            return NotFound();
        }

        var mapper = HttpContext.RequestServices.GetRequiredService<IMapper>();
        var viewModel = mapper.Map<TViewModel>( data.Page );
        if( !string.IsNullOrEmpty( viewName ) )
        {
            return View( viewName, viewModel );
        }

        return View( viewModel );
    }

    protected virtual bool TryRetrievePageContext<TNode>( out IPageDataContext<TNode> context )
        where TNode : TreeNode, new()
        => HttpContext.RequestServices.GetRequiredService<IPageDataContextRetriever>()
            .TryRetrieve( out context );
}
