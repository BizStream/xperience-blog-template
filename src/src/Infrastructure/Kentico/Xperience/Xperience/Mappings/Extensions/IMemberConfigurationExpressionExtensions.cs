using System;
using System.Linq.Expressions;
using AutoMapper;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Extensions
{

    public static class IMemberConfigurationExpressionExtensions
    {

        public static void ConvertMediaPathToUri<TSource, TDestination>( this IMemberConfigurationExpression<TSource, TDestination, Uri> expression, Expression<Func<TSource, string>> sourceMember )
        {
            if( expression == null )
            {
                throw new ArgumentNullException( nameof( expression ) );
            }

            expression.ConvertUsing<MediaUriConverter, string>( sourceMember );
        }

    }

}
