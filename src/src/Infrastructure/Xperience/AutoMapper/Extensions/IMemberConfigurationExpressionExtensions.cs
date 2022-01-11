using AutoMapper;
using BlogTemplate.Infrastructure.Xperience.AutoMapper.Resolvers;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.AutoMapper.Extensions;

public static class IMemberConfigurationExpressionExtensions
{
    public static void ResolveModifiedDate<TNode, TDest>( this IMemberConfigurationExpression<TNode, TDest, DateTime> expression )
        where TNode : TreeNode
        where TDest : class
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom(
            ( source, destination, destMember, context ) => context.Options.CreateInstance<DocumentModifiedDateResolver>()
                .Resolve( source, destination, destMember, context )
        );
    }

    public static void ResolvePublishedDate<TNode, TDest>( this IMemberConfigurationExpression<TNode, TDest, DateTime> expression )
        where TNode : TreeNode
        where TDest : class
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom(
            ( source, destination, destMember, context ) => context.Options.CreateInstance<DocumentPublishedDateResolver>()
                .Resolve( source, destination, destMember, context )
        );
    }
}
