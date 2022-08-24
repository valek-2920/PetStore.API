using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pet_Store.Domains.Models.DataModels;
using Pet_Store.Infraestructure.Repository.UnitOfWork;
using Pet_Store.Infraestructure.Repository.Repositories;
using Pet_Store.Application.Contracts.Data;
using Pet_Store.Infraestructure.Data;

namespace Pet_Store.Infraestructure
{
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>
         (options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString")))
             .AddUnitOfWork<ApplicationDbContext>()
               .AddRepository<Category, CategoryRepository>()
               .AddRepository<OrderDetails, OrderDetailsRepository>()
               .AddRepository<OrderHeader, OrderHeaderRepository>()
               .AddRepository<Products, ProductsRepository>()
               .AddRepository<ShoppingCart, ShoppingCartRepository>()
               .AddRepository<Users, UsersRepository>()
               .AddRepository<Payments, PaymentsRepository>();

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
