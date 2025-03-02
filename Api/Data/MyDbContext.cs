﻿using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                // get múi giờ Getutcdate
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("Getutcdate()");
                e.Property(dh => dh.NguoiNhan).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<DonHangChiTiet>(entity => {
                entity.ToTable("ChiTietDonHang");
                // Set 2 PK
                entity.HasKey(e => new { e.MaDh, e.MaHh });

                entity.HasOne(e => e.DonHang)
                       .WithMany(e => e.DonHangChiTiets)
                       .HasForeignKey(e => e.MaDh)
                       .HasConstraintName("FK_DonHangCT_DonHang");
                entity.HasOne(e => e.HangHoa)
                       .WithMany(e => e.DonHangChiTiets)
                       .HasForeignKey(e => e.MaDh)
                       .HasConstraintName("FK_DonHangCT_HangHoa");

            });
            modelBuilder.Entity<NguoiDung>(entity => {
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            });
        }
    }
}
