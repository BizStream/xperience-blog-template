namespace BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers
{

    /// <summary> Indicates the version of documents to query. </summary>
    public enum DocumentVersion
    {

        /// <summary> Indicates that the latest version of documents should be queried. </summary>
        /// <remarks> This method should be used to support Preview Mode. </remarks>
        Latest,

        /// <summary> Indicates that the published version of documents should be queried. </summary>
        /// <remarks> This method should be used by default. </remarks>
        Published

    }

}
