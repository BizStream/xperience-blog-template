using System;
using System.Linq;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.Retrievers;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Extensions
{

    public static class IDocumentRetrieverExtensions
    {

        /// <summary> Query a document for the node with the given <paramref name="nodeAlias"/>. </summary>
        /// <typeparam name="TNode"> The Page Type of documents to query. </typeparam>
        /// <param name="nodeAlias"> The <see cref="TreeNode.NodeAlias"/> of the document to retrieve. </param>
        /// <returns> A document for the node identified by the given <paramref name="nodeAlias"/>. </returns>
        public static TNode GetDocument<TNode>( this IDocumentRetriever retriever, string nodeAlias )
            where TNode : TreeNode, new()
        {
            ThrowIfRetrieverIsNull( retriever );
            if( string.IsNullOrWhiteSpace( nodeAlias ) )
            {
                return null;
            }

            return retriever.GetDocuments<TNode>()
                .WhereEquals( nameof( TreeNode.NodeAlias ), nodeAlias )
                .TopN( 1 )
                .FirstOrDefault();
        }

        private static void ThrowIfRetrieverIsNull( IDocumentRetriever retriever )
        {
            if( retriever == null )
            {
                throw new ArgumentNullException( nameof( retriever ) );
            }
        }

    }

}
