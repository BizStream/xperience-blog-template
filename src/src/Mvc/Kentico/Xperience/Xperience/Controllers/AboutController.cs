using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Kentico.Xperience.Abstractions;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Controllers
{

    public class AboutController : XperienceController
    {

        public IActionResult Index( )
            => PageView<AboutNode, AboutViewModel>();

    }

}
