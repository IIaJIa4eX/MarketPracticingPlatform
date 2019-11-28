using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.EntityFrameworkCore;

namespace MarketPracticingPlatform.Data.DataBaseConnection
{

    public class DBConnection : DbContext
        {



        public DBConnection(DbContextOptions<DBConnection> option) : base(option)
        {


        }


        public DbSet<User> Users { get; set; }

            public DbSet<Product> Products { get; set; }

            public DbSet<Category> Categories { get; set; }

            public DbSet<ProductCategory> ProductCategories { get; set; }

            public DbSet<MainSub_Products> MainSubProducts { get; set; }
            

      



        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {


                modelBuilder.Entity<ProductCategory>()
                    .HasKey(t => new { t.ProductId, t.CategoryId });


                modelBuilder.Entity<ProductCategory>()
                    .HasOne(sc => sc.Product)
                    .WithMany(s => s.ProductCategories)
                    .HasForeignKey(sc => sc.ProductId);


                modelBuilder.Entity<ProductCategory>()
                    .HasOne(sc => sc.Category)
                    .WithMany(c => c.ProductCategories)
                    .HasForeignKey(sc => sc.CategoryId);

            }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

            {

                

            }

        }
    
}
