using System;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using Microsoft.AspNetCore.Routing;

namespace BlogTemplate.Mvc.App.Mappings.Resolvers
{

    public class ArticleUrlResolver : IValueResolver<Article, object, Uri>
    {
        #region Fields
        private readonly LinkGenerator linkGenerator;
        #endregion

        public ArticleUrlResolver( LinkGenerator linkGenerator )
            => this.linkGenerator = linkGenerator;

        public Uri Resolve( Article article, object destination, Uri destMember, ResolutionContext context )
        {
            var url = linkGenerator.GetPathByAction( "Article", "Articles", new { article.Slug } );
            return new Uri( url, UriKind.Relative );
        }

    }

}
