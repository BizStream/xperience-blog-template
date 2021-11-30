using System.Reflection;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.App.Extensions
{
    public static partial class OptionsBuilderExtensions
    {
        public static OptionsBuilder<StaticFileOptions> ConfigureEmbeddedProvider( this OptionsBuilder<StaticFileOptions> options, Assembly assembly, string root )
        {
            if( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }

            return options.PostConfigure<IWebHostEnvironment>(
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
}
