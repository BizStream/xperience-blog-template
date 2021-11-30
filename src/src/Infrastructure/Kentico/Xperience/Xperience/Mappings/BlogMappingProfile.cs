using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings
{
    public class BlogMappingProfile : Profile
    {
        public BlogMappingProfile( )
        {
            CreateMap<HomeNode, Blog>()
                .ForMember( blog => blog.FeaturedAuthorGuid, opt => opt.Condition( node => node.FeaturedAuthorGuid != Guid.Empty ) )
                .ForMember( blog => blog.Name, opt => opt.MapFrom( node => node.DocumentName ) );

            CreateMap<HomeNode, MetaData>()
                .IncludeBase<TreeNode, MetaData>();

            CreateMap<HomeNode, OpenGraphData>()
                .IncludeBase<TreeNode, OpenGraphData>();
        }
    }
}
