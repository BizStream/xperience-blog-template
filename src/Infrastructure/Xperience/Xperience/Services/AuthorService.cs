﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Xperience.Abstractions.Retrievers;
using BlogTemplate.Infrastructure.Xperience.Extensions;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Infrastructure.Xperience.Services
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
