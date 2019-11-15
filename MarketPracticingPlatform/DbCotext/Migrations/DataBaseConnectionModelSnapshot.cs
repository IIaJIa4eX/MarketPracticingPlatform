﻿// <auto-generated />
using System;
using MarketPracticingPlatform.Data.DataBaseConnection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketPracticingPlatform.Migrations
{
    [DbContext(typeof(DBConnection))]
    partial class DataBaseConnectionModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099");

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int?>("ParentCategoryId");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.MainSub_Products", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MainProductId");

                    b.Property<int>("SubProductID");

                    b.HasKey("id");

                    b.ToTable("MainSubProducts");
                });

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Manufacturer");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.Property<int>("SoldOut");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.ProductCategory", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("CategoryId");

                    b.HasKey("ProductId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BonusScore");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Number");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MarketPracticingPlatform.Data.DataBaseModels.ProductCategory", b =>
                {
                    b.HasOne("MarketPracticingPlatform.Data.DataBaseModels.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MarketPracticingPlatform.Data.DataBaseModels.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
