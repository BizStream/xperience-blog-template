using System;
using System.Reflection;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage
{

    public static partial class OptionsBuilderExtensions
    {

        public static OptionsBuilder<PageBuilderBundlesOptions> ConfigureRCLBundle( this OptionsBuilder<PageBuilderBundlesOptions> options, Assembly assembly )
        {
            if( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }

            return options.Configure(
                options =>
                {
                    var basePath = $"_content\\{assembly.GetName().Name}\\PageBuilder";
                    var adminPath = $"{basePath}\\Admin";
                    var publicPath = $"{basePath}\\Public";

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
            );
        }

    }

}
