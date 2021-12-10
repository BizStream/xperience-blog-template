﻿using AutoMapper;
using BlogTemplate.Infrastructure.Xperience.Abstractions.PageTypes;
using BlogTemplate.Mvc.Authors.Abstractions;

namespace BlogTemplate.Mvc.Authors.Mappings;

public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile( )
    {
        CreateMap<AuthorNode, AuthorItem>()
            .ForMember( author => author.Description, opt => opt.MapFrom( node => node.Description ) )
            .ForMember( author => author.ImageUrl, opt => opt.MapFrom( node => node.Image ) )
            .ForMember( author => author.Name, opt => opt.MapFrom( node => node.DocumentName ) );
    }
}
