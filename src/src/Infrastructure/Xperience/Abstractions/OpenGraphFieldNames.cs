namespace BlogTemplate.Infrastructure.Xperience.Abstractions;

public static class OpenGraphFieldNames
{
    #region Fields
    private const string Prefix = "OpenGraph";
    #endregion

    public const string Description = Prefix + nameof( Description );
    public const string Image = Prefix + nameof( Image );
    public const string Title = Prefix + nameof( Title );
    public const string Video = Prefix + nameof( Video );
}
