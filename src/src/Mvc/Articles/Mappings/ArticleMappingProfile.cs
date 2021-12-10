using AutoMapper;
using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Xperience.AutoMapper.Extensions;
using BlogTemplate.Mvc.Articles.Abstractions;
using BlogTemplate.Mvc.Infrastructure.AutoMapper.Mappings.Extensions;
using BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Extensions;

namespace BlogTemplate.Mvc.Articles.Mappings;

public class ArticleMappingProfile : Profile
{
    public ArticleMappingProfile( )
    {
        CreateMap<ArticleNode, ArticleItem>()
            .ForMember( article => article.PublishedAt, opt => opt.ResolvePublishedDate() )
            .ForMember( article => article.Summary, opt => opt.MapFrom( node => node.Summary ) )
            .ForMember( article => article.Title, opt => opt.MapFrom( node => node.DocumentName ) )
            .ForMember( article => article.Url, opt => opt.ResolvePageUrl() );

        CreateMap<ArticleNode, ArticleViewModel>()
            .ForMember( viewModel => viewModel.AuthorGuid, opt => opt.MapFrom( node => node.AuthorGuid ) )
            .ForMember( viewModel => viewModel.Content, opt => opt.ConvertToHtmlContent( node => node.Content ) )
            .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( node => node.DocumentName ) )
            .ForMember( viewModel => viewModel.HeroImageUrl, opt => opt.ResolveMediaUrl( node => node.HeroImage ) )
            .ForMember( viewModel => viewModel.ModifiedAt, opt => opt.ResolveModifiedDate() )
            .ForMember( viewModel => viewModel.PublishedAt, opt => opt.ResolvePublishedDate() );
    }
}
