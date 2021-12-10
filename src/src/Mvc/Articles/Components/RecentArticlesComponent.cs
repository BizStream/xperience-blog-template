using BlogTemplate.Mvc.Articles.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Articles.Components;

[ViewComponent( Name = "RecentArticles" )]
public class RecentArticlesComponent : ViewComponent
{
    #region Fields
    private readonly IRecentArticlesComponentProvider provider;
    #endregion

    public RecentArticlesComponent( IRecentArticlesComponentProvider provider )
        => this.provider = provider;

    public async Task<IViewComponentResult> InvokeAsync( int count )
    {
        var articles = await provider.GetRecentArticlesAsync( count, HttpContext.RequestAborted );
        return View( articles );
    }
}
