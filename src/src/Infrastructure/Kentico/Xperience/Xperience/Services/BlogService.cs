using System;
using System.Linq;
using AutoMapper;
using BizStream.Extensions.Kentico.Xperience.Caching;
using BizStream.Extensions.Kentico.Xperience.DocumentEngine;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Abstractions.Documents;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Services
{

    public class BlogService : IBlogService, IMetaDataService<Blog>
    {
        #region Fields
        private readonly IMemoryCache cache;
        private readonly IDocumentRetriever documentRetriever;
        private readonly IMapper mapper;
        #endregion

        public BlogService(
            IMemoryCache cache,
            IDocumentRetriever documentRetriever,
            IMapper mapper
        )
        {
            this.cache = cache;
            this.documentRetriever = documentRetriever;
            this.mapper = mapper;
        }

        public Blog GetBlog( )
        {
            var node = GetHomeNode();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<Blog>( node );
        }

        private HomeNode GetHomeNode( )
            => cache.GetOrCreate(
                "home|node",
                entry =>
                {
                    var node = documentRetriever.GetDocuments<HomeNode>()
                        .AtRootLevel()
                        .FirstOrDefault();

                    if( node == null )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return null;
                    }

                    entry.WithCMSDependency( depends => depends.OnNode( node ) );
                    return node;
                }
            );

        public MetaData GetMetaData( Blog entity )
        {
            var node = GetHomeNode();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<MetaData>( node );
        }

        public OpenGraphData GetOpenGraphData( Blog entity )
        {
            var node = GetHomeNode();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<OpenGraphData>( node );
        }
    }
}
