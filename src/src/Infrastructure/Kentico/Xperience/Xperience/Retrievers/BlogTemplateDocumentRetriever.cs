using BizStream.Extensions.Kentico.Xperience.Retrievers.Documents;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers
{
    /// <summary> A class that can query Kentico Documents. </summary>
    public class BlogTemplateDocumentRetriever : DocumentRetriever
    {
        public BlogTemplateDocumentRetriever( IOptionsSnapshot<DocumentRetrieverOptions> options )
            : base( options )
        {
        }

        protected override TQuery ApplyOptions<TQuery, TNode>( IDocumentQuery<TQuery, TNode> query )
           => EnsureSyntheticFields( base.ApplyOptions( query ) );

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
    }
}
