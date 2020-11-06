using System.Collections.Generic;

namespace BlogTemplate.Core.Abstractions.Models
{

    public class MetaData
    {

        public string Description { get; set; }

        public IEnumerable<string> Keywords { get; set; }

        public string Title { get; set; }

    }

}
