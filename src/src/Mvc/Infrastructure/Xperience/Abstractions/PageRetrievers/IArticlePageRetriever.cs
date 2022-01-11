using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

public interface IArticlePageRetriever
{
    Task<IEnumerable<ArticleNode>> RetrieveRecentAsync( int count, CancellationToken cancellation = default );
}
