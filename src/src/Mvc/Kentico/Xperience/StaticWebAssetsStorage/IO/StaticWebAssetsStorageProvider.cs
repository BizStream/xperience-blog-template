using System;
using CMS.IO;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage.IO
{

    public class StaticWebAssetsStorageProvider : AbstractStorageProvider
    {
        #region Fields
        private readonly string rclPath;
        private readonly string rootPath;
        #endregion

        public StaticWebAssetsStorageProvider( string rclPath, string rootPath )
        {
            this.rclPath = rclPath;
            this.rootPath = rootPath;
        }

        public override DirectoryInfo GetDirectoryInfo( string path )
            => throw new NotImplementedException();

        public override FileInfo GetFileInfo( string fileName )
            => throw new NotImplementedException();

        public override FileStream GetFileStream( string path, FileMode mode )
            => throw new NotImplementedException();

        public override FileStream GetFileStream( string path, FileMode mode, FileAccess access )
            => throw new NotImplementedException();

        public override FileStream GetFileStream( string path, FileMode mode, FileAccess access, FileShare share )
            => throw new NotImplementedException();

        public override FileStream GetFileStream( string path, FileMode mode, FileAccess access, FileShare share, int bufferSize )
            => throw new NotImplementedException();

        protected override AbstractDirectory CreateDirectoryProviderObject( )
            => new StaticWebAssetsDirectory( rclPath, rootPath );

        protected override AbstractFile CreateFileProviderObject( )
            => new StaticWebAssetsFile( rclPath, rootPath );
    }

}
