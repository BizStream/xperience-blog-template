using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Mvc.Abstractions.Models
{
    public class BaseViewModel
    {
        public MetaData Meta { get; set; }

        public OpenGraphData OpenGraph { get; set; }
    }
}
