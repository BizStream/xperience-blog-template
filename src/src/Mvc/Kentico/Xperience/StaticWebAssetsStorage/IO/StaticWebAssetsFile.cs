using System;
using System.Security.AccessControl;
using System.Text;
using CMS.IO;

namespace BlogTemplate.Mvc.Kentico.Xperience.StaticWebAssetsStorage.IO
{

    public class StaticWebAssetsFile : AbstractFile
    {
        #region Fields
        private readonly AbstractFile file;
        private readonly string rclPath;
        private readonly string rootPath;
        #endregion

        public StaticWebAssetsFile( string rclPath, string rootPath )
        {
            this.file = new CMS.FileSystemStorage.File();
            this.rclPath = rclPath;
            this.rootPath = rootPath;
        }

        public override void AppendAllText( string path, string contents )
            => throw new NotImplementedException();

        public override void AppendAllText( string path, string contents, Encoding encoding )
            => throw new NotImplementedException();

        public override void Copy( string sourceFileName, string destFileName )
            => throw new NotImplementedException();

        public override void Copy( string sourceFileName, string destFileName, bool overwrite )
            => throw new NotImplementedException();

        public override FileStream Create( string path )
            => throw new NotImplementedException();

        public override StreamWriter CreateText( string path )
            => throw new NotImplementedException();

        public override void Delete( string path )
            => throw new NotImplementedException();

        public override bool Exists( string path )
        {
            if( TryGetRCLPath( path, out string rclPath ) )
            {
                return file.Exists( rclPath );
            }

            return file.Exists( path );
        }

        public override FileSecurity GetAccessControl( string path )
            => throw new NotImplementedException();

        public override string GetFileUrl( string path, string siteName )
            => throw new NotImplementedException();

        public override DateTime GetLastWriteTime( string path )
            => throw new NotImplementedException();

        public override void Move( string sourceFileName, string destFileName )
            => throw new NotImplementedException();

        public override FileStream Open( string path, FileMode mode, FileAccess access )
            => throw new NotImplementedException();

        public override FileStream OpenRead( string path )
            => throw new NotImplementedException();

        public override StreamReader OpenText( string path )
            => throw new NotImplementedException();

        public override byte[] ReadAllBytes( string path )
            => throw new NotImplementedException();

        public override string ReadAllText( string path )
            => throw new NotImplementedException();

        public override string ReadAllText( string path, Encoding encoding )
            => throw new NotImplementedException();

        public override void SetAttributes( string path, FileAttributes fileAttributes )
            => throw new NotImplementedException();

        public override void SetLastWriteTime( string path, DateTime lastWriteTime )
            => throw new NotImplementedException();

        public override void SetLastWriteTimeUtc( string path, DateTime lastWriteTimeUtc )
            => throw new NotImplementedException();

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

        public override void WriteAllBytes( string path, byte[] bytes )
            => throw new NotImplementedException();

        public override void WriteAllText( string path, string contents )
            => throw new NotImplementedException();

        public override void WriteAllText( string path, string contents, Encoding encoding )
            => throw new NotImplementedException();

    }

}
