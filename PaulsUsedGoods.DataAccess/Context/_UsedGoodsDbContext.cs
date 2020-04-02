using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PaulsUsedGoods.DataAccess.Context
{
    public class UsedGoodsDbContext : DbContext
    { 
        public UsedGoodsDbContext(DbContextOptions options) : base(options)  
        {  

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.Property(e => e.SellerId)
                    .IsRequired();
                entity.Property(e => e.SellerName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId)
                    .IsRequired();
                entity.Property(e => e.LocationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TopicOption>(entity =>
            {
                entity.Property(e => e.TopicOptionId)
                    .IsRequired();
                entity.Property(e => e.TopicName)
                    .IsRequired()
                    .HasMaxLength(50);
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
                    .HasForeignKey(e => e.OrderId);
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

        DbSet<Item> Items {get; set;}
        DbSet<Order> Orders {get; set;}
        DbSet<Person> People { get; set; }
        DbSet<Review> Reviews {get; set;}
        DbSet<Seller> Sellers {get; set;}
        DbSet<Store> Stores {get; set;}
        DbSet<TopicOption> TopicOptions {get; set;}
        
    }
}
