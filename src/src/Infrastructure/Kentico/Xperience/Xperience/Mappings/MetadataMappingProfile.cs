using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Extensions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Extensions;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings
{
    public class MetaDataMappingProfile : Profile
    {
        public MetaDataMappingProfile( )
        {
            CreateMap<TreeNode, MetaData>()
                .ForMember( meta => meta.Description, opt => opt.MapFrom( node => node.DocumentPageDescription ) )
                .ForMember( meta => meta.Keywords, opt => opt.MapFrom(
                    node => node.DocumentPageKeyWords.Split( new[] { "," }, StringSplitOptions.RemoveEmptyEntries )
                        .Concat( node.GetDocumentTags() )
                        .Select( keyword => keyword.Trim() )
                        .Distinct()
                        .OrderBy( keyword => keyword )
                ) )
                .ForMember( meta => meta.Title, opt => opt.MapFrom( node => node.GetStringValue( nameof( TreeNode.DocumentPageTitle ), node.DocumentName ) ) );

            CreateMap<TreeNode, OpenGraphData>()
                .ForMember(
                    openGraphData => openGraphData.Description,
                    opt => opt.MapFrom( node => node.GetStringValue( nameof( BaseNode.OpenGraphDescription ), node.DocumentPageDescription ) )
                )
                .ForMember( openGraphData => openGraphData.ImageUrl, opt => opt.ConvertMediaPathToUri( node => node.GetStringValue( nameof( BaseNode.OpenGraphImage ), string.Empty ) ) )
                .ForMember(
                    openGraphData => openGraphData.Title,
                    opt => opt.MapFrom( node => node.GetStringValue(
                            nameof( BaseNode.OpenGraphTitle ),
                            node.GetStringValue( nameof( TreeNode.DocumentPageTitle ), node.DocumentName )
                    ) )
                )
                .ForMember( openGraphData => openGraphData.VideoUrl, opt => opt.ConvertMediaPathToUri( node => node.GetStringValue( nameof( BaseNode.OpenGraphVideo ), string.Empty ) ) );
        }
    }
}
