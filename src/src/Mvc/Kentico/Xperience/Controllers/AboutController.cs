using System.Data;
using System.Linq;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.Retrievers;
using BlogTemplate.Mvc.Abstractions.Models;
using CMS.DocumentEngine;
using Kentico.Content.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Controllers
{

    public class AboutController : Controller
    {
        #region Fields
        private readonly IPageDataContextInitializer pageInitializer;
        private readonly IDocumentRetriever retriever;
        #endregion

        public AboutController(
            IPageDataContextInitializer pageInitializer,
            IDocumentRetriever retriever
        )
        {
            this.pageInitializer = pageInitializer;
            this.retriever = retriever;
        }

        [HttpGet( "about" )]
        public IActionResult Index( )
        {
            var documentID = retriever.GetDocuments<AboutNode>()
                .Column( nameof( TreeNode.DocumentID ) )
                .TopN( 1 )
                .Select( row => row.Field<int>( nameof( TreeNode.DocumentID ) ) )
                .Cast<int?>()
                .FirstOrDefault();

            if( !documentID.HasValue )
            {
                return NotFound();
            }

            pageInitializer.Initialize( documentID.Value );
            return View( new BaseViewModel() );
        }

    }

}
