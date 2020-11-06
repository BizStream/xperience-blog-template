using System;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.Retrievers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers
{

    /// <summary> A class that can query Kentico Documents. </summary>
    public class DocumentRetriever : IDocumentRetriever
    {
        #region Fields
        private readonly IOptions<DocumentRetrieverOptions> options;
        #endregion

        public DocumentRetriever( IOptionsSnapshot<DocumentRetrieverOptions> options )
            => this.options = options;

        /// <summary> Modifies the query based on the <see cref="DocumentRetrieverOptions"/>. </summary>
        /// <returns> The modified query. </returns>
        protected virtual TQuery ApplyOptions<TQuery, TNode>( IDocumentQuery<TQuery, TNode> query )
            where TQuery : IDocumentQuery<TQuery, TNode>, new()
            where TNode : TreeNode, new()
        {
            if( query == null )
            {
                throw new ArgumentNullException( nameof( query ) );
            }

            var typedQuery = query.GetTypedQuery()
                .CheckPermissions( options.Value.CheckPermissions );

            if( !string.IsNullOrWhiteSpace( options.Value.CultureCode ) )
            {
                typedQuery = typedQuery.Culture( options.Value.CultureCode );
            }

            if( options.Value.Version == DocumentVersion.Latest )
            {
                typedQuery = typedQuery.LatestVersion( true );
            }
            else if( options.Value.Version == DocumentVersion.Published )
            {
                typedQuery = typedQuery.PublishedVersion( true );
            }

            if( options.Value.SiteID.HasValue )
            {
                typedQuery = typedQuery.WhereEquals( nameof( TreeNode.NodeSiteID ), options.Value.SiteID );
            }

            return EnsureSyntheticFields( typedQuery );
        }

        protected virtual TQuery EnsureSyntheticFields<TQuery, TNode>( IDocumentQuery<TQuery, TNode> query )
            where TQuery : IDocumentQuery<TQuery, TNode>, new()
            where TNode : TreeNode, new()
        {
            if( query == null )
            {
                throw new ArgumentNullException( nameof( query ) );
            }

            var publishedColumn = new QueryColumn( "CASE WHEN DocumentPublishFrom IS NOT NULL THEN DocumentPublishFrom ELSE DocumentCreatedWhen END" );
            var modifiedColumn = new QueryColumn( $"CASE WHEN DocumentModifiedWhen IS NOT NULL THEN DocumentModifiedWhen ELSE {publishedColumn} END" );

            var typedQuery = query.GetTypedQuery();
            typedQuery = typedQuery.AddColumns(
                publishedColumn.As( SyntheticDocumentFields.PublishedAtDate ),
                modifiedColumn.As( SyntheticDocumentFields.ModifiedAtDate )
            );

            return typedQuery;
        }

        /// <inheritdoc />
        public virtual DocumentQuery<TNode> GetDocuments<TNode>( )
            where TNode : TreeNode, new()
            => ApplyOptions( DocumentHelper.GetDocuments<TNode>() );

        /// <inheritdoc />
        public virtual MultiDocumentQuery GetDocuments( )
            => ApplyOptions( DocumentHelper.GetDocuments() );

    }

}
