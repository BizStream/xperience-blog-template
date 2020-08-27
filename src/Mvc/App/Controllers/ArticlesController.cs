using AutoMapper;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.App.Controllers
{

    public class ArticlesController : Controller
    {
        #region Fields
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        #endregion

        public ArticlesController(
            IArticleService articleService,
            IMapper mapper
        )
        {
            this.articleService = articleService;
            this.mapper = mapper;
        }

        [HttpGet( "articles" )]
        public IActionResult Index( )
            => NotFound();

        [HttpGet( "articles/{slug}" )]
        public IActionResult Article( string slug )
        {
            var article = articleService.GetArticle( slug );
            if( article == null )
            {
                return NotFound();
            }

            var viewModel = mapper.Map<ArticleViewModel>( article );
            return View( viewModel );
        }

    }

}
