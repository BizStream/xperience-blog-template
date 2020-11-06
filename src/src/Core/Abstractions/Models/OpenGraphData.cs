using System;

namespace BlogTemplate.Core.Abstractions.Models
{

    public class OpenGraphData
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public Uri ImageUrl { get; set; }

        public Uri VideoUrl { get; set; }

    }

}
