using System;
using AutoMapper;

namespace BlogTemplate.Infrastructure.Xperience.Mappings.Converters
{

    public class MediaUriConverter : IValueConverter<string, Uri>
    {

        public Uri Convert( string mediaUrl, ResolutionContext context )
        {
            if( string.IsNullOrWhiteSpace( mediaUrl ) )
            {
                return null;
            }

            return new Uri( mediaUrl.TrimStart( '~' ), UriKind.Relative );
        }

    }

}
