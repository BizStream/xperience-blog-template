namespace BlogTemplate.Mvc.Authors.Abstractions;

public class AuthorItem
{
    public string Description { get; set; } = string.Empty;
    public Uri? ImageUrl { get; set; }
    public string Name { get; set; } = string.Empty;
}
