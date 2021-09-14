using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Kentico.Xperience.Controllers;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterPageRoute( StatusCodeNode.CLASS_NAME, typeof( StatusCodeController ) )]
[assembly: RegisterPageRoute( AboutNode.CLASS_NAME, typeof( AboutController ) )]
