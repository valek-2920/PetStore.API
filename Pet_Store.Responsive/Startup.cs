using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pet_Store.Application;
using Pet_Store.Domains.Models.MailModels;
using Pet_Store.Infraestructure;
using Pet_Store.Responsive.Services;
using Pet_Store.Responsive.Services.IServices;

namespace Pet_Store.Responsive
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.  IShoppingCartService
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.RegisterApplicationServices(Configuration);
            services.RegisterInfrastructureServices(Configuration);

            services.AddSingleton<ICartero, Cartero>();
            services.Configure<ConfiguracionSmtp>(Configuration.GetSection("ConfiguracionSmtp"));

            services.AddAuthentication
                  (
                      options =>
                      {
                          options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                      }
                  );
            //  .AddCookie
            //      (
            //          options =>
            //          {
            //              options.LoginPath = "/accounts/login";
            //              options.LogoutPath = "/accounts/logout";
            //              options.AccessDeniedPath = "/accounts/accessdenied";
            //              options.Cookie.SameSite = SameSiteMode.Lax;
            //          }
            //);

            services.ConfigureApplicationCookie
                (
                    options =>
                    {
                        options.AccessDeniedPath = new PathString("/User/AccessDenied");
                        options.AccessDeniedPath = new PathString("/Inventario/AccessDenied");
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
                        options.Password.RequireNonAlphanumeric = true;
                        options.Password.RequireDigit = true;
                        options.Password.RequireUppercase = true;
                        options.Password.RequireLowercase = true;
                        options.Password.RequiredUniqueChars = 1;
                    }
                );

            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddScoped<IInventarioServices, InventarioServices>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddScoped<IUserServices, UsersServices>();
            services.AddScoped<ICheckoutServices, CheckoutServices>();


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
            }
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
