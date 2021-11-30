using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.Abstractions.Models;

namespace BlogTemplate.Mvc.App.Mappings.Resolvers
{
    public class MetaDataResolver<TEntity> : IValueResolver<TEntity, BaseViewModel, MetaData>
    {
        #region Fields
        private readonly IMetaDataService<TEntity> metaDataService;
        #endregion

        public MetaDataResolver( IMetaDataService<TEntity> metaDataService )
            => this.metaDataService = metaDataService;

        public MetaData Resolve( TEntity entity, BaseViewModel viewModel, MetaData meta, ResolutionContext context )
            => metaDataService.GetMetaDataAsync( entity )
                .GetAwaiter()
                .GetResult();
    }
}
