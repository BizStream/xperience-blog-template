using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using CMS.DocumentEngine;
using CMS.DocumentEngine.Routing;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.PageRetrievers;

public class SitemapPageRetriever : ISitemapPageRetriever
{
    #region Fields
    private readonly IPageRetriever pageRetriever;
    #endregion

    public SitemapPageRetriever( IPageRetriever pageRetriever )
        => this.pageRetriever = pageRetriever;

    private static void ApplySitemapParameters( DocumentQuery<TreeNode> query )
    {
        query.WithPageUrlPaths()
            .WhereNotEmpty( nameof( PageUrlPathInfo.PageUrlPathUrlPath ) )
            .WhereEqualsOrNull( nameof( TreeNode.DocumentSearchExcluded ), false );

        query.OrderBy( nameof( PageUrlPathInfo.PageUrlPathUrlPath ) );
    }

    private static void BuildSitemapCache( IPageCacheBuilder<TreeNode> cache )
    {
        cache.Dependencies( ( _, _ ) => { }, true );
        cache.Key( PageCacheKeys.SitemapPages );
    }

    public Task<IEnumerable<TreeNode>> RetrieveAsync( CancellationToken cancellation = default )
        => pageRetriever.RetrieveAsync<TreeNode>( ApplySitemapParameters, BuildSitemapCache, cancellation );
}
