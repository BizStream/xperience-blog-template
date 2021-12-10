using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.About.Controllers;

public class AboutController : XperienceController
{
    public IActionResult Index( )
        => !TryRetrievePageContext<AboutNode>( out var _ )
            ? NotFound()
            : View();
}
