using System;
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
                entity.ToTable("CartItem");

                entity.HasIndex(e => new { e.ItemId, e.UserId }, "UQ_CartItem_ItemID_UserID")
                    .IsUnique();

                entity.Property(e => e.CartItemId)
                    .HasColumnName("CartItemID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("CartItem_ItemID_FK");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CartItems)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("CartItem_UserID_FK");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(e => e.Name, "UQ_Name")
                    .IsUnique();

                entity.Property(e => e.CategoryId)
                    .HasColumnName("CategoryID")
                    .HasDefaultValueSql("(newid())");

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

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Items)
                    .HasForeignKey(d => d.SubCategoryId)
                    .HasConstraintName("SubCategory_FK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("Shopper_FK");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => new { e.ItemId, e.OrderId })
                    .HasName("PK__OrderIte__9E478651C1B375E1");

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
                    .HasName("PK__Shopper__1788CCACC649A93D");

                entity.ToTable("Shopper");

                entity.HasIndex(e => e.Username, "UQ__Shopper__536C85E40EDF9E14")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShoppingList>(entity =>
            {
                entity.ToTable("ShoppingList");

                entity.Property(e => e.ShoppingListId)
                    .HasColumnName("ShoppingListID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.IsActive)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingLists)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("ShoppingList_UserID_FK");
            });

            modelBuilder.Entity<ShoppingListItem>(entity =>
            {
                entity.ToTable("ShoppingListItem");

                entity.Property(e => e.ShoppingListItemId)
                    .HasColumnName("ShoppingListItemID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsCrossedOff)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength();

                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ShoppingListId).HasColumnName("ShoppingListID");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.ShoppingListItems)
                    .HasForeignKey(d => d.ItemId)
                    .HasConstraintName("ShoppingListItem_Item_FK");

                entity.HasOne(d => d.ShoppingList)
                    .WithMany(p => p.ShoppingListItems)
                    .HasForeignKey(d => d.ShoppingListId)
                    .HasConstraintName("ShoppingListItem_ShoppingList_FK");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.ToTable("SubCategory");

                entity.HasIndex(e => e.Name, "UQ__SubCateg__737584F62ED49748")
                    .IsUnique();

                entity.Property(e => e.SubCategoryId)
                    .HasColumnName("SubCategoryID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("Category_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
