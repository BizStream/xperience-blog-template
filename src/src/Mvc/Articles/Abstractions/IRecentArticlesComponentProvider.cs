namespace BlogTemplate.Mvc.Articles.Abstractions;

public interface IRecentArticlesComponentProvider
{
    Task<IEnumerable<ArticleItem>> GetRecentArticlesAsync( int count, CancellationToken cancellation = default );
}
