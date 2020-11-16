using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Abstractions.Models;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Controllers
{

    public class AboutController : Controller
    {
        #region Fields
        private readonly IPageDataContextRetriever retriever;
        #endregion

        public AboutController(
            IPageDataContextRetriever retriever
        )
        {
            this.retriever = retriever;
        }

        public IActionResult Index( )
        {
            if( !retriever.TryRetrieve<AboutNode>( out IPageDataContext<AboutNode> data ) )
            {
                return NotFound();
            }

            // var node = data.Page;
            return View( new BaseViewModel() );
        }

    }

}
