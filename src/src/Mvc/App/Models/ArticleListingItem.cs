using System;

namespace BlogTemplate.Mvc.App.Models
{

    public class ArticleListingItem
    {

        public string Summary { get; set; }

        public string Title { get; set; }

        public DateTime PublishedAt { get; set; }

        public Uri Url { get; set; }

    }

}
