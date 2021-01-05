using System;
using Microsoft.EntityFrameworkCore;
using ShoppingAppApi.Entity;

namespace ShoppingAppApi.Infrastructure
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<ShoppingBracket> ShoppingBracket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Goods>().HasData(
                new Goods
                {
                    Id = Guid.NewGuid(),
                    Name = "热水器",
                    Price = new decimal(300),
                    Stock = 200,
                    Class = "电器"
                },
                new Goods
                {
                    Id = Guid.NewGuid(),
                    Name = "冰箱",
                    Price = new decimal(270),
                    Stock = 800,
                    Class = "电器"
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Name = "TV",
                    Price = new decimal(300),
                    Stock = 800,
                    Class = "电器"
                });
            base.OnModelCreating(modelBuilder);

        }
    }
}
