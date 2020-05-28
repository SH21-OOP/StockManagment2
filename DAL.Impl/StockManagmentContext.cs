using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Impl
{
    public class StockManagmentContext : DbContext
    {

        public StockManagmentContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = @"Data Source=E:\data.db";
            optionsBuilder.UseSqlite(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(cpg => cpg.ProductGroup)
                .WithMany(p => p.Products)
                .HasForeignKey(cpg => cpg.ComercialProductGroupID)
                .HasPrincipalKey(p => p.Id);
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<ComercialProductGroup> ComercialProductGroup { get; set; }
    }
}
