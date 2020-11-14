using System;
using System.Linq;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage
{

    public static class IServiceCollectionExtensions
    {

        public static OptionsBuilder<PageBuilderBundlesOptions> ConfigureRCLBundles( this OptionsBuilder<PageBuilderBundlesOptions> options )
        {
            if( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }

            return options.Configure<IWebHostEnvironment, IConfiguration>(
                ( options, environment, configuration ) =>
                {
                    var paths = StaticWebAssetsHelper.GetRCLPaths( environment, configuration );
                    if( paths?.Any() != true )
                    {
                        return;
                    }

                    // add RCL paths at Kentico's conventional location
                    foreach( var (basePath, path) in paths )
                    {
                        if( basePath == Strings.KenticoMvcRCLBasePath )
                        {
                            // ignore Kentico's RCL; it's supported by Kentico's logic
                            continue;
                        }

                        var adminPath = $"{path}/PageBuilder/Admin".Replace( '/', '\\' );
                        var publicPath = $"{path}/PageBuilder/Public".Replace( '/', '\\' );

                        options.PageBuilderAdminScripts
                            .Contents
                            .IncludedWebRootDirectories
                            .Add( adminPath );

                        options.PageBuilderAdminStyles
                            .Contents
                            .IncludedWebRootDirectories
                            .Add( adminPath );

                        options.PageBuilderPublicScripts
                            .Contents
                            .IncludedWebRootDirectories
                            .Add( publicPath );

                        options.PageBuilderPublicStyles
                            .Contents
                            .IncludedWebRootDirectories
                            .Add( publicPath );
                    }
                }
            );
        }

    }

}
