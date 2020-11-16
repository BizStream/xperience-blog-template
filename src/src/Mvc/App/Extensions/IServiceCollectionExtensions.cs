using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace BlogTemplate.Mvc.App.Extensions
{

    public static class IServiceCollectionExtensions
    {
        #region Fields
        private static readonly Assembly KenticoMappingsAssembly = typeof( Infrastructure.Kentico.Xperience.Mappings.ArticleMappingProfile ).Assembly;
        private static readonly Assembly KenticoMvcMappingsAssembly = typeof( Mvc.Kentico.Xperience.Mappings.AboutMappingProfile ).Assembly;
        private static readonly Assembly MvcMappingsAssembly = typeof( Mvc.App.Mappings.HomeMappingProfile ).Assembly;
        #endregion

        public static IServiceCollection AddBlogMappings( this IServiceCollection services )
            => services.AddAutoMapper(
                KenticoMappingsAssembly,
                KenticoMvcMappingsAssembly,
                MvcMappingsAssembly
            );

    }

}
