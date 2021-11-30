using BlogTemplate.Mvc.Abstractions.Models;

namespace BlogTemplate.Mvc.App.Models
{
    public class HomeViewModel : BaseViewModel
    {
        public Guid? FeaturedAuthorGuid { get; set; }

        public string Heading { get; set; }

        public IEnumerable<ArticleListingItem> RecentArticles { get; set; }
    }
}
