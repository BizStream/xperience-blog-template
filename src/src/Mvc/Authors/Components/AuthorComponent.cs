using BlogTemplate.Mvc.Authors.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Authors.Components;

[ViewComponent( Name = "Author" )]
public class AuthorComponent : ViewComponent
{
    #region Fields
    private readonly IAuthorComponentProvider provider;
    #endregion

    public AuthorComponent( IAuthorComponentProvider provider )
        => this.provider = provider;

    public async Task<IViewComponentResult> InvokeAsync( Guid authorGuid )
    {
        var author = await provider.GetAuthorAsync( authorGuid, HttpContext.RequestAborted );
        return View( author );
    }
}
