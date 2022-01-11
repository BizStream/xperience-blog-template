using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using BlogTemplate.Mvc.Errors.Controllers;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterPageRoute( StatusCodeNode.CLASS_NAME, typeof( ErrorController ) )] 