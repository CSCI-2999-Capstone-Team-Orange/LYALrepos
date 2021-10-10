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
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
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

            modelBuilder.Entity<CartTable>(entity =>
            {
                entity.HasKey(e => new { e.IdCartTable, e.IdProduct, e.IdUser, e.UserIdUser, e.ProductIdProduct, e.ProductIdDrinks, e.ProductIdSize, e.ProductSizeIdSize, e.ProductDrinksIdDrinks })
                    .HasName("PRIMARY");

                entity.ToTable("CartTable");

                entity.HasIndex(e => new { e.ProductIdProduct, e.ProductIdDrinks, e.ProductIdSize, e.ProductSizeIdSize, e.ProductDrinksIdDrinks }, "fk_CartTable_Product1_idx");

                entity.HasIndex(e => e.UserIdUser, "fk_CartTable_User1_idx");

                entity.Property(e => e.IdCartTable).HasColumnName("idCartTable");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

                entity.Property(e => e.ProductIdProduct).HasColumnName("Product_idProduct");

                entity.Property(e => e.ProductIdDrinks).HasColumnName("Product_idDrinks");

                entity.Property(e => e.ProductIdSize).HasColumnName("Product_idSize");

                entity.Property(e => e.ProductSizeIdSize).HasColumnName("Product_Size_idSize");

                entity.Property(e => e.ProductDrinksIdDrinks).HasColumnName("Product_Drinks_idDrinks");

                entity.Property(e => e.Purchased).HasColumnType("tinyint");

                entity.Property(e => e.TotalCost).HasColumnName("Total Cost");

                entity.HasOne(d => d.UserIdUserNavigation)
                    .WithMany(p => p.CartTables)
                    .HasForeignKey(d => d.UserIdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CartTable_User1");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CartTables)
                    .HasForeignKey(d => new { d.ProductIdProduct, d.ProductIdDrinks, d.ProductIdSize, d.ProductSizeIdSize, d.ProductDrinksIdDrinks })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CartTable_Product1");
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.HasKey(e => e.IdDrinks)
                    .HasName("PRIMARY");

                entity.Property(e => e.IdDrinks).HasColumnName("idDrinks");

                entity.Property(e => e.CoffeeName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Coffee Name");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(45);
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

            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => new { e.IdOrderHistory, e.IdCartTable, e.IdUser, e.CartTableIdCartTable, e.CartTableIdProduct, e.CartTableIdUser })
                    .HasName("PRIMARY");

                entity.ToTable("Order History");

                entity.HasIndex(e => new { e.CartTableIdCartTable, e.CartTableIdProduct, e.CartTableIdUser }, "fk_Order History_CartTable1_idx");

                entity.Property(e => e.IdOrderHistory).HasColumnName("idOrder History");

                entity.Property(e => e.IdCartTable).HasColumnName("idCartTable");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.CartTableIdCartTable).HasColumnName("CartTable_idCartTable");

                entity.Property(e => e.CartTableIdProduct).HasColumnName("CartTable_idProduct");

                entity.Property(e => e.CartTableIdUser).HasColumnName("CartTable_idUser");

                entity.Property(e => e.Purchased).HasColumnType("tinyint");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.IdProduct, e.IdDrinks, e.IdSize, e.SizeIdSize, e.DrinksIdDrinks })
                    .HasName("PRIMARY");

                entity.ToTable("Product");

                entity.HasIndex(e => e.DrinksIdDrinks, "fk_Product_Drinks1_idx");

                entity.HasIndex(e => e.SizeIdSize, "fk_Product_Size1_idx");

                entity.Property(e => e.IdProduct).HasColumnName("idProduct");

                entity.Property(e => e.IdDrinks).HasColumnName("idDrinks");

                entity.Property(e => e.IdSize).HasColumnName("idSize");

                entity.Property(e => e.SizeIdSize).HasColumnName("Size_idSize");

                entity.Property(e => e.DrinksIdDrinks).HasColumnName("Drinks_idDrinks");

                entity.HasOne(d => d.DrinksIdDrinksNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.DrinksIdDrinks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Drinks1");

                entity.HasOne(d => d.SizeIdSizeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SizeIdSize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Product_Size1");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.IdSize)
                    .HasName("PRIMARY");

                entity.ToTable("Size");

                entity.Property(e => e.IdSize).HasColumnName("idSize");

                entity.Property(e => e.Size1).HasColumnName("Size");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PRIMARY");

                entity.ToTable("User");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("User Email");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("User Name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
