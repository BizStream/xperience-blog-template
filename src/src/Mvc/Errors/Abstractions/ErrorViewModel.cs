using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Errors.Abstractions;

public class ErrorViewModel
{
    public IHtmlContent Content { get; set; } = HtmlString.Empty;
    public string Heading { get; set; } = string.Empty;
}
