namespace BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers
{

    /// <summary> Represents options for querying documents. </summary>
    public class DocumentRetrieverOptions
    {

        /// <summary> Indicates whether permissions should be checked when querying. </summary>
        public bool CheckPermissions { get; set; } = false;

        /// <summary> The code that identifies the culture variants to query for. </summary>
        public string CultureCode { get; set; } = "en-US";

        /// <summary> Optional: The ID of the site to query nodes on. </summary>
        public int? SiteID { get; set; }

        /// <summary> The version of documents to query for. </summary>
        /// <value> <see cref="DocumentVersion.Published"/>. </value>
        public DocumentVersion Version { get; set; } = DocumentVersion.Published;

    }

}
