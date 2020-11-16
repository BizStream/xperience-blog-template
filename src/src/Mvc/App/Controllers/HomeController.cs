using System.Linq;
using AutoMapper;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlogTemplate.Mvc.App.Controllers
{

    [Route( "" )]
    public class HomeController : Controller
    {
        #region Fields
        private readonly IArticleService articleService;
        private readonly IBlogService blogService;
        private readonly IMapper mapper;
        #endregion

        public HomeController(
            IArticleService articleService,
            IBlogService blogService,
            IMapper mapper
        )
        {
            this.articleService = articleService;
            this.blogService = blogService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index( )
        {
            var blog = blogService.GetBlog();
            if( blog == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<HomeViewModel>( blog );
            viewModel.RecentArticles = articleService.GetRecentArticles()
                .Select( mapper.Map<ArticleListingItem> );

            return View( viewModel );
        }

    }

}
