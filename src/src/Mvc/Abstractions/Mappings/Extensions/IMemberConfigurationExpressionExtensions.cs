using System.Linq.Expressions;
using AutoMapper;
using BlogTemplate.Mvc.Abstractions.Mappings.Converters;
using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Abstractions.Mappings.Extensions
{
    public static class IMemberConfigurationExpressionExtensions
    {
        public static void ConvertToHtmlContent<TSource, TDestination>(
            this IMemberConfigurationExpression<TSource, TDestination, IHtmlContent> expression,
            Expression<Func<TSource, string>> sourceMember
        )
        {
            ThrowIfExpressionIsNull( expression );
            expression.ConvertUsing<HtmlContentConverter, string>( sourceMember );
        }

        private static void ThrowIfExpressionIsNull<TSource, TDestination, TDestMember>( IMemberConfigurationExpression<TSource, TDestination, TDestMember> expression )
        {
            if( expression == null )
            {
                throw new ArgumentNullException( nameof( expression ) );
            }
        }
    }
}
