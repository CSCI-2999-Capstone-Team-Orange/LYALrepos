using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace LoveYouALatte.Data.Entities
{
    public partial class loveyoualattedbContext : DbContext
    {
        public loveyoualattedbContext()
        {
        }

        public loveyoualattedbContext(DbContextOptions<loveyoualattedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<CartTable> CartTables { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<UserOrder> UserOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server= authtest.cjiyeakoxxft.us-east-1.rds.amazonaws.com;port=3306;user=test;password=orange1234;database=loveyoualattedb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.ClaimType).HasColumnType("longtext");

                entity.Property(e => e.ClaimValue).HasColumnType("longtext");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).HasMaxLength(255);

                entity.Property(e => e.ConcurrencyStamp).HasColumnType("longtext");

                entity.Property(e => e.Discriminator)
                    .IsRequired()
                    .HasColumnType("longtext");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(250)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(250)
                    .HasColumnName("lastName");

                entity.Property(e => e.LockoutEnd).HasColumnType("datetime(6)");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.PasswordHash).HasColumnType("longtext");

                entity.Property(e => e.PhoneNumber).HasColumnType("longtext");

                entity.Property(e => e.SecurityStamp).HasColumnType("longtext");

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.ClaimType).HasColumnType("longtext");

                entity.Property(e => e.ClaimValue).HasColumnType("longtext");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.ProviderDisplayName).HasColumnType("longtext");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name })
                    .HasName("PRIMARY");

                entity.Property(e => e.UserId).HasMaxLength(255);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.Property(e => e.Value).HasColumnType("longtext");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<CartTable>(entity =>
            {
                entity.HasKey(e => e.IdCartTable)
                    .HasName("PRIMARY");

                entity.ToTable("CartTable");

                entity.HasIndex(e => e.IdUser, "cartTableUserID");

                entity.HasIndex(e => e.IdProduct, "productID_idx");

                entity.Property(e => e.IdCartTable).HasColumnName("idCartTable");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdUser)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("idUser");

                entity.Property(e => e.LineCost)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("lineCost");

                entity.Property(e => e.LineItemCost)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("lineItemCost");

                entity.Property(e => e.LineTax)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("lineTax");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.CartTables)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productID");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.CartTables)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("cartTableUserID");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.HasKey(e => e.IdDrinks)
                    .HasName("PRIMARY");

                entity.ToTable("drinks");

                entity.Property(e => e.IdDrinks).HasColumnName("idDrinks");

                entity.Property(e => e.DrinkDescription)
                    .IsRequired()
                    .HasColumnName("drink_description");

                entity.Property(e => e.DrinkName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("drink_name");
            });

            modelBuilder.Entity<EfmigrationsHistory>(entity =>
            {
                entity.HasKey(e => e.MigrationId)
                    .HasName("PRIMARY");

                entity.ToTable("__EFMigrationsHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryId).HasColumnName("inventoryId");

                entity.Property(e => e.InvDescription)
                    .HasColumnType("longtext")
                    .HasColumnName("invDescription");

                entity.Property(e => e.InvName)
                    .HasMaxLength(255)
                    .HasColumnName("invName");

                entity.Property(e => e.InvPrice)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("invPrice");

                entity.Property(e => e.InvQuantity).HasColumnName("invQuantity");

                entity.Property(e => e.InvSize)
                    .HasMaxLength(255)
                    .HasColumnName("invSize");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("orderItem");

                entity.HasIndex(e => e.ProductId, "prodIDFK_idx");

                entity.HasIndex(e => e.UserOrderId, "userOrderIDFK");

                entity.Property(e => e.OrderItemId).HasColumnName("orderItemId");

                entity.Property(e => e.LineItemCost)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("lineItemCost");

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.Tax)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("tax");

                entity.Property(e => e.TotalCost)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("totalCost");

                entity.Property(e => e.UserOrderId).HasColumnName("userOrderId");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("prodIDFK");

                entity.HasOne(d => d.UserOrder)
                    .WithMany(p => p.OrderItems)
                    .HasForeignKey(d => d.UserOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("userOrderIDFK");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PRIMARY");

                entity.ToTable("product");

                entity.HasIndex(e => e.IdDrink, "drinkIDFK");

                entity.HasIndex(e => e.IdSize, "sizeIDFK");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdDrink).HasColumnName("idDrink");

                entity.Property(e => e.IdSize).HasColumnName("idSize");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(13,2)")
                    .HasColumnName("price");

                entity.HasOne(d => d.IdDrinkNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdDrink)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("drinkIDFK");

                entity.HasOne(d => d.IdSizeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("sizeIDFK");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.IdSize)
                    .HasName("PRIMARY");

                entity.ToTable("size");

                entity.Property(e => e.IdSize).HasColumnName("idSize");

                entity.Property(e => e.Size1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("size");
            });

            modelBuilder.Entity<UserOrder>(entity =>
            {
                entity.ToTable("userOrder");

                entity.HasIndex(e => e.UserId, "FK_userID");

                entity.Property(e => e.UserOrderId).HasColumnName("userOrderId");

                entity.Property(e => e.OrderDate).HasColumnName("orderDate");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserOrders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_userID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
