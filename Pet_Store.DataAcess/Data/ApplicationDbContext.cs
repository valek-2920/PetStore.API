using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pet_Store.Domains.Models.DataModels;
using PetStore.Infraestructure.Data;


namespace Pet_Store.Infraestructure.Data
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Seed-Roles

            //Roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "ca2f3294-c463-4e37-af70-a57fe2b30d36", ConcurrencyStamp = "f14e68d6-91db-42e9-b3c5-e0ff68c5a3a0", Name = "Administrador", NormalizedName = "Administrador".ToUpper() });
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "72251981-78ae-4fa3-b76c-a80b286ee749", ConcurrencyStamp = "d8661340-9f2b-4da6-b0ae-6992cf41f8d4", Name = "Cliente", NormalizedName = "Cliente".ToUpper() });

            //Claims
            //For admin

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(new IdentityRoleClaim<string>
            {
                Id = 1,
                RoleId = "72251981-78ae-4fa3-b76c-a80b286ee749",
                ClaimType = "Roles",
                ClaimValue = "R"
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(new IdentityRoleClaim<string>
            {
                Id = 2,
                RoleId = "ca2f3294-c463-4e37-af70-a57fe2b30d36",
                ClaimType = "Roles",
                ClaimValue = "CRUD"
            });

            //SuperUser
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = "2577ec7c-cf49-4188-aa19-ead2263c33eb",
                UserName = "superuser@gmail.com",
                NormalizedUserName = "SUPERUSER@GMAIL.COM",
                Email = "superuser@gmail.com",
                NormalizedEmail = "SUPERUSER@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEJrQGBOpXrZcVpp3A3IRTWsHlyn9Y5JM/UWfefDstFBiqYFZyCeOa4EQk2McrTMPEA==",
                SecurityStamp = "ANUB6IG6WSDYCO6I3H6AMGCSYD53RDHJ",
                ConcurrencyStamp = "afac18e9-95d7-4f9a-9708-996e3d347a92",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = "2577ec7c-cf49-4188-aa19-ead2263c33eb", RoleId = "ca2f3294-c463-4e37-af70-a57fe2b30d36" });




            #endregion
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Products> Products { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
