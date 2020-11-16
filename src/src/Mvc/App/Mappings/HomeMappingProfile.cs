using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Mvc.App.Mappings.Extensions;
using BlogTemplate.Mvc.App.Models;

namespace BlogTemplate.Mvc.App.Mappings
{

    public class HomeMappingProfile : Profile
    {

        public HomeMappingProfile( )
        {
            CreateMap<Blog, HomeViewModel>()
                .IncludeMetaData()
                .IncludeOpenGraphData()
                .ForMember( viewModel => viewModel.Heading, opt => opt.MapFrom( blog => blog.Name ) );
        }

    }

}
