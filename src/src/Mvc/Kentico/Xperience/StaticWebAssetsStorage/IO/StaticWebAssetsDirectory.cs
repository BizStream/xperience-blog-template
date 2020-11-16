using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using CMS.IO;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage.IO
{

    public class StaticWebAssetsDirectory : AbstractDirectory
    {
        #region Fields
        private readonly AbstractDirectory directory;
        private readonly string rclPath;
        private readonly string rootPath;
        #endregion

        public StaticWebAssetsDirectory( string rclPath, string rootPath )
        {
            this.directory = new CMS.FileSystemStorage.Directory();
            this.rclPath = rclPath;
            this.rootPath = rootPath;
        }

        public override DirectoryInfo CreateDirectory( string path )
            => throw new System.NotImplementedException();

        public override void Delete( string path, bool recursive )
            => throw new System.NotImplementedException();

        public override void Delete( string path )
            => throw new System.NotImplementedException();

        public override void DeleteDirectoryStructure( string path )
            => throw new System.NotImplementedException();

        public override IEnumerable<string> EnumerateDirectories( string path, string searchPattern, SearchOption searchOption )
        {
            if( TryGetRCLPath( path, out string rclPath ) )
            {
                return directory.EnumerateDirectories( rclPath, searchPattern, searchOption )
                    .Select( file => this.rclPath + '\\' + file.Substring( rootPath.Length ) );
            }

            return directory.EnumerateDirectories( path, searchPattern, searchOption );
        }

        public override IEnumerable<string> EnumerateFiles( string path, string searchPattern )
        {
            if( TryGetRCLPath( path, out string rclPath ) )
            {
                return directory.EnumerateFiles( rclPath, searchPattern )
                    .Select( file => this.rclPath + '\\' + file.Substring( rootPath.Length ) );
            }

            return directory.EnumerateFiles( path, searchPattern );
        }

        public override bool Exists( string path )
        {
            if( TryGetRCLPath( path, out string rclPath ) )
            {
                return directory.Exists( rclPath );
            }

            return directory.Exists( path );
        }

        public override DirectorySecurity GetAccessControl( string path )
            => throw new System.NotImplementedException();

        public override string[] GetDirectories( string path, string searchPattern, SearchOption searchOption )
            => throw new System.NotImplementedException();

        public override string[] GetFiles( string path, string searchPattern )
            => throw new System.NotImplementedException();

        public override void Move( string sourceDirName, string destDirName )
            => throw new System.NotImplementedException();

        public override void PrepareFilesForImport( string path )
            => throw new System.NotImplementedException();

        private bool TryGetRCLPath( string path, out string rclPath )
        {
            if( path?.StartsWith( this.rclPath, StringComparison.InvariantCultureIgnoreCase ) == true )
            {
                rclPath = rootPath.TrimEnd( '\\', '/' ) + path.Substring( this.rclPath.Length );
                return true;
            }

            rclPath = null;
            return false;
        }

    }

}
