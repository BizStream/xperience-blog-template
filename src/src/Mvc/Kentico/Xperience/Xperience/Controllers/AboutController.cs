using System.Linq;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Abstractions.Models;
using CMS.Core;
using CMS.IO;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

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

            var env = Service.Resolve<IWebHostEnvironment>();
            var options = Service.Resolve<IOptions<PageBuilderBundlesOptions>>();
            var files = options.Value
                .PageBuilderAdminScripts
                .Contents
                .EnumerateFiles( env.WebRootPath )
                .ToList();

            // var node = data.Page;
            return View( new BaseViewModel() );
        }

    }

}
