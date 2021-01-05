﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShoppingAppApi.Infrastructure;

namespace ShoppingAppApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210104070644_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ShoppingAppApi.Entity.AdminUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Accout")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Role")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("AdminUser");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Accout")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Goods", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Class")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<Guid?>("ShoppingBracketId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoppingBracketId");

                    b.ToTable("Goods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("add77178-3619-4cd5-aa54-f79ab42df1bc"),
                            Class = "电器",
                            Name = "热水器",
                            Price = 300m,
                            Stock = 200
                        },
                        new
                        {
                            Id = new Guid("4284cb01-5786-4112-9ecc-8ddf0c5cd17d"),
                            Class = "电器",
                            Name = "冰箱",
                            Price = 270m,
                            Stock = 800
                        },
                        new
                        {
                            Id = new Guid("81b4ab69-d459-47fa-81d6-58417ee5e7eb"),
                            Class = "电器",
                            Name = "TV",
                            Price = 300m,
                            Stock = 800
                        });
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("char(36)");

                    b.Property<string>("DeliverAddress")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.ShoppingBracket", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("ShoppingBracket");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Goods", b =>
                {
                    b.HasOne("ShoppingAppApi.Entity.Order", null)
                        .WithMany("GoodsList")
                        .HasForeignKey("OrderId");

                    b.HasOne("ShoppingAppApi.Entity.ShoppingBracket", null)
                        .WithMany("GoodsList")
                        .HasForeignKey("ShoppingBracketId");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Order", b =>
                {
                    b.HasOne("ShoppingAppApi.Entity.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.ShoppingBracket", b =>
                {
                    b.HasOne("ShoppingAppApi.Entity.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Order", b =>
                {
                    b.Navigation("GoodsList");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.ShoppingBracket", b =>
                {
                    b.Navigation("GoodsList");
                });
#pragma warning restore 612, 618
        }
    }
}
