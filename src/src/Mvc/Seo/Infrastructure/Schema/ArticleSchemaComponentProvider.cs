using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Infrastructure.Xperience.Abstractions;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Schema.NET;

namespace BlogTemplate.Mvc.Seo.Infrastructure.Schema;

public class ArticleSchemaComponentProvider : XperienceSchemaComponentProvider<ArticleNode>
{
    protected override Task<IThing?> InvokeAsync( ViewComponentContext context, IPageDataContext<ArticleNode> pageContext )
    {
        ArgumentNullException.ThrowIfNull( context );
        ArgumentNullException.ThrowIfNull( pageContext );

        var serviceProvider = context.ViewContext.HttpContext.RequestServices;
        var request = context.ViewContext.HttpContext.Request;

        var post = new BlogPosting
        {
            Description = pageContext.Metadata.Description,
            Name = pageContext.Page.DocumentName,
            Keywords = pageContext.Metadata.GetNormalizedKeywords().ToArray(),
            IsPartOf = new Uri( $"{request.Scheme}://{request.Host}/", UriKind.Absolute ),
            Url = new Uri(
                serviceProvider.GetRequiredService<IUrlHelper>()
                    .Kentico()
                    .PageCanonicalUrl(),
                UriKind.Absolute
            )
        };

        return Task.FromResult<IThing?>( post );
    }
}
