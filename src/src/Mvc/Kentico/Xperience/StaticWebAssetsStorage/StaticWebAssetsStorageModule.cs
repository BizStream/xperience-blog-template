using System.IO;
using BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage.IO;
using CMS.Core;
using CMS.DataEngine;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using CMSIO = CMS.IO;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage
{

    public class StaticWebAssetsStorageModule : Module
    {

        public StaticWebAssetsStorageModule( )
            : base( nameof( StaticWebAssetsStorageModule ), false )
        {
        }

        protected override void OnInit( )
        {
            base.OnInit();

            var environment = Service.Resolve<IWebHostEnvironment>();
            if( !environment.IsDevelopment() )
            {
                // storage provider is only required during development
                return;
            }

            var configuration = Service.Resolve<IConfiguration>();
            var paths = StaticWebAssetsHelper.GetRCLPaths( environment, configuration );
            foreach( var (basePath, path) in paths )
            {
                if( basePath == Strings.KenticoMvcRCLBasePath )
                {
                    // ignore Kentico's RCL; it's supported by Kentico's logic
                    continue;
                }

                // `BuilderAssetsProvider` prepends the `IWebHostEnvironment.WebRootPath` when resolving configured bundles/scripts/styles
                var rclPath = Path.Combine(
                    environment.WebRootPath,
                    basePath.Replace( "/", "\\" )
                );

                CMSIO.StorageHelper.MapStoragePath( rclPath, new StaticWebAssetsStorageProvider( rclPath, path ) );
            }
        }

    }

}
