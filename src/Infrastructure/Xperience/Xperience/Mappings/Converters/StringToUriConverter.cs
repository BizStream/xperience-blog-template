using System;
using AutoMapper;

namespace BlogTemplate.Infrastructure.Xperience.Mappings.Converters
{

    public class StringToUriConverter : IValueConverter<string, Uri>
    {

        public Uri Convert( string url, ResolutionContext context )
        {
            if( string.IsNullOrWhiteSpace( url ) )
            {
                return default;
            }

            return new Uri( url, UriKind.Absolute );
        }

    }

}
