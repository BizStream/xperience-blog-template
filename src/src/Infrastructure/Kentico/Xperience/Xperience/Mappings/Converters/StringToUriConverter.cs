using System;
using AutoMapper;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters
{

    public class StringToUriConverter : IValueConverter<string, Uri>
    {

        public Uri Convert( string source, ResolutionContext context )
        {
            if( string.IsNullOrWhiteSpace( source ) )
            {
                return default;
            }

            return new Uri( source, UriKind.Absolute );
        }

    }

}
