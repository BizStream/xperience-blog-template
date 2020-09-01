using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.App.Extensions
{

    public static class IServiceCollectionExtensions
    {
        #region Fields
        private static readonly Assembly KenticoMappingsAssembly = typeof( Infrastructure.Kentico.Xperience.Mappings.ArticleMappingProfile ).Assembly;
        private static readonly Assembly MvcMappingsAssembly = typeof( Mvc.App.Mappings.HomeMappingProfile ).Assembly;
        #endregion

        public static IServiceCollection AddBlogMappings( this IServiceCollection services )
            => services.AddAutoMapper( KenticoMappingsAssembly, MvcMappingsAssembly );

        public static OptionsBuilder<StaticFileOptions> AddEmbeddedStaticFileProvider( this IServiceCollection services, Assembly assembly, string root )
            => services.AddOptions<StaticFileOptions>()
                .PostConfigure<IWebHostEnvironment>(
                    ( options, environment ) =>
                    {
                        options.ContentTypeProvider ??= new FileExtensionContentTypeProvider();
                        options.FileProvider ??= environment.WebRootFileProvider;

                        var embeddedFileProvider = new ManifestEmbeddedFileProvider( assembly, root );
                        options.FileProvider = new CompositeFileProvider( options.FileProvider, embeddedFileProvider );
                    }
                );

    }

}
