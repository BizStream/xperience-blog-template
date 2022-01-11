using AutoMapper;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.AutoMapper.Resolvers;

/// <summary> Resolves the publish date for a document. </summary>
/// <remarks> This implementation resolves the value of <see cref="TreeNode.DocumentCreatedWhen"/>, if <see cref="TreeNode.DocumentPublishFrom"/> does not have a value. </remarks>
public class DocumentPublishedDateResolver : IValueResolver<TreeNode, object, DateTime>
{
    public DateTime Resolve( TreeNode source, object _, DateTime __, ResolutionContext ___ )
    {
        if( source is null )
        {
            throw new ArgumentNullException( nameof( source ) );
        }

        return source.DocumentPublishFrom != DateTime.MinValue
            ? source.DocumentPublishFrom
            : source.DocumentCreatedWhen;
    }
}
