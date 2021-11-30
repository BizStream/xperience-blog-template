using AutoMapper;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Resolvers
{
    /// <summary> Resolves the publish date for a document. </summary>
    /// <remarks> This implementation resolves the value of <see cref="TreeNode.DocumentCreatedWhen"/>, if <see cref="TreeNode.DocumentPublishFrom"/> does not have a value. </remarks>
    public class DocumentPublishDateResolver : IValueResolver<TreeNode, object, DateTime>
    {
        public DateTime Resolve( TreeNode source, object destination, DateTime destMember, ResolutionContext context )
        {
            if( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            var published = source.GetDateTimeValue( SyntheticDocumentFields.PublishedAtDate, source.DocumentPublishFrom );
            return published != DateTime.MinValue
                ? published
                : source.DocumentCreatedWhen;
        }
    }
}
