using System.Reflection;
using BlogTemplate.Infrastructure.Extensions;
using BlogTemplate.Mvc.App.Extensions;
using BlogTemplate.Mvc.Kentico.Xperience.Controllers;
using BlogTemplate.Mvc.Kentico.Xperience.Extensions;
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
            app.UseResponseCaching();
            app.UseResponseCompression();

            app.UseKentico();

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
