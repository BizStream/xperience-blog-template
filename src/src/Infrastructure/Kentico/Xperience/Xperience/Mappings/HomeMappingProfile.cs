using System;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings
{

    public class HomeMappingProfile : Profile
    {

        public HomeMappingProfile( )
        {
            CreateMap<HomeNode, Home>()
                .ForMember( home => home.FeaturedAuthorGuid, opt => opt.Condition( node => node.FeaturedAuthorGuid != Guid.Empty ) );

            CreateMap<HomeNode, MetaData>()
                .IncludeBase<TreeNode, MetaData>();

            CreateMap<HomeNode, OpenGraphData>()
                .IncludeBase<TreeNode, OpenGraphData>();
        }

    }

}
