using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Abstractions.Documents;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Extensions;
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

        public Author GetAuthor( Guid authorGuid )
            => GetAuthors().FirstOrDefault(
                author => author.AuthorGuid == authorGuid
            );

        public IEnumerable<Author> GetAuthors( )
            => cache.GetOrCreate(
                "author|all",
                entry =>
                {
                    var nodes = documentRetriever.GetDocuments<AuthorNode>()
                        .ToList();

                    if( !nodes.Any() )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return Enumerable.Empty<Author>();
                    }

                    var node = nodes.FirstOrDefault();
                    entry.SetCMSDependency( $"nodes|{node.NodeSiteName}|{node.ClassName}|all" );

                    return nodes.Select( mapper.Map<Author> ).ToList();
                }
            );

    }

}
