namespace BlogTemplate.Mvc.Home.Abstractions;

public class HomeViewModel
{
    public Guid? FeaturedAuthorGuid { get; set; }
    public string Heading { get; set; } = string.Empty;
}
