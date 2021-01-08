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
    [Migration("20210107183836_addChecked")]
    partial class addChecked
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

                    b.Property<string>("Account")
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

                    b.Property<string>("Account")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NickName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<Guid>("ShoppingBracketId")
                        .HasColumnType("char(36)");

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

                    b.Property<string>("ImgSrc")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Goods");

                    b.HasData(
                        new
                        {
                            Id = new Guid("390c7b0f-1030-47d5-95c6-8a635a05a5bf"),
                            Class = "电器",
                            Name = "热水器",
                            Price = 300m,
                            Stock = 200
                        },
                        new
                        {
                            Id = new Guid("4990bb56-d7e1-48dd-a3f5-60b012dd37b8"),
                            Class = "电器",
                            Name = "冰箱",
                            Price = 270m,
                            Stock = 800
                        },
                        new
                        {
                            Id = new Guid("1fb33a89-2c00-43c8-aadb-67468d30c39d"),
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

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("ShoppingBracket");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.ShoppingBracketGoods", b =>
                {
                    b.Property<Guid>("GoodsId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ShoppingBracketId")
                        .HasColumnType("char(36)");

                    b.Property<int>("BracketGoodsNum")
                        .HasColumnType("int");

                    b.Property<bool>("Checked")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("GoodsId", "ShoppingBracketId");

                    b.HasIndex("ShoppingBracketId");

                    b.ToTable("ShoppingBracketGoods");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Goods", b =>
                {
                    b.HasOne("ShoppingAppApi.Entity.Order", null)
                        .WithMany("GoodsList")
                        .HasForeignKey("OrderId");
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
                        .WithOne("ShoppingBracket")
                        .HasForeignKey("ShoppingAppApi.Entity.ShoppingBracket", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.ShoppingBracketGoods", b =>
                {
                    b.HasOne("ShoppingAppApi.Entity.Goods", "Goods")
                        .WithMany("InShoppingBracketGoods")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShoppingAppApi.Entity.ShoppingBracket", "ShoppingBracket")
                        .WithMany("GoodsList")
                        .HasForeignKey("ShoppingBracketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("ShoppingBracket");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Customer", b =>
                {
                    b.Navigation("ShoppingBracket");
                });

            modelBuilder.Entity("ShoppingAppApi.Entity.Goods", b =>
                {
                    b.Navigation("InShoppingBracketGoods");
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
