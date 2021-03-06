﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class UsedGoodsDbContext : DbContext
    {
        public virtual DbSet<Item> Items {get; set;}
        public virtual DbSet<Order> Orders {get; set;}
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Review> Reviews {get; set;}
        public virtual DbSet<Seller> Sellers {get; set;}
        public virtual DbSet<Store> Stores {get; set;}
        public virtual DbSet<TopicOption> TopicOptions {get; set;}
        public UsedGoodsDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopicOption>().HasData(
                new TopicOption()
                {
                    TopicOptionId = 1, TopicName = "Candy"
                }
            );
            modelBuilder.Entity<Store>().HasData(
                new Store()
                {
                    StoreId = 1, LocationName = "Arlington,TX"
                }
            );
            modelBuilder.Entity<Person>().HasData(
                new Person()
                {
                    PersonId = 1, StoreId = 1, FirstName = "ad", LastName = "min", Username = "admin", Password = "admin", Employee = true
                }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    OrderId = 1, PersonId = 1, OrderDate = DateTime.Now, TotalOrderPrice = 2.50
                }
            );
            modelBuilder.Entity<Seller>().HasData(
                new Seller()
                {
                    SellerId = 1, SellerName = "Fat Joe"
                }
            );
            modelBuilder.Entity<Review>().HasData(
                new Review()
                {
                    ReviewId = 1,PersonId = 1, SellerId = 1, Score = 6, Comment = "All candy is half eaten, so I am giving half of a "
                }
            );
            modelBuilder.Entity<Item>().HasData(
                new Item()
                {
                    ItemId = 1, StoreId = 1, OrderId = 1, SellerId = 1, TopicId = 1, ItemName = "Candy Bar", ItemDescription = "Half of a Kit-Kat bar", ItemPrice = 2.50
                }
            );


            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.SellerId)
                    .IsRequired();
                entity.Property(e => e.SellerName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId)
                    .IsRequired();
                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<TopicOption>(entity =>
            {
                entity.Property(e => e.TopicOptionId)
                    .IsRequired();
                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);
                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);
                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(128);
                entity.HasIndex(e => e.StoreId);
                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);
                entity.Property(e => e.Employee)
                    .HasDefaultValue(false);
                entity.HasOne(e => e.Store)
                    .WithMany(e => e.Person)
                    .HasForeignKey(e => e.StoreId);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .IsRequired();
                entity.HasIndex(e => e.PersonId);
                entity.Property(e => e.OrderDate)
                    .IsRequired();
                entity.Property(e => e.TotalOrderPrice)
                    .IsRequired();
                entity.HasOne(e => e.Person)
                    .WithMany(e => e.Order)
                    .HasForeignKey(e => e.PersonId);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId)
                    .IsRequired();
                entity.HasIndex(e => e.PersonId);
                entity.HasIndex(e => e.SellerId);
                entity.Property(e => e.Score)
                    .IsRequired();
                entity.Property(e => e.Comment)
                    .HasMaxLength(2048);
                entity.HasOne(e => e.Person)
                    .WithMany(e => e.Review)
                    .HasForeignKey(e => e.PersonId);
                entity.HasOne(e => e.Seller)
                    .WithMany(e => e.Review)
                    .HasForeignKey(e => e.SellerId);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId)
                    .IsRequired();
                entity.HasIndex( e => e.StoreId);
                entity.HasIndex(e => e.OrderId);
                entity.HasIndex(e => e.SellerId);
                entity.HasIndex(e => e.TopicId);
                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(128);
                entity.Property(e => e.ItemDescription)
                    .HasMaxLength(1024);
                entity.Property(e => e.ItemPrice)
                    .IsRequired();
                entity.HasOne(e => e.Store)
                    .WithMany(e => e.Item)
                    .HasForeignKey(e => e.StoreId);
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.Item)
                    .HasForeignKey(e => e.OrderId);
                entity.HasOne(e => e.Seller)
                    .WithMany(e => e.Item)
                    .HasForeignKey(e => e.SellerId);
                entity.HasOne(e => e.TopicOption)
                    .WithMany(e => e.Item)
                    .HasForeignKey(e => e.TopicId);
            });
        }
    }
}
