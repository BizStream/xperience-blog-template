using AutoMapper;
using BizStream.Extensions.Kentico.Xperience.Caching;
using BizStream.Extensions.Kentico.Xperience.DataEngine;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Abstractions.Documents;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Services
{
    public class AuthorService : IAuthorService
    {
        #region Fields
        private readonly IMemoryCache cache;
        private readonly IDocumentRetriever documentRetriever;
        private readonly IMapper mapper;
        #endregion

        public AuthorService(
            IMemoryCache cache,
            IDocumentRetriever documentRetriever,
            IMapper mapper
        )
        {
            this.cache = cache;
            this.documentRetriever = documentRetriever;
            this.mapper = mapper;
        }

        public async Task<Author> GetAuthorAsync( Guid authorGuid )
            => await GetAuthorsAsync().ContinueWith(
                ( task, state ) => task.Result.FirstOrDefault( author => author.AuthorGuid == ( Guid )state ),
                authorGuid
            );

        public async Task<IEnumerable<Author>> GetAuthorsAsync( )
            => await cache.GetOrCreate(
                "author|all",
                async entry =>
                {
                    var query = documentRetriever.GetDocuments<AuthorNode>();
                    if( !query.Any() )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return Enumerable.Empty<Author>();
                    }

                    entry.WithCMSDependency( depends => depends.OnNodesOfType<AuthorNode>( SiteNames.BlogTemplate ) );
                    return await query.ToListAsync()
                        .ContinueWith(
                            task => task.Result.Select( mapper.Map<Author> )
                                .ToList()
                                .AsReadOnly()
                    );
                }
            );
    }
}
