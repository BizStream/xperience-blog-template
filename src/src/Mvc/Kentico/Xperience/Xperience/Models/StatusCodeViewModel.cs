using BlogTemplate.Mvc.Abstractions.Models;
using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Kentico.Xperience.Models
{

    public class StatusCodeViewModel : BaseViewModel
    {

        public IHtmlContent Content { get; set; }

        public string Heading { get; set; }

    }

}
