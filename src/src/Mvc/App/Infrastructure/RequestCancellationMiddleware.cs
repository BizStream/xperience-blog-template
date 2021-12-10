namespace BlogTemplate.Mvc.App.Infrastructure;

public class RequestCancellationMiddleware
{
    #region Fields
    private readonly RequestDelegate next;
    #endregion

    public RequestCancellationMiddleware( RequestDelegate next )
        => this.next = next;

    public async Task Invoke( HttpContext context )
    {
        try
        {
            await next( context );
        }
        catch( Exception exception )
           when( exception is OperationCanceledException or TaskCanceledException )
        {
            context.Abort();
        }
    }
}
