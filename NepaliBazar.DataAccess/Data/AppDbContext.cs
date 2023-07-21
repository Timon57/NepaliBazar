using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NepaliBazar.Models;

namespace NepaliBazar.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<Microsoft.AspNet.Identity.IdentityUserLogin>().HasKey(u => u.UserId);
        //    builder.Entity<Microsoft.AspNet.Identity.IdentityUserRole>().HasKey(u => u.RoleId);
        //    // Configure any other entity configurations
        //    // ...
        //}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
    }
}
