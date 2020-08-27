using System.Linq;
using AutoMapper;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.App.Models;
using CMS.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace BlogTemplate.Mvc.App.Controllers
{

    [Route( "" )]
    public class HomeController : Controller
    {
        #region Fields
        private readonly IArticleService articleService;
        private readonly IHomeService homeService;
        private readonly IMapper mapper;
        #endregion

        public HomeController(
            IArticleService articleService,
            IHomeService homeService,
            IMapper mapper
        )
        {
            this.articleService = articleService;
            this.homeService = homeService;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index( )
        {
            var home = homeService.GetHome();
            if( home == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<HomeViewModel>( home );
            viewModel.RecentArticles = articleService.GetRecentArticles()
                .Select( mapper.Map<ArticleListingItem> );

            return View( viewModel );
        }

    }

}
