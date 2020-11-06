using System;
using System.Linq;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.Retrievers;
using BlogTemplate.Infrastructure.Kentico.Xperience.Extensions;
using CMS.DocumentEngine;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Services
{

    public class HomeService : IHomeService, IMetaDataService<Home>
    {
        #region Fields
        private readonly IMemoryCache cache;
        private readonly IDocumentRetriever documentRetriever;
        private readonly IMapper mapper;
        #endregion

        public HomeService(
            IMemoryCache cache,
            IDocumentRetriever documentRetriever,
            IMapper mapper
        )
        {
            this.cache = cache;
            this.documentRetriever = documentRetriever;
            this.mapper = mapper;
        }

        public Home GetHome( )
        {
            var node = GetHomeNode();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<Home>( node );
        }

        private HomeNode GetHomeNode( )
            => cache.GetOrCreate(
                "home|node",
                entry =>
                {
                    var node = documentRetriever.GetDocuments<HomeNode>()
                        .OrderByAscending( nameof( TreeNode.NodeLevel ) )
                        .TopN( 1 )
                        .FirstOrDefault();

                    if( node == null )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return null;
                    }

                    entry.SetCMSDependency( $"nodeid|{node.NodeID}" );
                    return node;
                }
            );

        public MetaData GetMetaData( Home entity )
        {
            var node = GetHomeNode();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<MetaData>( node );
        }

        public OpenGraphData GetOpenGraphData( Home entity )
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
