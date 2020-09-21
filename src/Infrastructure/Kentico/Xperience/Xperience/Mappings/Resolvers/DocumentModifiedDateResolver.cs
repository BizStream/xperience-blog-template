using System;
using AutoMapper;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Resolvers
{

    /// <summary> Resolves the last modified date for a document. </summary>
    /// <remarks> This implementation resolves the value using <see cref="DocumentPublishDateResolver"/>, if <see cref="TreeNode.DocumentModifiedWhen"/> does not have a value. </remarks>
    public class DocumentModifiedDateResolver : IValueResolver<TreeNode, object, DateTime>
    {
        #region Fields
        private readonly DocumentPublishDateResolver publishDateResolver;
        #endregion

        public DocumentModifiedDateResolver( DocumentPublishDateResolver publishDateResolver )
            => this.publishDateResolver = publishDateResolver;

        public DateTime Resolve( TreeNode source, object destination, DateTime destMember, ResolutionContext context )
        {
            if( source == null )
            {
                throw new ArgumentNullException( nameof( source ) );
            }

            var modified = source.GetDateTimeValue( SyntheticDocumentFields.ModifiedAtDate, source.DocumentModifiedWhen );
            return modified != DateTime.MinValue
                ? modified
                : publishDateResolver.Resolve( source, destination, destMember, context );
        }

    }

}
