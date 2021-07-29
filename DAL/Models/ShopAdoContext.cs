using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ShopWebApi.DAL.Models
{
    public partial class ShopAdoContext : DbContext
    {
        public ShopAdoContext()
        {
        }

        public ShopAdoContext(DbContextOptions<ShopAdoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<SalePo> SalePos { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Initial Catalog=ShopAdo;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.ToTable("Good");

                entity.Property(e => e.GoodCount).HasColumnType("numeric(18, 3)");

                entity.Property(e => e.GoodName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Good__CategoryId__29572725");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.ManufacturerId)
                    .HasConstraintName("FK__Good__Manufactur__286302EC");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturer");

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.ToTable("Photo");

                entity.Property(e => e.PhotoPath)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.DateSale).HasColumnType("date");

                entity.Property(e => e.Summa).HasColumnType("money");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('0')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SalePo>(entity =>
            {
                entity.HasKey(e => e.SalePosId)
                    .HasName("PK__SalePos__07E2A923BF98B336");

                entity.Property(e => e.Summa).HasColumnType("money");

                entity.HasOne(d => d.Good)
                    .WithMany(p => p.SalePos)
                    .HasForeignKey(d => d.GoodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalePos__GoodId__32E0915F");

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.SalePos)
                    .HasForeignKey(d => d.SaleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalePos__SaleId__31EC6D26");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.PasswordHash).IsRequired();

                entity.Property(e => e.UserLogin)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__4BAC3F29");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__UserRole__8AFACE1A05A38541");

                entity.ToTable("UserRole");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
