using System.Reflection;
using BlogTemplate.Infrastructure.Extensions;
using BlogTemplate.Infrastructure.Kentico.Xperience.Retrievers;
using BlogTemplate.Mvc.App.Extensions;
using BlogTemplate.Mvc.Kentico.Xperience.Controllers;
using CMS.SiteProvider;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogTemplate.Mvc.App
{

    public class Startup
    {
        #region Fields
        private const string UIRootPath = "client-ui/dist";
        private static readonly Assembly MvcAssembly = typeof( Startup ).Assembly;
        #endregion

        public IConfiguration Configuration { get; }
        public IHostEnvironment Environment { get; }

        public Startup( IConfiguration configuration, IHostEnvironment environment )
        {
            Configuration = configuration;
            Environment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            var mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            RazorRuntimeCompilationMvcBuilderExtensions.AddRazorRuntimeCompilation( mvcBuilder );
#endif

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddCors();
            services.AddRouting( options => options.LowercaseUrls = true );
            services.AddResponseCaching();
            services.AddResponseCompression( options => options.EnableForHttps = true );

            services.AddBlogKentico( Environment );
            services.AddBlogMappings();
            services.AddBlogServices();

            services.AddOptions<StaticFileOptions>()
                .ConfigureEmbeddedProvider( MvcAssembly, UIRootPath )
                .ConfigureEmbeddedProvider( typeof( AboutController ).Assembly, UIRootPath );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app )
        {
            if( Environment.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Home/Error" );

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseCors();

            app.UseStaticFiles();
            app.UseRepsonseCaching();
            app.UseResponseCompression();

            app.UseKentico(
                features =>
                {
                    features.UsePageBuilder();
                    features.UsePreview();
                }
            );

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.Kentico().MapRoutes();
                    endpoints.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}" );
                }
            );
        }

    }

}
