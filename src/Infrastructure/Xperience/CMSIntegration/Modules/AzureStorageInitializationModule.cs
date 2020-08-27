using System.Linq;
using CMS.DataEngine;
using CMS.IO;
using CMS.SiteProvider;

namespace BlogTemplate.Infrastructure.Xperience.CMSIntegration.Modules
{

    public class AzureStorageInitializationModule : Module
    {

        public AzureStorageInitializationModule( )
            : base( nameof( AzureStorageInitializationModule ) )
        {
        }

        private static StorageProvider CreateAzureProvider( )
        {
            // Creates a new StorageProvider instance for Azure
            var mediaProvider = StorageProvider.CreateAzureStorageProvider();

            // Specifies the target container name in Azure Storage
            mediaProvider.CustomRootPath = "cms";

            // Makes the container publicly accessible
            mediaProvider.PublicExternalFolderObject = true;

            return mediaProvider;
        }

        private static void MapMediaLibraries( StorageProvider provider )
        {
            // Map all site media libraries to azure
            var siteNames = SiteInfo.Provider.Get()
                .Select( site => site.SiteName )
                .ToList();

            foreach( var siteName in siteNames )
            {
                StorageHelper.MapStoragePath( $"~/{siteName}/media", provider );
            }
        }

        // Contains initialization code that is executed when the application starts
        protected override void OnInit( )
        {
            base.OnInit();

            var provider = CreateAzureProvider();
            MapMediaLibraries( provider );
        }

    }

}
