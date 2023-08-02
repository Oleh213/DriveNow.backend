﻿// <auto-generated />
using System;
using DriveNow.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DriveNow.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DriveNow.Context.Car", b =>
                {
                    b.Property<Guid>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AccualFileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CategoryForId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Expenditure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Free")
                        .HasColumnType("int");

                    b.Property<int>("FromOneToHundred")
                        .HasColumnType("int");

                    b.Property<int>("MaxSpeed")
                        .HasColumnType("int");

                    b.Property<string>("NameCar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Passengers")
                        .HasColumnType("int");

                    b.Property<int>("Power")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarId");

                    b.HasIndex("CategoryForId");

                    b.ToTable("cars");
                });

            modelBuilder.Entity("DriveNow.Context.CartItem", b =>
                {
                    b.Property<Guid>("CartItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartItemId");

                    b.HasIndex("UserId");

                    b.ToTable("cartItems");
                });

            modelBuilder.Entity("DriveNow.Context.Catogory", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("catogories");
                });

            modelBuilder.Entity("DriveNow.Context.Order", b =>
                {
                    b.Property<Guid>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("OrderTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Promocode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderId");

                    b.HasIndex("UserId");

                    b.ToTable("orders");
                });

            modelBuilder.Entity("DriveNow.Context.OrderItem", b =>
                {
                    b.Property<Guid>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("OrderItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("orderItems");
                });

            modelBuilder.Entity("DriveNow.Context.Promocode", b =>
                {
                    b.Property<Guid>("PromocodeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PromocodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.HasKey("PromocodeId");

                    b.ToTable("promocodes");
                });

            modelBuilder.Entity("DriveNow.Context.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Sex")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("DriveNow.Context.Car", b =>
                {
                    b.HasOne("DriveNow.Context.Catogory", "Catogories")
                        .WithMany("Cars")
                        .HasForeignKey("CategoryForId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catogories");
                });

            modelBuilder.Entity("DriveNow.Context.CartItem", b =>
                {
                    b.HasOne("DriveNow.Context.User", "User")
                        .WithMany("CartItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DriveNow.Context.Order", b =>
                {
                    b.HasOne("DriveNow.Context.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DriveNow.Context.OrderItem", b =>
                {
                    b.HasOne("DriveNow.Context.Order", "Order")
                        .WithMany("OrderItem")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DriveNow.Context.Catogory", b =>
                {
                    b.Navigation("Cars");
                });

            modelBuilder.Entity("DriveNow.Context.Order", b =>
                {
                    b.Navigation("OrderItem");
                });

            modelBuilder.Entity("DriveNow.Context.User", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
