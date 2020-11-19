using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.Retrievers;
using BlogTemplate.Infrastructure.Kentico.Xperience.Extensions;
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

        public Article GetArticle( string slug )
        {
            var node = GetArticleNode( slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<Article>( node );
        }

        private ArticleNode GetArticleNode( string slug )
            => cache.GetOrCreate(
                $"article|node|{slug}",
                entry =>
                {
                    var node = documentRetriever.GetDocument<ArticleNode>( slug );
                    if( node == null )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return null;
                    }

                    entry.SetCMSDependency( $"nodeid|{node.NodeID}" );
                    return node;
                }
            );

        public MetaData GetMetaData( Article article )
        {
            var node = GetArticleNode( article?.Slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<MetaData>( node );
        }

        public OpenGraphData GetOpenGraphData( Article article )
        {
            var node = GetArticleNode( article?.Slug );
            if( node == null )
            {
                return null;
            }

            return mapper.Map<OpenGraphData>( node );
        }

        public IEnumerable<Article> GetRecentArticles( )
            => cache.GetOrCreate(
                $"article|recent",
                entry =>
                {
                    var nodes = documentRetriever.GetDocuments<ArticleNode>()
                        .OrderByDescending( SyntheticDocumentFields.PublishedAtDate )
                        .TopN( 5 )
                        .ToList();

                    if( !nodes.Any() )
                    {
                        entry.SetAbsoluteExpiration( TimeSpan.FromMinutes( 20 ) );
                        return Enumerable.Empty<Article>();
                    }

                    var node = nodes.FirstOrDefault();
                    entry.SetCMSDependency( $"nodes|{node.NodeSiteName}|{node.ClassName}|all" );

                    return nodes.Select( mapper.Map<Article> )
                        .OrderByDescending( article => article.PublishedAt )
                        .ToList();
                }
            );

    }

}
