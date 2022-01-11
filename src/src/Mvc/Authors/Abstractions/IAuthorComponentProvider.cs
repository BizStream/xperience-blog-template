namespace BlogTemplate.Mvc.Authors.Abstractions;

public interface IAuthorComponentProvider
{
    Task<AuthorItem?> GetAuthorAsync( Guid authorGuid, CancellationToken cancellation = default );
}
