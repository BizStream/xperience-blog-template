using System.Linq.Expressions;
using AutoMapper;
using BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Resolvers;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Extensions;

public static class IMemberConfigurationExpressionExtensions
{
    public static void ResolveMediaUrl<TSource, TDest>( this IMemberConfigurationExpression<TSource, TDest, string> expression, Expression<Func<TSource, string>> sourceMember )
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom<MediaUrlResolver<TSource, TDest>, string>( sourceMember );
    }

    public static void ResolveMediaUrl<TSource, TDest>( this IMemberConfigurationExpression<TSource, TDest, Uri> expression, Expression<Func<TSource, string>> sourceMember )
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom<MediaUrlResolver<TSource, TDest>, string>( sourceMember );
    }

    public static void ResolvePageUrl<TNode, TDest>( this IMemberConfigurationExpression<TNode, TDest, PageUrl> expression )
        where TNode : TreeNode
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom<PageUrlResolver<TNode, TDest>>();
    }

    public static void ResolvePageUrl<TNode, TDest>( this IMemberConfigurationExpression<TNode, TDest, string> expression )
        where TNode : TreeNode
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom<PageUrlResolver<TNode, TDest>>();
    }

    public static void ResolvePageUrl<TNode, TDest>( this IMemberConfigurationExpression<TNode, TDest, Uri> expression )
        where TNode : TreeNode
    {
        if( expression is null )
        {
            throw new ArgumentNullException( nameof( expression ) );
        }

        expression.MapFrom<PageUrlResolver<TNode, TDest>>();
    }
}
