using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Infrastructure.Abstractions.Services
{
    /// <summary> Describes a service that can retrieve <see cref="Author"/>s. </summary>
    public interface IAuthorService
    {
        /// <summary> Retrieve the <see cref="Author"/> with the given <see cref="Author.AuthorGuid"/>. </summary>
        /// <param name="authorGuid"> The <see cref="Author.AuthorGuid"/> of the <see cref="Author"/> to retrieve. </param>
        Task<Author> GetAuthorAsync( Guid authorGuid );

        /// <summary> Retrieve all <see cref="Author"/>s. </summary>
        Task<IEnumerable<Author>> GetAuthorsAsync( );
    }
}
