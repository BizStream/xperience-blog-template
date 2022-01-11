using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Schema.NET;

namespace BlogTemplate.Mvc.Seo.Infrastructure.Schema;

public class HomeSchemaComponentProvider : XperienceSchemaComponentProvider<HomeNode>
{
    protected override Task<IThing?> InvokeAsync( ViewComponentContext context, IPageDataContext<HomeNode> pageContext )
    {
        ArgumentNullException.ThrowIfNull( context );
        ArgumentNullException.ThrowIfNull( pageContext );

        var serviceProvider = context.ViewContext.HttpContext.RequestServices;
        var blog = new Blog
        {
            Description = pageContext.Metadata.Description,
            Name = pageContext.Page.DocumentName,
            Keywords = pageContext.Metadata.GetNormalizedKeywords().ToArray(),
            Url = new Uri(
                serviceProvider.GetRequiredService<IUrlHelper>()
                    .Kentico()
                    .PageCanonicalUrl(),
                UriKind.Absolute
            )
        };

        return Task.FromResult<IThing?>( blog );
    }
}
