using AutoMapper;
using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Home.Abstractions;

namespace BlogTemplate.Mvc.Home.Mappings;

public class HomeMappingProfile : Profile
{
    public HomeMappingProfile( )
    {
        CreateMap<HomeNode, HomeViewModel>()
            .ForMember( viewModel => viewModel.FeaturedAuthorGuid, opt => opt.Condition( node => node.FeaturedAuthorGuid != Guid.Empty ) )
            .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( node => node.DocumentName ) );
    }
}
