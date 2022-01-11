using AutoMapper;
using BlogTemplate.Mvc.Articles.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

namespace BlogTemplate.Mvc.Articles.Infrastructure;

public class RecentArticlesComponentProvider : IRecentArticlesComponentProvider
{
    #region Fields
    private readonly IArticlePageRetriever articlePageRetriever;
    private readonly IMapper mapper;
    #endregion

    public RecentArticlesComponentProvider( IArticlePageRetriever articlePageRetriever, IMapper mapper )
    {
        this.articlePageRetriever = articlePageRetriever;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ArticleItem>> GetRecentArticlesAsync( int count, CancellationToken cancellation = default )
    {
        var pages = await articlePageRetriever.RetrieveRecentAsync( count, cancellation );
        return pages.Select( mapper.Map<ArticleItem> );
    }
}
