using Microsoft.EntityFrameworkCore;
using Project_PetStore.API.Models.DataModels;

namespace Project_PetStore.API.DataAccess
{
    public interface IApplicationDbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserDirection> UserDirection { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }

    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        public DbSet<Users> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<UserDirection> UserDirection { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<ShoppingCart> shoppingCarts { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
