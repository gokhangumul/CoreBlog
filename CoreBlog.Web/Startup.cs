using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Business.Abstract;
using CoreBlog.Business.Concrete;
using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using CoreBlog.Data.Concrete.EfCore.Repository;
using CoreBlog.Data.Concrete.IdendityCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreBlog.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
            b=>b.MigrationsAssembly("CoreBlog.Data")));
            services.AddDbContext<AppIdentityDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"), 
            b=> b.MigrationsAssembly("CoreBlog.Data")));
            services.AddIdentity<AppUser, IdentityRole>(b=>
            {
                b.Password.RequiredLength = 8;
                b.Password.RequireDigit = true;
                b.Password.RequireNonAlphanumeric = true;
                b.Password.RequireLowercase = true;
                b.Password.RequireUppercase = true;
                b.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+";
                b.User.RequireUniqueEmail = true;
                
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();
             services.ConfigureApplicationCookie(_ =>
            {
                _.LoginPath = new PathString("/User/Login");
                _.LogoutPath = new PathString("/User/Logout");
                _.Cookie = new CookieBuilder
                {
                    Name = "AspNetCoreIdentityCookie",
                    HttpOnly = false,
                    Expiration = TimeSpan.FromMinutes(30),
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
                _.SlidingExpiration = true; 
                _.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            services.AddTransient<IUserService, UserServiceManager>();
            services.AddTransient<ICategoryService, CategoryManager>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IImageService, ImageManager>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IPostService, PostManager>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ISettingsService, SettingsManager>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(route=> {
                route.MapRoute(
                name:"default",
                template: "{controller=User}/{action=Login}/{uniqkey?}"
                );
            route.MapRoute(
               "About",
               "About",
               new { controller = "Hakkimda", action = "Index" }
                    );
                route.MapRoute(
               "Contact",
               "Contact",
               new { controller = "Iletisim", action = "Index" }
                    );
                route.MapRoute(
                    "Posts",
                    "Posts",
                    new {controller="Makale",action="Index"}
                    );
            });
            SeedIdentity.CreateUser(app.ApplicationServices, Configuration).Wait();
        }
    }
}
