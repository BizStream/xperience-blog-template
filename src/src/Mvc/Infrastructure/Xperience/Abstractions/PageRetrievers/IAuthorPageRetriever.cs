using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

public interface IAuthorPageRetriever
{
    Task<AuthorNode?> RetrieveAsync( Guid documentGuid, CancellationToken cancellation = default );
}
