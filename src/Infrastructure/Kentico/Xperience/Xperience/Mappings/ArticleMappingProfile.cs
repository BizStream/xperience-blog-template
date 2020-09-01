using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Extensions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Resolvers;
using CMS.DocumentEngine;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings
{

    public class ArticleMappingProfile : Profile
    {

        public ArticleMappingProfile( )
        {
            CreateMap<ArticleNode, Article>()
                .IncludeAuthored()
                .ForMember( article => article.HeroImageUrl, opt => opt.ConvertUsing<StringToUriConverter, string>( node => node.HeroImage ) )
                .ForMember( article => article.PublishedAt, opt => opt.MapFrom<DocumentPublishDateResolver>() )
                .ForMember( article => article.Slug, opt => opt.MapFrom( node => node.NodeAlias ) )
                .ForMember( article => article.Summary, opt => opt.MapFrom( node => node.Summary ) )
                .ForMember( article => article.Title, opt => opt.MapFrom( node => node.DocumentName ) );

            // use default meta-data mapping
            CreateMap<ArticleNode, MetaData>()
                .IncludeBase<TreeNode, MetaData>()
                .ForMember( meta => meta.Description, opt => opt.MapFrom( node => node.Summary ) );

            CreateMap<ArticleNode, OpenGraphData>()
                .IncludeBase<TreeNode, OpenGraphData>()
                .ForMember( openGraphData => openGraphData.Description, opt => opt.MapFrom( node => node.Summary ) );
        }

    }

}
