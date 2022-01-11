using AutoMapper;
using BizStream.AspNetCore.Components.OpenGraph.Abstractions;
using BlogTemplate.Infrastructure.Xperience.Abstractions;
using BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Extensions;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Seo.Mappings;

public class OpenGraphMappingProfile : Profile
{
    public OpenGraphMappingProfile( )
    {
        CreateMap<TreeNode, OpenGraphMeta>()
            .ForMember( openGraph => openGraph.Description, opt => opt.MapFrom( node => node.GetStringValue( OpenGraphFieldNames.Description, string.Empty ) ) )
            .ForMember( openGraph => openGraph.Images, opt => opt.MapFrom( node => node ) )
            .ForMember( openGraph => openGraph.Locale, opt => opt.MapFrom( node => node ) )
            .ForMember( openGraph => openGraph.SiteName, opt => opt.MapFrom( node => node.NodeSiteName ) )
            .ForMember( openGraph => openGraph.Title, opt => opt.MapFrom( node => node.GetStringValue( OpenGraphFieldNames.Title, string.Empty ) ) )
            .ForMember( openGraph => openGraph.Videos, opt => opt.MapFrom( node => node ) );

        CreateMap<IPageMetadata, OpenGraphMeta>()
            .ForMember(
                openGraph => openGraph.Description,
                opt =>
                {
                    opt.PreCondition( ( _, og, __ ) => string.IsNullOrEmpty( og.Description ) );
                    opt.MapFrom( meta => meta.Description );
                }
            )
            .ForMember(
                openGraph => openGraph.Title,
                opt =>
                {
                    opt.PreCondition( ( _, og, __ ) => string.IsNullOrEmpty( og.Title ) );
                    opt.MapFrom( meta => meta.Title );
                }
            );

        CreateMap<TreeNode, IList<OpenGraphImage>>()
            .ConstructUsing( ( node, context ) => new[] { context.Mapper.Map<OpenGraphImage>( node ) } )
            .ForAllMembers( opt => opt.Ignore() );

        CreateMap<TreeNode, OpenGraphImage>()
            .ForMember( image => image.Url, opt => opt.ResolveMediaUrl( node => node.GetStringValue( OpenGraphFieldNames.Image, string.Empty ) ) );

        CreateMap<TreeNode, OpenGraphLocale>()
            .ConstructUsing(
                node => new(
                    node.DocumentCulture,
                    node.GetTranslatedCultures()
                        .Where( cultureCode => !cultureCode.Equals( node.DocumentCulture ) )
                        .ToArray()
                )
            );

        CreateMap<TreeNode, IList<OpenGraphVideo>>()
            .ConstructUsing( ( node, context ) => new[] { context.Mapper.Map<OpenGraphVideo>( node ) } )
            .ForAllMembers( opt => opt.Ignore() );

        CreateMap<TreeNode, OpenGraphVideo>()
            .ForMember( image => image.Url, opt => opt.ResolveMediaUrl( node => node.GetStringValue( OpenGraphFieldNames.Video, string.Empty ) ) );
    }
}
