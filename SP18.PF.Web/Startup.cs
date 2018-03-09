using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SP18.PF.Web.Data;
using SP18.PF.Web.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace SP18.PF.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // see: https://docs.microsoft.com/en-us/aspnet/core/security/authentication/cookie?tabs=aspnetcore2x
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    // see: https://devblog.dymel.pl/2016/07/07/return-401-unauthorized-from-asp-net-core-api/
                    opt.LoginPath = new PathString("/Admin/Account/Login");
                    opt.Events.OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api") &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    };
                });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc()
                // we are using "fluent validation" to make validators easy and automatically applied server and client side (rules in "*Validator" classes - e.g. LoginViewModelValidator)
                // see: https://github.com/JeremySkinner/FluentValidation
                .AddFluentValidation(x => x.RegisterValidatorsFromAssembly(typeof(Startup).Assembly));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API", Version = "v1" });
            });

            // we are using automapper to map data between different classes (mapping rules in "*Profile" classes - e.g. UserProfile)
            // see: https://github.com/AutoMapper/AutoMapper
            services.AddAutoMapper();

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<DbContext, DataContext>(); // note: we do this so we can use the project-agnostic "DbContext" in our controllers/services

            // these are custom services to help us do repetetive tasks, ideally these auto register but that is 'complex'
            services.AddScoped(typeof(CrudService<,,,>));
            services.AddScoped(typeof(NoSearchService<,>));
            services.AddScoped(typeof(EventSearchService));
            services.AddScoped(typeof(TicketService));
            services.AddScoped(typeof(UserService));

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSpaStaticFiles();

            app.UseMvc(routes =>
            {
                // see areas: https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/areas
                routes.MapRoute(
                    name: "adminArea",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            // this uses this template: https://docs.microsoft.com/en-us/aspnet/core/spa/react?tabs=visual-studio
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
