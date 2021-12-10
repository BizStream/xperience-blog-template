using AutoMapper;
using CMS.DocumentEngine;

namespace BlogTemplate.Mvc.Infrastructure.Xperience.AutoMapper.Resolvers;

public class PageUrlResolver<TNode, TDest> : IValueResolver<TNode, TDest, PageUrl>, IValueResolver<TNode, TDest, string>, IValueResolver<TNode, TDest, Uri>
    where TNode : TreeNode
{
    #region Fields
    private readonly IPageUrlRetriever pageUrlRetriever;
    #endregion

    public PageUrlResolver( IPageUrlRetriever pageUrlRetriever )
        => this.pageUrlRetriever = pageUrlRetriever;

    public PageUrl Resolve( TNode source, TDest _, PageUrl __, ResolutionContext ___ )
    {
        ArgumentNullException.ThrowIfNull( source );
        return pageUrlRetriever.Retrieve( source );
    }

    public string Resolve( TNode source, TDest destination, string _, ResolutionContext context )
        => Resolve( source, destination, default( PageUrl )!, context )
            .AbsoluteUrl;

    public Uri Resolve( TNode source, TDest destination, Uri _, ResolutionContext context )
    {
        string url = Resolve( source, destination, default( string )!, context );
        return new( url, UriKind.Absolute );
    }
}
