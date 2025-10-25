using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL.Entities;

public partial class EVRentalDBContext : DbContext
{
    public EVRentalDBContext()
    {
    }

    public EVRentalDBContext(DbContextOptions<EVRentalDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rental> Rentals { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rental>(entity =>
        {
            entity.HasKey(e => e.RentalId).HasName("PK__Rentals__970059637FB6195C");

            entity.Property(e => e.RentalId).HasColumnName("RentalID");
            entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
            entity.Property(e => e.PickupStationId).HasColumnName("PickupStationID");
            entity.Property(e => e.RentalTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ReturnTime).HasColumnType("datetime");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.TotalCost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");

            entity.HasOne(d => d.Customer).WithMany(p => p.RentalCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__Custome__4222D4EF");

            entity.HasOne(d => d.PickupStation).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.PickupStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__PickupS__44FF419A");

            entity.HasOne(d => d.Staff).WithMany(p => p.RentalStaffs)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__StaffID__440B1D61");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.Rentals)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Rentals__Vehicle__4316F928");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("PK__Stations__E0D8A6DD85597F86");

            entity.Property(e => e.StationId).HasColumnName("StationID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.StationName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC1580C8F5");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534F6AAFC96").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(20);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.VehicleId).HasName("PK__Vehicles__476B54B273FDE0E0");

            entity.HasIndex(e => e.LicensePlate, "UQ__Vehicles__026BC15C6FE2121B").IsUnique();

            entity.Property(e => e.VehicleId).HasColumnName("VehicleID");
            entity.Property(e => e.CurrentStationId).HasColumnName("CurrentStationID");
            entity.Property(e => e.LicensePlate).HasMaxLength(20);
            entity.Property(e => e.Model).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.CurrentStation).WithMany(p => p.Vehicles)
                .HasForeignKey(d => d.CurrentStationId)
                .HasConstraintName("FK__Vehicles__Curren__3F466844");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
