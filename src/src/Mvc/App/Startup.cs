using BlogTemplate.Mvc.App.Infrastructure;
using BlogTemplate.Mvc.Errors;
using BlogTemplate.Mvc.Infrastructure.Xperience.Extensions;
using Kentico.Web.Mvc;
using WebMarkupMin.AspNetCore6;

namespace BlogTemplate.Mvc.App;

public class Startup
{
    #region Properties
    public IConfiguration Configuration { get; }
    public IHostEnvironment Environment { get; }
    #endregion

    public Startup( IConfiguration configuration, IHostEnvironment environment )
    {
        Configuration = configuration;
        Environment = environment;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices( IServiceCollection services )
    {
        var mvcBuilder = services.AddMvc()
            .AddDataAnnotationsLocalization();

#if DEBUG
        RazorRuntimeCompilationMvcBuilderExtensions.AddRazorRuntimeCompilation( mvcBuilder );
#endif

        services.AddAntiforgery();
        services.AddAuthentication();
        services.AddAuthorization();

        services.AddCors();
        services.AddRouting( options => options.LowercaseUrls = true );
        services.AddResponseCaching();
        services.AddResponseCompression( options => options.EnableForHttps = true );
        services.AddWebMarkupMin()
              .AddHtmlMinification()
              .AddHttpCompression()
              .AddXmlMinification();

        services.AddBlogTemplateKenticoMvc( Environment );
        services.AddBlogTemplateApp();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure( IApplicationBuilder app )
    {
        if( Environment.IsDevelopment() )
        {
            //app.UseDeveloperExceptionPage();
            app.UseBlogTemplateErrors();
        }
        else
        {
            app.UseBlogTemplateErrors();

            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCookiePolicy();
        app.UseCors();
        app.UseStaticFiles();

        app.UseRequestCancellation();

        app.UseResponseCompression();
        app.UseWebMarkupMin();
        app.UseResponseCaching();

        app.UseKentico();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(
            endpoints =>
            {
                endpoints.Kentico().MapRoutes();
                endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}" );
            }
        );
    }
}
