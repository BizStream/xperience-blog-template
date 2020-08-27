using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Infrastructure.Abstractions.Services
{

    /// <summary> Describes a service that can retrieve meta-data for the specified <typeparamref name="TEntity"/>. </summary>
    /// <typeparam name="TEntity"> The type of entity to retrieve meta-data for. </typeparam>
    public interface IMetaDataService<TEntity>
    {

        /// <summary> Retrieve basic meta-data for the given entity. </summary>
        /// <param name="entity"> The entity to retrieve basic meta-data for. </param>
        MetaData GetMetaData( TEntity entity );

        /// <summary> Retrieve OpenGraph meta-data for the given entity. </summary>
        /// <param name="entity"> The entity to retrieve OpenGraph meta-data for. </param>
        OpenGraphData GetOpenGraphData( TEntity entity );

    }

}
