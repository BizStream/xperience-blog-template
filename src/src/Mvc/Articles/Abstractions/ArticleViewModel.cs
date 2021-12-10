using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Articles.Abstractions;

public class ArticleViewModel
{
    public Guid AuthorGuid { get; set; }
    public IHtmlContent Content { get; set; } = HtmlString.Empty;
    public string Heading { get; set; } = string.Empty;
    public Uri? HeroImageUrl { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime PublishedAt { get; set; }
}
