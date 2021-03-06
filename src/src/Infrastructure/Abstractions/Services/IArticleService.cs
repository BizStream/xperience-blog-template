﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlogTemplate.Core.Abstractions.Models;

namespace BlogTemplate.Infrastructure.Abstractions.Services
{

    /// <summary> Describes a service that can retrieve <see cref="Article"/>s. </summary>
    public interface IArticleService
    {

        /// <summary> Retrieve an <see cref="Article"/>, via its <see cref="Article.Slug"/>. </summary>
        /// <param name="slug"> <see cref="Article.Slug"/>. </param>
        Task<Article> GetArticleAsync( string slug );

        /// <summary> Retrieves the 5 most recently published <see cref="Article"/>s. </summary>
        Task<IEnumerable<Article>> GetRecentArticlesAsync();

    }

}
