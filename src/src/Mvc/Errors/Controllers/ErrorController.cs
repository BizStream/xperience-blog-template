using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using BlogTemplate.Mvc.Errors.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Errors.Controllers;

public class ErrorController : XperienceController
{
    public IActionResult Index( )
        => PageView<StatusCodeNode, ErrorViewModel>();

    [HttpGet( "error" )]
    public IActionResult Error( )
        => StatusCode( 500 );
}
