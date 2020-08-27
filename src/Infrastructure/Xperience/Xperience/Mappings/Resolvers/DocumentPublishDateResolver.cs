using System;
using AutoMapper;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.Mappings.Resolvers
{

    /// <summary> Resolves the publish date for a document. </summary>
    /// <remarks> This implementation resolves the value of <see cref="TreeNode.DocumentCreatedWhen"/>, if <see cref="TreeNode.DocumentPublishFrom"/> does not have a value. </remarks>
    public class DocumentPublishDateResolver : IValueResolver<TreeNode, object, DateTime>
    {

        public DateTime Resolve( TreeNode source, object destination, DateTime destMember, ResolutionContext context )
        {
            var published = source.GetDateTimeValue( "PublishedAtDate", source.DocumentPublishFrom );
            return published != DateTime.MinValue
                ? published
                : source.DocumentCreatedWhen;
        }

    }

}
