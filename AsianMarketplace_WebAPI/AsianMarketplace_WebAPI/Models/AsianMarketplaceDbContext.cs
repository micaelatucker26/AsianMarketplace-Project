﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AsianMarketplace_WebAPI.Models
{
    public partial class AsianMarketplaceDbContext : DbContext
    {
        public AsianMarketplaceDbContext()
        {
        }

        public AsianMarketplaceDbContext(DbContextOptions<AsianMarketplaceDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CartItem> CartItems { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderItem> OrderItems { get; set; } = null!;
        public virtual DbSet<Shopper> Shoppers { get; set; } = null!;
        public virtual DbSet<ShoppingList> ShoppingLists { get; set; } = null!;
        public virtual DbSet<ShoppingListItem> ShoppingListItems { get; set; } = null!;
        public virtual DbSet<SubCategory> SubCategories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NS5UDT2;Database=AsianMarketplaceDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.UserId })
                    .HasName("PK__CartItem__A3060F2106CCBB11");

                entity.ToTable("CartItem");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("CartItem_ItemID_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartItems)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("CartItem_UserID_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UC_Category_Name")
                    .IsUnique();

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Item");

                entity.Property(e => e.ItemId)
                    .HasColumnName("ItemID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SubCategoryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.SubCategoryNameNavigation)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.SubCategoryName)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("SubCategory_FK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Orders)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("Shopper_FK");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.OrderId })
                    .HasName("PK__OrderIte__9E478651E5C85267");

                entity.ToTable("OrderItem");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("OrderItem_ItemID_FK");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("OrderItem_OrderID_FK");
            });

            modelBuilder.Entity<Shopper>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Shopper__1788CCACC91DA542");

                entity.ToTable("Shopper");

                entity.HasIndex(e => e.Username, "UC_Shopper_Name")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.HasKey(e => new { e.Title, e.UserId })
                    .HasName("PK__Shopping__FDCEE8177E466141");

                entity.ToTable("ShoppingList");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingLists)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ShoppingList_UserID_FK");
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.HasKey(e => new { e.Title, e.UserId, e.ItemId })
                    .HasName("PK__Shopping__17BC9694005304B1");

                entity.ToTable("ShoppingListItem");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("UserID");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.IsCrossedOff)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ShoppingListItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("ShoppingListItem_Item_FK");

                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.ShoppingListItems)
                    .HasForeignKey(d => new { d.Title, d.UserId })
                    .HasConstraintName("ShoppingListItem_ShoppingList_FK");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__SubCateg__737584F73EE87B95");

                entity.ToTable("SubCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.CategoryNameNavigation)
                    .WithMany(p => p.SubCategories)
                    .HasPrincipalKey(p => p.Name)
                    .HasForeignKey(d => d.CategoryName)
                    .HasConstraintName("Category_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
