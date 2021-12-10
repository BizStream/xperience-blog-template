using AutoMapper;
using BlogTemplate.Mvc.Authors.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions.PageRetrievers;

namespace BlogTemplate.Mvc.Authors.Infrastructure;

public class AuthorComponentProvider : IAuthorComponentProvider
{
    #region Fields
    private readonly IAuthorPageRetriever authorPageRetriever;
    private readonly IMapper mapper;
    #endregion

    public AuthorComponentProvider( IAuthorPageRetriever authorPageRetriever, IMapper mapper )
    {
        this.authorPageRetriever = authorPageRetriever;
        this.mapper = mapper;
    }

    public async Task<AuthorItem?> GetAuthorAsync( Guid authorGuid, CancellationToken cancellation = default )
    {
        if( authorGuid == Guid.Empty )
        {
            return null;
        }

        var page = await authorPageRetriever.RetrieveAsync( authorGuid, cancellation );
        return mapper.Map<AuthorItem>( page );
    }
}
