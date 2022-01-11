using BizStream.Kentico.Xperience.AspNetCore.StaticWebAssetsStorage;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.PageBuilder;

public class ConfigurePageBuilderBundles : IConfigureOptions<PageBuilderBundlesOptions>
{
    public void Configure( PageBuilderBundlesOptions options ) => options.AddRCLBundle( typeof( ConfigurePageBuilderBundles ).Assembly );
}
