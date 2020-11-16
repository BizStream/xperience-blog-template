using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Infrastructure.Abstractions.Services
{

    /// <summary> Describes a service that can retrieve a <see cref="Blog"/>. </summary>
    public interface IBlogService
    {

        Blog GetBlog( );

    }

}
