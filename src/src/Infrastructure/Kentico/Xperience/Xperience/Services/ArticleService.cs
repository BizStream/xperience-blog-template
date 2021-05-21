using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BizStream.Extensions.Kentico.Xperience.Caching;
using BizStream.Extensions.Kentico.Xperience.DataEngine;
using BizStream.Extensions.Kentico.Xperience.Retrievers.Abstractions.Documents;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using CMS.DocumentEngine;
using Microsoft.Extensions.Caching.Memory;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Services
{

    public class ArticleService : IArticleService, IMetaDataService<Article>
    {
        #region Fields
        private readonly IMemoryCache cache;
        private readonly IDocumentRetriever documentRetriever;
        private readonly IMapper mapper;
        #endregion

        public ArticleService(
            IMemoryCache cache,
            IDocumentRetriever documentRetriever,
            IMapper mapper
        )
        {
            this.cache = cache;
            this.documentRetriever = documentRetriever;
            this.mapper = mapper;
        }

        public async Task<Article> GetArticleAsync( string slug )
        {
            var node = await GetArticleNodeAsync( slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<Article>( node );
        }

        private async Task<ArticleNode> GetArticleNodeAsync( string slug )
            => await cache.GetOrCreateAsync(
                $"article|node|{slug}",
                async entry =>
                {
                    var node = await documentRetriever.GetDocuments<ArticleNode>()
                        .WhereEquals( nameof( TreeNode.NodeAlias ), slug )
                        .TopN( 1 )
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

        public async Task<MetaData> GetMetaDataAsync( Article article )
        {
            var node = await GetArticleNodeAsync( article?.Slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<MetaData>( node );
        }

        public async Task<OpenGraphData> GetOpenGraphDataAsync( Article article )
        {
            var node = await GetArticleNodeAsync( article?.Slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<OpenGraphData>( node );
        }

        public async Task<IEnumerable<Article>> GetRecentArticlesAsync( )
            => await cache.GetOrCreateAsync(
                $"article|recent",
                async entry =>
                {
                    var query = documentRetriever.GetDocuments<ArticleNode>()
                        .OrderByDescending( SyntheticDocumentFields.PublishedAtDate )
                        .TopN( 5 );

                    if( !query.Any() )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return Enumerable.Empty<Article>();
                    }

                    entry.WithCMSDependency( depends => depends.OnNodesOfType<ArticleNode>( SiteNames.BlogTemplate ) );
                    return await query.ToListAsync()
                        .ContinueWith(
                            task => task.Result.Select( mapper.Map<Article> )
                                .OrderByDescending( article => article.PublishedAt )
                                .ToList()
                                .AsReadOnly()
                        );
                }
            );

    }

}
