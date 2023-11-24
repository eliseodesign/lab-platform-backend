// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;

// namespace LabPlatform.Models;

// public partial class ChatBotDbContext : DbContext
// {
//     public ChatBotDbContext()
//     {
//     }

//     public ChatBotDbContext(DbContextOptions<ChatBotDbContext> options)
//         : base(options)
//     {
//     }

//     public virtual DbSet<ClientUser> ClientUsers { get; set; }

//     public virtual DbSet<FeedBack> FeedBacks { get; set; }

//     public virtual DbSet<TypeUser> TypeUsers { get; set; }

//     protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//     {
        
//     }

//     protected override void OnModelCreating(ModelBuilder modelBuilder)
//     {
//         modelBuilder.Entity<ClientUser>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("PK__ClientUs__3214EC07627DAAB3");

//             entity.ToTable("ClientUser");

//             entity.HasIndex(e => e.Email, "UQ__ClientUs__A9D10534C8A10572").IsUnique();

//             entity.Property(e => e.Banned).HasComputedColumnSql("(case when [BadConduct]>(7) then (1) else (0) end)", false);
//             entity.Property(e => e.ConfirmAccount).HasDefaultValueSql("((0))");
//             entity.Property(e => e.Email)
//                 .HasMaxLength(100)
//                 .IsUnicode(false);
//             entity.Property(e => e.FirstName)
//                 .HasMaxLength(60)
//                 .IsUnicode(false);
//             entity.Property(e => e.LastName)
//                 .HasMaxLength(60)
//                 .IsUnicode(false);
//             entity.Property(e => e.Password)
//                 .HasMaxLength(255)
//                 .IsUnicode(false);
//             entity.Property(e => e.RestartAccount).HasDefaultValueSql("((0))");
//             entity.Property(e => e.Token)
//                 .HasMaxLength(6)
//                 .IsUnicode(false);
//             entity.Property(e => e.TypeUserId)
//                 .HasMaxLength(30)
//                 .IsUnicode(false);

//             entity.HasOne(d => d.TypeUser).WithMany(p => p.ClientUsers)
//                 .HasForeignKey(d => d.TypeUserId)
//                 .HasConstraintName("FK__ClientUse__TypeU__3C69FB99");
//         });

//         modelBuilder.Entity<FeedBack>(entity =>
//         {
//             entity.HasKey(e => e.Id).HasName("PK__FeedBack__3214EC075FD8F0AA");

//             entity.ToTable("FeedBack");

//             entity.Property(e => e.Message)
//                 .HasMaxLength(200)
//                 .IsUnicode(false);

//             entity.HasOne(d => d.ClientUser).WithMany(p => p.FeedBacks)
//                 .HasForeignKey(d => d.ClientUserId)
//                 .HasConstraintName("FK__FeedBack__Client__3F466844");
//         });

//         modelBuilder.Entity<TypeUser>(entity =>
//         {
//             entity.HasKey(e => e.TypeName).HasName("PK__TypeUser__D4E7DFA99DB99A9D");

//             entity.ToTable("TypeUser");

//             entity.Property(e => e.TypeName)
//                 .HasMaxLength(30)
//                 .IsUnicode(false);
//         });

//         OnModelCreatingPartial(modelBuilder);
//     }

//     partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
// }
