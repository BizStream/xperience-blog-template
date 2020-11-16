using System;

namespace BlogTemplate.Core.Abstractions.Models
{

    public class Blog
    {

        public Guid? FeaturedAuthorGuid { get; set; }

        public string Name { get; set; }

    }

}
