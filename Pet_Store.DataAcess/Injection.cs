using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pet_Store.DataAcess.Data;
using Pet_Store.DataAcess.Repository.UnitOfWork;
using Pet_Store.Domains.Models.DataModels;
using PetStore.DataAccess.Repository;


namespace Pet_Store.DataAcess
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
              (options => options.UseSqlServer(configuration.GetConnectionString("Default")))
                  .AddUnitOfWork<ApplicationDbContext>()
                    .AddRepository<Category, CategoryRepository>()
                    .AddRepository<OrderDetails, OrderDetailsRepository>()
                    .AddRepository<OrderHeader, OrderHeaderRepository>()
                    .AddRepository<Products, ProductsRepository>()
                    .AddRepository<ShoppingCart, ShoppingCartRepository>()
                    .AddRepository<Users, UsersRepository>();

            services.AddScoped<IApplicationDbContext>
                (options => options.GetService<ApplicationDbContext>());

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>
                    (TokenOptions.DefaultProvider);

            return services;
        }
    }
}
