using AutoMapper;
using CMS.SiteProvider;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Resolvers;

public class MediaUrlResolver<TSource, TDest> : IMemberValueResolver<TSource, TDest, string, string>, IMemberValueResolver<TSource, TDest, string, Uri>
{
    private static string CreateAbsolutePath( string mediaPath )
        => SiteContext.CurrentSite.SitePresentationURL + mediaPath.TrimStart( '~' );

    public string Resolve( TSource source, TDest destination, string sourceMember, string destMember, ResolutionContext context )
        => CreateAbsolutePath( sourceMember );

    public Uri Resolve( TSource source, TDest destination, string sourceMember, Uri destMember, ResolutionContext context )
        => new( CreateAbsolutePath( sourceMember ) );
}
