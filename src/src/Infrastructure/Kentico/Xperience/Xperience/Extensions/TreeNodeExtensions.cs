using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Extensions
{
    public static partial class TreeNodeExtensions
    {
        public static IEnumerable<string> GetDocumentTags( this TreeNode node )
        {
            ThrowIfNodeIsNull( node );
            return node.DocumentTags?.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
                .Select( tagName => tagName.Replace( "\"", string.Empty ).Trim() )
                .OrderBy( tagName => tagName )
                    ?? Enumerable.Empty<string>();
        }

        private static void ThrowIfNodeIsNull( TreeNode node )
        {
            if( node == null )
            {
                throw new ArgumentNullException( nameof( node ) );
            }
        }
    }
}
