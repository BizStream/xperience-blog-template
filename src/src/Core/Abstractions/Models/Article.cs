namespace BlogTemplate.Core.Abstractions.Models
{
    /// <summary> Represents an instance of marketing content. </summary>
    public class Article : IAuthored
    {
        /// <inheritdoc />
        public Guid AuthorGuid { get; set; }

        /// <summary> The Rich (WYSISWYG) text content. </summary>
        public string Content { get; set; }

        /// <summary> A URL to an associated image. </summary>
        public Uri HeroImageUrl { get; set; }

        public DateTime PublishedAt { get; set; }

        /// <summary> A URL (SEO) friendly, unique identifier. </summary>
        public string Slug { get; set; }

        /// <summary> A brief explanation of the article's content. </summary>
        public string Summary { get; set; }

        public string Title { get; set; }

        /// <inheritdoc />
        public DateTime LastAuthoredAt { get; set; }
    }
}
