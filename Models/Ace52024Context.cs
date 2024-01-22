using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace flight.Models;

public partial class Ace52024Context : DbContext
{
    public Ace52024Context()
    {
    }

    public Ace52024Context(DbContextOptions<Ace52024Context> options)
        : base(options)
    {
    }

    public virtual DbSet<SanchitBooking> SanchitBookings { get; set; }

    public virtual DbSet<SanchitFlight> SanchitFlights { get; set; }

    public virtual DbSet<SanchitPassenger> SanchitPassengers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DEVSQL.Corp.local;Database=ACE 5- 2024;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SanchitBooking>(entity =>
        {
            entity.HasKey(e => e.Bookingid).HasName("PK__SanchitB__73961EC54BB15592");

            entity.ToTable("SanchitBooking");

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.Destination)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
        });

        modelBuilder.Entity<SanchitFlight>(entity =>
        {
            entity.HasKey(e => e.Flightid).HasName("PK__SanchitF__8A9900666335D21C");

            entity.Property(e => e.Flightid).ValueGeneratedNever();
            entity.Property(e => e.Destination)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Flightname)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SanchitPassenger>(entity =>
        {
            entity.HasKey(e => e.Cid).HasName("PK__SanchitP__C1FFD8613625ED93");

            entity.ToTable("SanchitPassenger");

            entity.Property(e => e.Cname)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Ltype)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ltype");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
