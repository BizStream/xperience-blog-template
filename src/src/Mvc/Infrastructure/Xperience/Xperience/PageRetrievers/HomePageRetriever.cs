using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using CMS.Base;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing;
using Microsoft.Extensions.Logging;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.PageRetrievers;

public class HomePageRetriever : IHomePageRetriever
{
    #region Fields
    private readonly ILogger logger;
    private readonly IPageRetriever pageRetriever;
    private readonly ISiteService siteService;
    #endregion

    public HomePageRetriever(
        ILogger<HomePageRetriever> logger,
        IPageRetriever pageRetriever,
        ISiteService siteService
    )
    {
        this.logger = logger;
        this.pageRetriever = pageRetriever;
        this.siteService = siteService;
    }

    private void ApplyHomePageParameters( DocumentQuery<HomeNode> query )
    {
        string? homePagePath = PageRoutingHelper.GetHomePagePath( siteService.CurrentSite.SiteID );
        if( !string.IsNullOrEmpty( homePagePath ) )
        {
            query.WhereEquals( nameof( HomeNode.NodeAliasPath ), homePagePath );
        }
        else
        {
            logger.LogWarning( $"The '{PageRoutingHelper.HOME_PAGE_PATH_KEY}' SettingKey is not set.{Environment.NewLine}Setting the '{PageRoutingHelper.HOME_PAGE_PATH_KEY}' SettingKey ensures the correct '{nameof( HomeNode )}' is selected." );
        }

        query.TopN( 1 );
    }

    private static void BuildHomePageCache( IPageCacheBuilder<HomeNode> cache )
    {
        cache.Dependencies( ( _, _ ) => { }, true );
        cache.Key( PageCacheKeys.HomePage );
    }

    public async Task<HomeNode?> RetrieveAsync( CancellationToken cancellation = default )
    {
        var pages = await pageRetriever.RetrieveAsync<HomeNode>(
            ApplyHomePageParameters,
            BuildHomePageCache,
            cancellation
        );

        return pages?.FirstOrDefault();
    }
}
