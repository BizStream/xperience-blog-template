using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Home.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Home.Controllers;

public class HomeController : XperienceController
{
    public IActionResult Home( )
        => PageView<HomeNode, HomeViewModel>( nameof( Index ) );

    [HttpGet( "" )]
    public async Task<IActionResult> Index(
        [FromServices] IHomePageRetriever homeRetriever,
        [FromServices] IPageDataContextInitializer pageContextInitializer
    )
    {
        var page = await homeRetriever.RetrieveAsync( HttpContext.RequestAborted );
        if( page is null )
        {
            return NotFound();
        }

        pageContextInitializer.Initialize( page );
        return Home();
    }
}
