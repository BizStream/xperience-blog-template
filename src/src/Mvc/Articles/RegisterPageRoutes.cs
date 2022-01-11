using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Articles.Controllers;
using Kentico.Content.Web.Mvc.Routing;

[assembly: RegisterPageRoute( ArticleNode.CLASS_NAME, typeof( ArticlesController ), ActionName = nameof( ArticlesController.Article ) )]