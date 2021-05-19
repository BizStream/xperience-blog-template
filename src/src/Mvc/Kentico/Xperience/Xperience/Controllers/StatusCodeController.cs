using BlogTemplate.Mvc.Kentico.Xperience.Abstractions;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.Kentico.Xperience.Controllers
{

    public class StatusCodeController : XperienceController
    {

        public IActionResult Index( )
            => PageView<StatusCodeNode, StatusCodeViewModel>();

    }

}
