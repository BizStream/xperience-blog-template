using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Articles.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Articles.Controllers;

public class ArticlesController : XperienceController
{
    public IActionResult Article( )
        => PageView<ArticleNode, ArticleViewModel>();
}
