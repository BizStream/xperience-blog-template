using AutoMapper;
using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using BlogTemplate.Mvc.Errors.Abstractions;
using BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Extensions;

namespace BlogTemplate.Mvc.Errors.Mappings;

public class ErrorMappingProfile : Profile
{
    public ErrorMappingProfile( )
    {
        CreateMap<StatusCodeNode, ErrorViewModel>()
            .ForMember( viewModel => viewModel.Content, opt => opt.ConvertToHtmlContent( node => node.Content ) )
            .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( node => node.DocumentName ) );
    }
}
