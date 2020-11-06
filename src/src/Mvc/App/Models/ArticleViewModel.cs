using System;
using BlogTemplate.Mvc.Abstractions.Models;

namespace BlogTemplate.Mvc.App.Models
{

    public class ArticleViewModel : BaseViewModel
    {

        public Guid AuthorGuid { get; set; }

        public string Content { get; set; }

        public Uri HeroImageUrl { get; set; }

        public DateTime ModifiedAt { get; set; }

        public DateTime PublishedAt { get; set; }

    }

}
