using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Mvc.App.Mappings.Extensions;
using BlogTemplate.Mvc.App.Mappings.Resolvers;
using BlogTemplate.Mvc.App.Models;

namespace BlogTemplate.Mvc.App.Mappings
{
    public class ArticleMappingProfile : Profile
    {
        public ArticleMappingProfile( )
        {
            CreateMap<Article, ArticleViewModel>()
                .IncludeMetaData()
                .IncludeOpenGraphData()
                .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( article => article.Title ) )
                .ForMember( viewModel => viewModel.ModifiedAt, opt => opt.MapFrom( article => article.LastAuthoredAt ) );

            CreateMap<Article, ArticleListingItem>()
                .ForMember( item => item.Url, opt => opt.MapFrom<ArticleUrlResolver>() );
        }
    }
}
