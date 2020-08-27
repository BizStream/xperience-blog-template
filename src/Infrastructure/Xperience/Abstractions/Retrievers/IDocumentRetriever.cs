using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.Abstractions.Retrievers
{

    /// <summary> Describes a class that can query Documents within the Kentico Content Tree. </summary>
    public interface IDocumentRetriever
    {

        /// <summary> Query documents of the specified Page Type. </summary>
        /// <typeparam name="TNode"> The Page Type of documents to query. </typeparam>
        /// <returns> A query that retrieves documents of type <typeparamref name="TNode"/>. </returns>
        DocumentQuery<TNode> GetDocuments<TNode>( )
            where TNode : TreeNode, new();

        /// <summary> Query documents of any Page Type. </summary>
        /// <returns> A query that retrieves documents of any type. </returns>
        MultiDocumentQuery GetDocuments( );

    }

}
