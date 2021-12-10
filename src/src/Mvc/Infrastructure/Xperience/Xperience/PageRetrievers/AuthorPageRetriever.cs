using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.PageRetrievers;

public class AuthorPageRetriever : IAuthorPageRetriever
{
    #region Fields
    private readonly IPageRetriever pageRetriever;
    #endregion

    public AuthorPageRetriever( IPageRetriever pageRetriever )
        => this.pageRetriever = pageRetriever;

    private static void ApplyHomePageParameters( DocumentQuery<AuthorNode> query, Guid documentGuid )
    {
        query.TopN( 1 );
        query.WhereEquals( nameof( AuthorNode.DocumentGUID ), documentGuid );
    }

    private static void BuildHomePageCache( IPageCacheBuilder<AuthorNode> cache, Guid documentGuid )
    {
        cache.Dependencies( ( _, _ ) => { }, true );
        cache.Key( PageCacheKeys.AuthorPage( documentGuid ) );
    }

    public async Task<AuthorNode?> RetrieveAsync( Guid documentGuid, CancellationToken cancellation = default )
    {
        if( documentGuid == Guid.Empty )
        {
            return null;
        }

        var pages = await pageRetriever.RetrieveAsync<AuthorNode>(
            query => ApplyHomePageParameters( query, documentGuid ),
            cache => BuildHomePageCache( cache, documentGuid ),
            cancellation
        );

        return pages?.FirstOrDefault();
    }
}
