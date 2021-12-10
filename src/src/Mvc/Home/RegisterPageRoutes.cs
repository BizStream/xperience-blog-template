using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Home.Controllers;

[assembly: RegisterPageRoute( HomeNode.CLASS_NAME, typeof( HomeController ), ActionName = nameof( HomeController.Home ) )] 