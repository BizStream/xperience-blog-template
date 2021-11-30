using AutoMapper;
using BizStream.Extensions.Kentico.Xperience.Caching;
using BizStream.Extensions.Kentico.Xperience.DataEngine;
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

        public async Task<Blog> GetBlogAsync( )
        {
            var node = await GetHomeNodeAsync();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<Blog>( node );
        }

        private async Task<HomeNode> GetHomeNodeAsync( )
            => await cache.GetOrCreateAsync(
                "home|node",
                async entry =>
                {
                    var node = await documentRetriever.GetDocuments<HomeNode>()
                        .AtRootLevel()
                        .FirstOrDefaultAsync();

                    if( node == null )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return null;
                    }

                    entry.WithCMSDependency( depends => depends.OnNode( node ) );
                    return node;
                }
            );

        public async Task<MetaData> GetMetaDataAsync( Blog entity )
        {
            var node = await GetHomeNodeAsync();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<MetaData>( node );
        }

        public async Task<OpenGraphData> GetOpenGraphDataAsync( Blog entity )
        {
            var node = await GetHomeNodeAsync();
            if( node == null )
            {
                return null;
            }

            return mapper.Map<OpenGraphData>( node );
        }
    }
}
