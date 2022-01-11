using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

public interface IHomePageRetriever
{
    Task<HomeNode?> RetrieveAsync( CancellationToken cancellation = default );
}
