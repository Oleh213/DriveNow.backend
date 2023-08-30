using System;
using DriveNow.Context;
using DriveNow.DTO;
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
		
		public DbSet<Order> orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Car>()
				.HasOne(x => x.Catogories)
				.WithMany(c => c.Cars)
				.HasForeignKey(y => y.CategoryForId);

			modelBuilder.Entity<Order>()
				.HasOne(x => x.User)
				.WithMany(c => c.Orders)
				.HasForeignKey(z => z.UserId);

			modelBuilder.Entity<Order>()
				.HasOne(x => x.User)
				.WithMany(y => y.Orders)
				.HasForeignKey(z => z.UserId);

			modelBuilder.Entity<Order>()
				.HasOne(user => user.Car)
				.WithMany(order => order.Orders)
				.HasForeignKey(user => user.CarId);

			modelBuilder.Entity<User>().HasKey(s => new { s.UserId });

			modelBuilder.Entity<Car>().HasKey(s => new { s.CarId });

			modelBuilder.Entity<Catogory>().HasKey(s => new { s.CategoryId });

			modelBuilder.Entity<Order>().HasKey(s => new { s.OrderId });

			modelBuilder.Entity<Promocode>().HasKey(s => new { s.PromocodeId });
        }
    }
}

