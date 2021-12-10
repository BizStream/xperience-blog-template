using BizStream.AspNetCore.Components.OpenGraph;
using BizStream.AspNetCore.Components.OpenGraph.Abstractions;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Seo;

public class ConfigureOpenGraphComponent : IConfigureOptions<OpenGraphComponentOptions>
{
    public void Configure( OpenGraphComponentOptions options )
        => options.Providers.Add( new MetadataOpenGraphComponentProvider() );
}
