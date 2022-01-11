using System.Linq.Expressions;
using AutoMapper;
using BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Converters;
using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Extensions;

public static class IMemberConfigurationExpressionExtensions
{
    public static void ConvertToHtmlContent<TSource, TDestination>( this IMemberConfigurationExpression<TSource, TDestination, IHtmlContent> expression, Expression<Func<TSource, string>> sourceMember )
    {
        ArgumentNullException.ThrowIfNull( expression );
        ArgumentNullException.ThrowIfNull( sourceMember );

        expression.ConvertUsing<HtmlContentConverter, string>( sourceMember );
    }
}
