namespace BlogTemplate.Mvc.Infrastructure.Xperience;

public class PageCacheKeys
{
    public static string AuthorPage( Guid documentGuid ) => $"{nameof( AuthorPage )}|{documentGuid}";
    public const string HomePage = nameof( HomePage );
    public static string RecentArticlePages( int count ) => $"{nameof( RecentArticlePages )}|{count}";
    public const string SitemapPages = nameof( SitemapPages );
}
