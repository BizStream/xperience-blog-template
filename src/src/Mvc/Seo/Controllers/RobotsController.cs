using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Seo.Controllers;

public class RobotsController : Controller
{
    [HttpGet( "robots.txt" )]
    public IActionResult Index( )
        => Content( "User-agent: * Disallow:" );
}
