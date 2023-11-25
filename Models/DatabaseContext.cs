using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LabPlatform.Models;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<FeedBack> FeedBacks { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Reaction> Reactions { get; set; }

    public virtual DbSet<SystemUser> SystemUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=ep-lingering-violet-29768740.us-east-2.aws.neon.fl0.io;Database=database;Username=fl0user;Password=FB3fK6uqsbGl");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Article_pkey");

            entity.ToTable("Article");

            entity.Property(e => e.ArticleState).HasMaxLength(20);
            entity.Property(e => e.ArticleType).HasMaxLength(20);
            entity.Property(e => e.CreateAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.Keywords).HasMaxLength(60);
            entity.Property(e => e.Title).HasMaxLength(100);

            entity.HasOne(d => d.AdminReview).WithMany(p => p.ArticleAdminReviews)
                .HasForeignKey(d => d.AdminReviewId)
                .HasConstraintName("Article_AdminReviewId_fkey");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.ArticleSystemUsers)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Article_SystemUserId_fkey");
        });

        modelBuilder.Entity<FeedBack>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FeedBack_pkey");

            entity.ToTable("FeedBack");

            entity.Property(e => e.Message).HasMaxLength(200);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Notification_pkey");

            entity.ToTable("Notification");

            entity.Property(e => e.CreateAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.IsRead).HasDefaultValueSql("false");
            entity.Property(e => e.Type).HasMaxLength(20);

            entity.HasOne(d => d.SystemUser).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.SystemUserId)
                .HasConstraintName("Notification_SystemUserId_fkey");
        });

        modelBuilder.Entity<Reaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Reaction_pkey");

            entity.ToTable("Reaction");

            entity.Property(e => e.Content).HasMaxLength(300);
            entity.Property(e => e.CreateAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.ParentId).HasDefaultValueSql("0");

            entity.HasOne(d => d.Article).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reaction_ArticleId_fkey");

            entity.HasOne(d => d.SystemUser).WithMany(p => p.Reactions)
                .HasForeignKey(d => d.SystemUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Reaction_SystemUserId_fkey");
        });

        modelBuilder.Entity<SystemUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SystemUser_pkey");

            entity.ToTable("SystemUser");

            entity.HasIndex(e => e.Email, "SystemUser_Email_key").IsUnique();

            entity.Property(e => e.Banned).HasComputedColumnSql("\nCASE\n    WHEN (\"BadConduct\" > 7) THEN true\n    ELSE false\nEND", true);
            entity.Property(e => e.Club).HasMaxLength(20);
            entity.Property(e => e.ConfirmAccount).HasDefaultValueSql("false");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Img).HasDefaultValueSql("'\\x5c78'::bytea");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.RestartAccount).HasDefaultValueSql("false");
            entity.Property(e => e.Rol).HasMaxLength(20);
            entity.Property(e => e.Token).HasMaxLength(6);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
