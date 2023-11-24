using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabPlatform.Models;

public partial class AuthdbContext : DbContext
{
    public AuthdbContext()
    {
    }

    public AuthdbContext(DbContextOptions<AuthdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Clientuser> Clientusers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Typeuser> Typeusers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Clientuser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clientuser_pkey");

            entity.ToTable("clientuser");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Badconduct).HasColumnName("badconduct");
            entity.Property(e => e.Banned)
                .HasComputedColumnSql("\nCASE\n    WHEN (badconduct > 7) THEN true\n    ELSE false\nEND", true)
                .HasColumnName("banned");
            entity.Property(e => e.Confirmaccount)
                .HasDefaultValueSql("false")
                .HasColumnName("confirmaccount");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(60)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(60)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Restartaccount)
                .HasDefaultValueSql("false")
                .HasColumnName("restartaccount");
            entity.Property(e => e.Token)
                .HasMaxLength(6)
                .HasColumnName("token");
            entity.Property(e => e.Typeuserid)
                .HasMaxLength(30)
                .HasColumnName("typeuserid");

            entity.HasOne(d => d.Typeuser).WithMany(p => p.Clientusers)
                .HasForeignKey(d => d.Typeuserid)
                .HasConstraintName("clientuser_typeuserid_fkey");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Clientuserid).HasColumnName("clientuserid");
            entity.Property(e => e.Message)
                .HasMaxLength(200)
                .HasColumnName("message");

            entity.HasOne(d => d.Clientuser).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.Clientuserid)
                .HasConstraintName("feedback_clientuserid_fkey");
        });

        modelBuilder.Entity<Typeuser>(entity =>
        {
            entity.HasKey(e => e.Typename).HasName("typeuser_pkey");

            entity.ToTable("typeuser");

            entity.Property(e => e.Typename)
                .HasMaxLength(30)
                .HasColumnName("typename");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
