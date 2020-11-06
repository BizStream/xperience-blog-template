using System;
using AutoMapper;
using BlogTemplate.Infrastructure.Abstractions.Services;
using BlogTemplate.Mvc.App.Models.Components;
using Microsoft.AspNetCore.Mvc;

namespace BlogTemplate.Mvc.App.ViewComponents
{

    public class AuthorViewComponent : ViewComponent
    {
        #region Fields
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        #endregion

        public AuthorViewComponent( IAuthorService authorService, IMapper mapper )
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke( Guid authorGuid )
        {
            var author = authorService.GetAuthor( authorGuid );
            if( author == null )
            {
                return Content( string.Empty );
            }

            var viewModel = mapper.Map<AuthorComponentViewModel>( author );
            return View( viewModel );
        }

    }

}
