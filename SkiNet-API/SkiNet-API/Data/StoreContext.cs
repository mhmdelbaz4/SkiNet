using Microsoft.EntityFrameworkCore;
using SkiNet_API.Entities;

namespace SkiNet_API
{
    public class StoreContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Price)
                                          .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
