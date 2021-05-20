using AutoMapper;
using Microsoft.AspNetCore.Html;

namespace BlogTemplate.Mvc.Abstractions.Mappings.Converters
{

    public class HtmlContentConverter : ITypeConverter<string, IHtmlContent>, IValueConverter<string, IHtmlContent>
    {

        public IHtmlContent Convert( string source, ResolutionContext context )
            => ToHtml( source );

        public IHtmlContent Convert( string source, IHtmlContent destination, ResolutionContext context )
            => ToHtml( source );

        private static IHtmlContent ToHtml( string source )
            => new HtmlContentBuilder()
                .SetHtmlContent( source );

    }

}
