using System;
using DriveNow.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DriveNow.DBContext
{
	public class ShopContext: DbContext
	{
		public ShopContext(DbContextOptions<ShopContext> options) : base(options)
		{

		}

		public DbSet<User> users { get; set; }

		public DbSet<Car> cars { get; set; }

		public DbSet<Catogory> catogories { get; set; }

		public DbSet<Promocode> promocodes { get; set; }

		public DbSet<CartItem> cartItems { get; set; }

		public DbSet<OrderItem> orderItems { get; set; }

		public DbSet<Order> orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Car>()
				.HasOne(x => x.Catogories)
				.WithMany(c => c.Cars)
				.HasForeignKey(y => y.CategoryForId);

			modelBuilder.Entity<CartItem>()
				.HasOne(x => x.User)
				.WithMany(c => c.CartItems)
				.HasForeignKey(x => x.UserId);

			modelBuilder.Entity<Order>()
				.HasOne(x => x.User)
				.WithMany(c => c.Orders)
				.HasForeignKey(z => z.UserId);

			modelBuilder.Entity<OrderItem>()
				.HasOne(x => x.Order)
				.WithMany(y => y.OrderItem)
				.HasForeignKey(z => z.OrderId);


			modelBuilder.Entity<User>().HasKey(s => new { s.UserId });

			modelBuilder.Entity<Car>().HasKey(s => new { s.CarId });

			modelBuilder.Entity<CartItem>().HasKey(s => new { s.CartItemId });

			modelBuilder.Entity<Catogory>().HasKey(s => new { s.CategoryId });

			modelBuilder.Entity<Order>().HasKey(s => new { s.OrderId });

			modelBuilder.Entity<OrderItem>().HasKey(s => new { s.OrderItemId });

			modelBuilder.Entity<Promocode>().HasKey(s => new { s.PromocodeId });
        }
    }
}

