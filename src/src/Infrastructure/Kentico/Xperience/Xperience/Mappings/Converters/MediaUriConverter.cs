using System;
using AutoMapper;
using CMS.SiteProvider;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters
{

    public class MediaUriConverter : IValueConverter<string, Uri>
    {

        public Uri Convert( string source, ResolutionContext context )
        {
            if( string.IsNullOrWhiteSpace( source ) )
            {
                return null;
            }

            if( source.StartsWith( "~/getmedia" ) )
            {
                return new Uri( SiteContext.CurrentSite.SitePresentationURL + source.TrimStart( '~' ), UriKind.Absolute );
            }

            return new Uri( source, UriKind.Absolute );
        }

    }

}
