using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Mvc.App.Models.Components;

namespace BlogTemplate.Mvc.App.Mappings
{
    public class AuthorMappingProfile : Profile
    {
        public AuthorMappingProfile( ) => CreateMap<Author, AuthorComponentViewModel>();
    }
}
