using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Resolvers;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Extensions
{
    public static partial class IMappingExpressionExtensions
    {
        public static IMappingExpression<TNode, TAuthored> IncludeAuthored<TNode, TAuthored>( this IMappingExpression<TNode, TAuthored> expression )
            where TNode : TreeNode, new()
            where TAuthored : class, IAuthored
        {
            ThrowIfExpressionIsNull( expression );
            return expression.ForMember( authored => authored.AuthorGuid, opt => opt.MapFrom( node => node.GetGuidValue( "AuthorGuid", Guid.Empty ) ) )
                .ForMember( authored => authored.LastAuthoredAt, opt => opt.MapFrom<DocumentModifiedDateResolver>() );
        }

        private static void ThrowIfExpressionIsNull<TSource, TDestination>( IMappingExpression<TSource, TDestination> expression )
        {
            if( expression == null )
            {
                throw new ArgumentNullException( nameof( expression ) );
            }
        }
    }
}
