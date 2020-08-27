using System;
using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Xperience.Mappings.Converters;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Xperience.Mappings
{

    public class MetaDataMappingProfile : Profile
    {

        public MetaDataMappingProfile( )
        {
            CreateMap<TreeNode, MetaData>()
                .ForMember( meta => meta.Description, opt => opt.MapFrom( node => node.DocumentPageDescription ) )
                .ForMember( meta => meta.Keywords, opt => opt.MapFrom(
                    node => node.DocumentPageKeyWords.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
                ) )
                .ForMember( meta => meta.Title, opt => opt.MapFrom( node => node.GetStringValue( nameof( TreeNode.DocumentPageTitle ), node.DocumentName ) ) );

            CreateMap<TreeNode, OpenGraphData>()
                .ForMember(
                    openGraphData => openGraphData.Description,
                    opt => opt.MapFrom( node => node.GetStringValue( nameof( BaseNode.OpenGraphDescription ), node.DocumentPageDescription ) )
                )
                .ForMember( openGraphData => openGraphData.ImageUrl, opt => opt.ConvertUsing<StringToUriConverter, string>( node => node.GetStringValue( nameof( BaseNode.OpenGraphImage ), string.Empty ) ) )
                .ForMember(
                    openGraphData => openGraphData.Title,
                    opt => opt.MapFrom( node => node.GetStringValue(
                            nameof( BaseNode.OpenGraphTitle ),
                            node.GetStringValue( nameof( TreeNode.DocumentPageTitle ), node.DocumentName )
                    ) )
                )
                .ForMember( openGraphData => openGraphData.VideoUrl, opt => opt.ConvertUsing<StringToUriConverter, string>( node => node.GetStringValue( nameof( BaseNode.OpenGraphVideo ), string.Empty ) ) );
        }

    }

}
