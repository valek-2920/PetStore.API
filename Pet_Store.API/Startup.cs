using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Data;
using PetStore.Infraestructure.Data;
using PetStore.Infraestructure.Repository;
using PetStore.Infraestructure.Repository.UnitOfWork;
using System.Linq;

namespace Pet_Store.API
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
            services.AddDbContext<ApplicationDbContext>
             (options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionString")))
                 .AddUnitOfWork<ApplicationDbContext>()
                   .AddRepository<Category, CategoryRepository>()
                   .AddRepository<OrderDetails, OrderDetailsRepository>()
                   .AddRepository<OrderHeader, OrderHeaderRepository>()
                   .AddRepository<Products, ProductsRepository>()
                   .AddRepository<ShoppingCart, ShoppingCartRepository>()
                   .AddRepository<Users, UsersRepository>();

            services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pet_Store.API", Version = "v1" });
            });


            //services.AddScoped<IApplicationDbContext>
            //    (options => options.GetService<ApplicationDbContext>());

            //services.AddDbContext<ApplicationDbContext>
            //   (options => options.UseSqlServer(Configuration.GetConnectionString("Default")));


            //services.AddScoped<IApplicationDbContext>
            //    (options => options.GetService<ApplicationDbContext>());

            //services.AddIdentity<IdentityUser, IdentityRole>(options =>
            //{
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireLowercase = true;
            //    options.Password.RequiredLength = 5;
            //}).AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            //services.AddAuthentication(auth =>
            //{
            //    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = Configuration["AuthSettings:Audience"],
            //        ValidIssuer = Configuration["AuthSettings:Issuer"],
            //        RequireExpirationTime = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
            //        ValidateIssuerSigningKey = true
            //    };
            //});

            //services.AddScoped<IUserService, UserService>();
            //services.AddTransient<IMailService, SendGridMailService>();
            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            //services.AddScoped<IunitOfWork, UnitOfWork>();
            //services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pet_Store.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
