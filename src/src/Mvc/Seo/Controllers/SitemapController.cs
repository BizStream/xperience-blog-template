using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using Microsoft.AspNetCore.Mvc;
using SimpleMvcSitemap;

namespace BlogTemplate.Mvc.Seo.Controllers;

public class SitemapController : Controller
{
    private static async Task<IEnumerable<SitemapNode>> GetXperienceSitemapNodesAsync(
        IPageUrlRetriever pageUrlRetriever,
        ISitemapPageRetriever sitemapPageRetriever,
        CancellationToken cancellation
    )
    {
        var pages = await sitemapPageRetriever.RetrieveAsync( cancellation );
        return pages.Select(
            page =>
            {
                var url = pageUrlRetriever.Retrieve( page );
                return new SitemapNode( url.AbsoluteUrl );
            }
        );
    }

    [HttpGet( "sitemap.xml" )]
    public async Task<IActionResult> Index(
        [FromServices] IPageUrlRetriever pageUrlRetriever,
        [FromServices] ISitemapProvider sitemapProvider,
        [FromServices] ISitemapPageRetriever sitemapPageRetriever
    )
    {
        var nodes = await GetXperienceSitemapNodesAsync( pageUrlRetriever, sitemapPageRetriever, HttpContext.RequestAborted );

        var model = new SitemapModel( nodes.ToList() );
        return sitemapProvider.CreateSitemap( model );
    }
}
