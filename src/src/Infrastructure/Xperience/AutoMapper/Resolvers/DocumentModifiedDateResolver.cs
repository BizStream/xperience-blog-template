using AutoMapper;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.AutoMapper.Resolvers;

/// <summary> Resolves the last modified date for a document. </summary>
/// <remarks> This implementation resolves the value using <see cref="DocumentPublishDateResolver"/>, if <see cref="TreeNode.DocumentModifiedWhen"/> does not have a value. </remarks>
public class DocumentModifiedDateResolver : IValueResolver<TreeNode, object, DateTime>
{
    #region Fields
    private readonly DocumentPublishedDateResolver publishDateResolver;
    #endregion

    public DocumentModifiedDateResolver( DocumentPublishedDateResolver publishDateResolver )
        => this.publishDateResolver = publishDateResolver;

    public DateTime Resolve( TreeNode source, object destination, DateTime destMember, ResolutionContext context )
    {
        if( source == null )
        {
            throw new ArgumentNullException( nameof( source ) );
        }

        return source.DocumentModifiedWhen != DateTime.MinValue
            ? source.DocumentModifiedWhen
            : publishDateResolver.Resolve( source, destination, destMember, context );
    }
}
