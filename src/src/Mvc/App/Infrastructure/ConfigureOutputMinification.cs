using Microsoft.Extensions.Options;
using WebMarkupMin.AspNetCore6;
using WebMarkupMin.NUglify;

namespace BlogTemplate.Mvc.App.Infrastructure;

public class ConfigureOutputMinification : IConfigureOptions<WebMarkupMinOptions>, IConfigureOptions<HtmlMinificationOptions>
{
    public void Configure( WebMarkupMinOptions options )
    {
        options.DisableCompression = false;
        options.DisablePoweredByHttpHeaders = true;

        // NOTE: change this setting if you need inspect HTML output while deving
        options.AllowMinificationInDevelopmentEnvironment = false;
    }

    public void Configure( HtmlMinificationOptions options )
    {
        options.CssMinifierFactory = new NUglifyCssMinifierFactory();
        options.JsMinifierFactory = new NUglifyJsMinifierFactory();
    }
}