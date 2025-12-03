using SportsClothes.DAOSQL.BO;
using Microsoft.EntityFrameworkCore;

namespace SportsClothes.DAOSQL
{
    public class SportsClothesDbContext : DbContext
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Product> Products { get; set; }

        string dbPath = Path.Combine(AppContext.BaseDirectory, "Database", "DaoSqlite.db");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={dbPath}");
            Console.WriteLine($"Database path: {dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProducerImpl)
                .WithMany()
                .HasForeignKey(p => p.ProducerId);
        }


    }
}
