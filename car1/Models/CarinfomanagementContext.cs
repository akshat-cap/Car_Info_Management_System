using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace car1.Models;

public partial class CarinfomanagementContext : DbContext
{
    public CarinfomanagementContext()
    {
    }

    public CarinfomanagementContext(DbContextOptions<CarinfomanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }

    public virtual DbSet<CarType> CarTypes { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LIN-DXGTJ64\\SQLEXPRESS;Database=carinfomanagement;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Car__3214EC07524D0C4F");

            entity.ToTable("Car");

            entity.HasIndex(e => e.Model, "UQ__Car__FB104C13DA7D6894").IsUnique();

            entity.Property(e => e.AirBagDetails)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Bhp).HasColumnName("BHP");
            entity.Property(e => e.Engine)
                .HasMaxLength(4)
                .IsUnicode(false);
            entity.Property(e => e.ManufacturerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Mileage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Transmission)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.CarTransmissionType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarTransmissionTypeId)
                .HasConstraintName("FK__Car__CarTransmis__36B12243");

            entity.HasOne(d => d.CarType).WithMany(p => p.Cars)
                .HasForeignKey(d => d.CarTypeId)
                .HasConstraintName("FK__Car__CarTypeId__35BCFE0A");
        });

        modelBuilder.Entity<CarTransmissionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarTrans__3214EC07977A8369");

            entity.ToTable("CarTransmissionType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CarType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarType__3214EC0790C6B5C8");

            entity.ToTable("CarType");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Type)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Manufact__3214EC076F79689A");

            entity.ToTable("Manufacturer");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ContactNo)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RegisteredOffice)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
