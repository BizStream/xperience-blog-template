using System;
using AutoMapper;
using BlogTemplate.Mvc.Abstractions.Models;
using BlogTemplate.Mvc.App.Mappings.Resolvers;

namespace BlogTemplate.Mvc.App.Mappings.Extensions
{

    public static partial class IMappingExpressionExtensions
    {

        public static IMappingExpression<TEntity, TViewModel> IncludeMetaData<TEntity, TViewModel>( this IMappingExpression<TEntity, TViewModel> expression )
            where TEntity : class
            where TViewModel : BaseViewModel
        {
            ThrowIfExpressionIsNull( expression );
            return expression.ForMember( viewModel => viewModel.Meta, opt => opt.MapFrom<MetaDataResolver<TEntity>>() );
        }

        public static IMappingExpression<TEntity, TViewModel> IncludeOpenGraphData<TEntity, TViewModel>( this IMappingExpression<TEntity, TViewModel> expression )
            where TEntity : class
            where TViewModel : BaseViewModel
        {
            ThrowIfExpressionIsNull( expression );
            return expression.ForMember( viewModel => viewModel.OpenGraph, opt => opt.MapFrom<OpenGraphDataResolver<TEntity>>() );
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
