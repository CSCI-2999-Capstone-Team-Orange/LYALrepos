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
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Coffee> Coffees { get; set; }
        public virtual DbSet<Drink> Drinks { get; set; }
        public virtual DbSet<EfmigrationsHistory> EfmigrationsHistories { get; set; }
        public virtual DbSet<LogTime> LogTimes { get; set; }
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<ProductTable> ProductTables { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<Size1> Size1s { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("Server= aa124gktif3j980.cjiyeakoxxft.us-east-1.rds.amazonaws.com;port=3306;user=test;password=orange1234;database=loveyoualattedb");
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

                entity.Property(e => e.Email).HasMaxLength(256);

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

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.HasIndex(e => e.DrinkName, "DrinkName_idx");

                entity.HasIndex(e => e.Price, "Price_idx");

                entity.HasIndex(e => e.IdProduct, "ProductID_idx");

                entity.HasIndex(e => e.SizeName, "SizeName_idx");

                entity.HasIndex(e => e.IdUser, "UserID_idx");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.DrinkName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdProduct).HasColumnName("id_Product");

                entity.Property(e => e.IdUser).HasColumnName("id_User");

                entity.Property(e => e.Price).HasColumnType("decimal(5,2)");

                entity.Property(e => e.SizeName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdProduct)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductID");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserID");
            });

            modelBuilder.Entity<Coffee>(entity =>
            {
                entity.HasKey(e => e.IdCoffee)
                    .HasName("PRIMARY");

                entity.ToTable("Coffee");

                entity.Property(e => e.IdCoffee).HasColumnName("id_Coffee");

                entity.Property(e => e.CoffeeName)
                    .IsRequired()
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.HasIndex(e => e.Name, "Name_idx");

                entity.Property(e => e.DrinkId).HasColumnName("DrinkID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
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

            modelBuilder.Entity<LogTime>(entity =>
            {
                entity.ToTable("log_time");

                entity.Property(e => e.LogTimeId)
                    .HasColumnType("int unsigned")
                    .HasColumnName("log_time_id");

                entity.Property(e => e.LogTime1).HasColumnName("log_time");
            });

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PRIMARY");

                entity.ToTable("Order_History");

                entity.Property(e => e.OrderId).HasColumnName("Order_ID");

                entity.Property(e => e.CartId).HasColumnName("Cart_ID");

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Item_name");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserId).HasColumnName("User_ID");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.HasKey(e => e.IdPrice)
                    .HasName("PRIMARY");

                entity.ToTable("Price");

                entity.Property(e => e.IdPrice).HasColumnName("id_Price");

                entity.Property(e => e.Price1).HasColumnName("Price");
            });

            modelBuilder.Entity<ProductTable>(entity =>
            {
                entity.ToTable("product_table");

                entity.HasIndex(e => e.DrinkId, "DrinkID_idx");

                entity.HasIndex(e => e.Price, "Price_idx");

                entity.HasIndex(e => e.SizeId, "SizeID_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DrinkId).HasColumnName("DrinkID");

                entity.Property(e => e.Price).HasColumnType("decimal(5,2)");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.Drink)
                    .WithMany(p => p.ProductTables)
                    .HasForeignKey(d => d.DrinkId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DrinkID");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductTables)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SizeID");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.HasIndex(e => e.Description, "description_idx");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Size1>(entity =>
            {
                entity.HasKey(e => e.IdSize)
                    .HasName("PRIMARY");

                entity.ToTable("Size.1");

                entity.HasComment("Item size");

                entity.Property(e => e.IdSize).HasColumnName("id.Size");

                entity.Property(e => e.Size)
                    .IsRequired()
                    .HasMaxLength(6);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("id_User");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(12);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(45);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
