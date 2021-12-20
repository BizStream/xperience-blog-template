namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;

public static class PageMetadataExtensions
{
    public static IEnumerable<string> GetNormalizedKeywords( this IPageMetadata metadata )
    {
        ArgumentNullException.ThrowIfNull( metadata );
        return metadata.Keywords?.Split( ',', StringSplitOptions.RemoveEmptyEntries )
            .Select( keyword => keyword.Trim() );
    }
}
