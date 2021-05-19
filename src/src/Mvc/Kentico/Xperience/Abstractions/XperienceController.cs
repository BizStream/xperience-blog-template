using AutoMapper;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using BlogTemplate.Mvc.Abstractions.Models;

namespace BlogTemplate.Mvc.Kentico.Xperience.Abstractions
{

    public abstract class XperienceController : Controller
    {

        [NonAction]
        protected virtual IActionResult PageView<TNode, TViewModel>( )
            where TNode : TreeNode, new()
            where TViewModel : BaseViewModel
        {
            var mapper = HttpContext.RequestServices.GetRequiredService<IMapper>();
            var pageRetriever = HttpContext.RequestServices.GetRequiredService<IPageDataContextRetriever>();

            if( !pageRetriever.TryRetrieve( out IPageDataContext<TNode> data ) )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<TViewModel>( data.Page );
            return View( viewModel );
        }

    }

}
