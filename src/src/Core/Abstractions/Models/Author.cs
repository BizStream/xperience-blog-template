namespace BlogTemplate.Core.Abstractions.Models
{
    /// <summary> Represents information about an individual that maintains an instance of an entity, or the data an entity may represent. </summary>
    public class Author
    {
        /// <summary> The unique identifier of the individual. </summary>
        public Guid AuthorGuid { get; set; }

        /// <summary> A brief explanation of the individual. </summary>
        public string Description { get; set; }

        public Uri FacebookUrl { get; set; }

        public Uri GitHubUrl { get; set; }

        /// <summary> A url to an image of the individual. </summary>
        public Uri ImageUrl { get; set; }

        /// <summary> The name of the individual. </summary>
        public string Name { get; set; }

        public Uri TwitterUrl { get; set; }
    }
}
