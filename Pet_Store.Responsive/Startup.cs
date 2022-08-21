using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pet_Store.Responsive.Services;
using Pet_Store.Responsive.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.DataAcess;
using PetStore.DataAccess.Repository;
using Pet_Store.Domains.Models.DataModels;

namespace Pet_Store.Responsive
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            var contentRoot = env.ContentRootPath;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("Default")))
                    .AddUnitOfWork<ApplicationDbContext>()
                    .AddRepository<Category, CategoryRepository>()
                    .AddRepository<OrderDetails, OrderDetailsRepository>()
                    .AddRepository<OrderHeader, OrderHeaderRepository>()
                    .AddRepository<Products, ProductsRepository>()
                    .AddRepository<ShoppingCart, ShoppingCartRepository>()
                    .AddRepository<Users, UsersRepository>();



            services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());

            services.RegisterApplicationServices(Configuration);
            services.RegisterInfrastructureServices(Configuration);

            services.AddAuthentication
                (
                    options =>
                    {
                        options.DefaultScheme =
                            CookieAuthenticationDefaults.AuthenticationScheme;
                    }
                );

            services.ConfigureApplicationCookie
                (
                    options =>
                    {
                        options.LoginPath = new PathString("/Accounts/login");
                        options.LogoutPath = new PathString("/Accounts/logout");
                        //ADD NEW ONE FOR EACH CONTROLLER IS PENDING
                        options.Cookie.SameSite = SameSiteMode.Lax;
                    }
                );

            services.AddMvc
                (
                    options =>
                    {
                        var policy =
                            new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();

                        options.Filters.Add
                            (new AuthorizeFilter(policy));
                    }
                )
                .AddXmlSerializerFormatters();

            services.Configure<IdentityOptions>
                (
                    options =>
                    {
                        options.Password.RequiredLength = 8;
                        options.Password.RequiredUniqueChars = 3;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireNonAlphanumeric = true;
                    }
                );

            services.AddControllersWithViews();
            services.AddScoped<IInventarioServices, InventarioServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy
                (
                    new CookiePolicyOptions
                    {
                        MinimumSameSitePolicy = SameSiteMode.Lax
                    }
                );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
