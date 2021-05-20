using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.Abstractions.Models;

namespace BlogTemplate.Mvc.App.Mappings.Resolvers
{

    public class OpenGraphDataResolver<TEntity> : IValueResolver<TEntity, BaseViewModel, OpenGraphData>
    {
        #region Fields
        private readonly IMetaDataService<TEntity> metaDataService;
        #endregion

        public OpenGraphDataResolver( IMetaDataService<TEntity> metaDataService )
            => this.metaDataService = metaDataService;

        public OpenGraphData Resolve( TEntity entity, BaseViewModel viewModel, OpenGraphData openGraphData, ResolutionContext context )
            => metaDataService.GetOpenGraphDataAsync( entity )
                .GetAwaiter()
                .GetResult();

    }

}
