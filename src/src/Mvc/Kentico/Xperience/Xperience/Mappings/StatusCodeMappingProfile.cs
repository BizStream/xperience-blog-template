using AutoMapper;
using BizStream.Kentico.Xperience.AspNetCore.StatusCodePages.Models;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Mvc.Abstractions.Mappings.Extensions;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Kentico.Xperience.Mappings
{
    public class StatusCodeMappingProfile : Profile
    {
        public StatusCodeMappingProfile( )
        {
            CreateMap<StatusCodeNode, StatusCodeViewModel>()
                .ForMember( viewModel => viewModel.Content, opt => opt.ConvertToHtmlContent( node => node.Content ) )
                .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( node => node.DocumentName ) )
                .ForMember( viewModel => viewModel.Meta, opt => opt.MapFrom( node => node ) )
                .ForMember( viewModel => viewModel.OpenGraph, opt => opt.MapFrom( node => node ) );

            CreateMap<StatusCodeNode, MetaData>()
               .IncludeBase<TreeNode, MetaData>();

            CreateMap<StatusCodeNode, OpenGraphData>()
                .IncludeBase<TreeNode, OpenGraphData>();
        }
    }
}
