using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;

namespace PetStore.Infraestructure.Data
{
    public interface IApplicationDbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Payments> Payments { get; set; }
    }

}
