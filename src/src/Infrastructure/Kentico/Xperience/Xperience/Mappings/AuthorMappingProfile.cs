using AutoMapper;
using BlogTemplate.Core.Abstractions.Models;
using BlogTemplate.Infrastructure.Kentico.Xperience.Abstractions.PageTypes;
using BlogTemplate.Infrastructure.Kentico.Xperience.Mappings.Converters;

namespace BlogTemplate.Infrastructure.Kentico.Xperience.Mappings
{

    public class AuthorMappingProfile : Profile
    {

        public AuthorMappingProfile( )
        {
            CreateMap<AuthorNode, Author>()
                .ForMember( author => author.AuthorGuid, opt => opt.MapFrom( node => node.DocumentGUID ) )
                .ForMember( author => author.Description, opt => opt.MapFrom( node => node.Description ) )
                .ForMember( author => author.FacebookUrl, opt => opt.ConvertUsing<StringToUriConverter, string>() )
                .ForMember( author => author.GitHubUrl, opt => opt.ConvertUsing<StringToUriConverter, string>() )
                .ForMember( author => author.ImageUrl, opt => opt.ConvertUsing<StringToUriConverter, string>( node => node.Image ) )
                .ForMember( author => author.Name, opt => opt.MapFrom( node => node.DocumentName ) )
                .ForMember( author => author.TwitterUrl, opt => opt.ConvertUsing<StringToUriConverter, string>() );
        }

    }
}
