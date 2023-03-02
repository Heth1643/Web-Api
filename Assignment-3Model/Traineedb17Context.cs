using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment_3Model;

public partial class Traineedb17Context : DbContext
{
    public Traineedb17Context()
    {
    }

    public Traineedb17Context(DbContextOptions<Traineedb17Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Register> Registers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=WIN-390Q81UT43Q\\SQLEXPRESS2017;Database=traineedb17;User Id=traineedb17;Password=hlmjXcsdRG7aPW63;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Register>(entity =>
        {
            entity.HasKey(e => e.Recid).HasName("PK__Register__36F91897739B23A4");

            entity.ToTable("Register");

            entity.Property(e => e.Cno)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dateadded)
                .HasColumnType("datetime")
                .HasColumnName("dateadded");
            entity.Property(e => e.Dob).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Endeffdt)
                .HasColumnType("date")
                .HasColumnName("endeffdt");
            entity.Property(e => e.Fname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Lastupdate)
                .HasColumnType("datetime")
                .HasColumnName("lastupdate");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
        modelBuilder.HasSequence<int>("s1")
            .StartsAt(10L)
            .HasMin(1L)
            .HasMax(20L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
