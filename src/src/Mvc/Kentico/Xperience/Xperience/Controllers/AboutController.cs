using AutoMapper;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Controllers
{

    public class AboutController : Controller
    {
        #region Fields
        private readonly IMapper mapper;
        private readonly IPageDataContextRetriever retriever;
        #endregion

        public AboutController(
            IMapper mapper,
            IPageDataContextRetriever retriever
        )
        {
            this.mapper = mapper;
            this.retriever = retriever;
        }

        public IActionResult Index( )
        {
            if( !retriever.TryRetrieve<AboutNode>( out IPageDataContext<AboutNode> data ) )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<AboutViewModel>( data.Page );
            return View( viewModel );
        }

    }

}
