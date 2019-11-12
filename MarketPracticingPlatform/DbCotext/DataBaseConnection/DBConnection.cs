﻿using MarketPracticingPlatform.Data.DataBaseModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

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

                modelBuilder.Entity<Category>()
                .Property(b => b.ParentCategoryId)
                .HasDefaultValue(null);


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

                // optionsBuilder.UseMySQL("Server=localhost;Database=productsmarket;Uid=root;Pwd=;CharSet=utf8;");

            }

        }
    
}
