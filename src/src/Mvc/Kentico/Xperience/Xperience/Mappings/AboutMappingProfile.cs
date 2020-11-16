using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Kentico.Xperience.Models;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Kentico.Xperience.Mappings
{

    public class AboutMappingProfile : Profile
    {

        public AboutMappingProfile( )
        {
            CreateMap<AboutNode, AboutViewModel>()
                .ForMember( viewModel => viewModel.Meta, opt => opt.MapFrom( node => node ) )
                .ForMember( viewModel => viewModel.OpenGraph, opt => opt.MapFrom( node => node ) );

            CreateMap<AboutNode, MetaData>()
                .IncludeBase<TreeNode, MetaData>();

            CreateMap<AboutNode, OpenGraphData>()
                .IncludeBase<TreeNode, OpenGraphData>();
        }

    }

}
