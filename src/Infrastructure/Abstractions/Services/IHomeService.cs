using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Infrastructure.Abstractions.Services
{

    /// <summary> Describes a service that can retrieve a <see cref="Home"/>. </summary>
    public interface IHomeService
    {

        Home GetHome( );

    }

}