namespace BlogTemplate.Mvc.Articles.Abstractions;

public class ArticleItem
{
    public DateTime PublishedAt { get; set; }
    public string Summary { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public Uri? Url { get; set; }
}
