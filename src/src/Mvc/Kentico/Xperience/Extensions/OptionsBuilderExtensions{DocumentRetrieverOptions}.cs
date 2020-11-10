using System;
using BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers;
using CMS.Base;
using Kentico.Content.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BlogTemplate.Mvc.Kentico.Xperience.Extensions
{

    public static partial class OptionsBuilderExtensions
    {

        public static OptionsBuilder<DocumentRetrieverOptions> ConfigureCurrentSite( this OptionsBuilder<DocumentRetrieverOptions> builder )
        {
            if( builder == null )
            {
                throw new ArgumentNullException( nameof( builder ) );
            }

            return builder.PostConfigure<ISiteService>(
                ( options, siteService ) => options.SiteID = siteService.CurrentSite.SiteID
            );
        }

        public static OptionsBuilder<DocumentRetrieverOptions> ConfigurePreviewMode( this OptionsBuilder<DocumentRetrieverOptions> builder )
        {
            if( builder == null )
            {
                throw new ArgumentNullException( nameof( builder ) );
            }

            return builder.PostConfigure<IHttpContextAccessor>(
                ( options, httpAccessor ) =>
                {
                    var httpContext = httpAccessor.HttpContext;
                    var isPreview = httpContext?.Kentico()
                        ?.Preview()
                        ?.Enabled;

                    options.Version = isPreview.HasValue && isPreview.Value
                        ? DocumentVersion.Latest
                        : DocumentVersion.Published;
                }
            );
        }

    }

}
