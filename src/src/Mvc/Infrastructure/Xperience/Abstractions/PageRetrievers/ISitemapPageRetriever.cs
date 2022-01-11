using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

public interface ISitemapPageRetriever
{
    Task<IEnumerable<TreeNode>> RetrieveAsync( CancellationToken cancellation = default );
}
