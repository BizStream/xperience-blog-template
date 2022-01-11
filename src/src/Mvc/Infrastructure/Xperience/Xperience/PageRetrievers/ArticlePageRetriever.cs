using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.PageRetrievers;

public class ArticlePageRetriever : IArticlePageRetriever
{
    #region Fields
    private readonly IPageRetriever pageRetriever;
    #endregion

    public ArticlePageRetriever( IPageRetriever pageRetriever )
        => this.pageRetriever = pageRetriever;

    private static void ApplyRecentArticleParameters( DocumentQuery<ArticleNode> query, int count )
    {
        query.OrderByDescending(
            nameof( ArticleNode.DocumentPublishFrom ),
            nameof( ArticleNode.DocumentCreatedWhen )
        );

        query.TopN( count );
    }

    private static void BuildRecentArticleCache( IPageCacheBuilder<ArticleNode> cache, int count )
    {
        cache.Dependencies( ( _, _ ) => { }, true );
        cache.Key( PageCacheKeys.RecentArticlePages( count ) );
    }

    public async Task<IEnumerable<ArticleNode>> RetrieveRecentAsync( int count, CancellationToken cancellation = default )
        => await pageRetriever.RetrieveAsync<ArticleNode>(
            query => ApplyRecentArticleParameters( query, count ),
            cache => BuildRecentArticleCache( cache, count ),
            cancellation
        );
}
