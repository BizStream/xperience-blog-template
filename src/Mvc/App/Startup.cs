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

        public Startup( IConfiguration configuration )
            => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {
            var mvcBuilder = services.AddControllersWithViews();
#if DEBUG
            RazorRuntimeCompilationMvcBuilderExtensions.AddRazorRuntimeCompilation( mvcBuilder );
#endif

            services.AddCors();
            services.AddResponseCompression( options => options.EnableForHttps = true );
            services.AddKentico();

            services.AddBlogMappings();
            services.AddBlogServices();

            // configure how documents are queried
            services.AddOptions<DocumentRetrieverOptions>()
                .PostConfigure( options => options.SiteID = SiteContext.CurrentSiteID );

            services.AddEmbeddedStaticFileProvider( MvcAssembly, UIRootPath );
            services.AddEmbeddedStaticFileProvider( typeof( AboutController ).Assembly, UIRootPath );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if( env.IsDevelopment() )
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
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseCors();

            app.UseKentico(
                features =>
                {
                    features.UsePageBuilder();
                    features.UsePreview();
                }
            );

            app.UseRouting();
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

}
