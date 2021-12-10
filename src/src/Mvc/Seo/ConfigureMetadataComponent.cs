using BizStream.AspNetCore.Components.Metadata.Abstractions;
using BlogTemplate.Mvc.Seo.Infrastructure;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Seo;

public class ConfigureMetadataComponent : IConfigureOptions<MetadataComponentOptions>
{
    public void Configure( MetadataComponentOptions options )
        => options.Providers.Insert( 0, new XperienceMetadataComponentProvider() );
}
