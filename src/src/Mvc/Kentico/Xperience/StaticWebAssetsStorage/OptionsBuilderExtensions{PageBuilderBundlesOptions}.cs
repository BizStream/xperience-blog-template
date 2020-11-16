using System;
using System.Reflection;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage
{

    public static partial class OptionsBuilderExtensions
    {

        /// <summary> Configures Page Builder to discover static files bundled via an RCL. </summary>
        /// <param name="assembly"> The Assembly representing the RCL (the <see cref="AssemblyName.Name"/> is used to generating the RCL file path). </param>
        /// <param name="rootPath"> A custom path within the RCL in which PageBuilder assets are located. </param>
        public static OptionsBuilder<PageBuilderBundlesOptions> ConfigureRCLBundle(
            this OptionsBuilder<PageBuilderBundlesOptions> options,
            Assembly assembly,
            string rootPath = "PageBuilder"
        )
        {
            if( options == null )
            {
                throw new ArgumentNullException( nameof( options ) );
            }

            return options.Configure(
                options =>
                {
                    var basePath = $"_content\\{assembly.GetName().Name}\\{rootPath}";
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
