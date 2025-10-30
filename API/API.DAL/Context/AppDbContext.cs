using API.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace API.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>(entity =>
            {
                entity.HasIndex(p => p.Sku).IsUnique();

                entity.Property(p => p.Name)
                      .IsRequired()      
                      .HasMaxLength(100);

                entity.Property(p => p.Price)
                .HasPrecision(18, 2);
              
                entity.HasQueryFilter(p => !p.IsDeleted);

            });
        }
        }
}
