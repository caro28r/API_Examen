using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Examen.Models;

public partial class AnswersDbContext : DbContext
{
    public AnswersDbContext()
    {
    }

    public AnswersDbContext(DbContextOptions<AnswersDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answer> Answers { get; set; }

    public virtual DbSet<Ask> Asks { get; set; }

    public virtual DbSet<AskStatus> AskStatuses { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<General> Generals { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=PC-CARO\\MSSQLSERVER07;Database=AnswersDB;Integrated Security=False;User Id=AnswersDB;Password=123;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>(entity =>
        {
            entity.HasKey(e => e.AnswerId).HasName("PK__Answer__D4825024F67106B6");

            entity.ToTable("Answer");

            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.Answer1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("Answer");
            entity.Property(e => e.AskId).HasColumnName("AskID");
            entity.Property(e => e.Date).HasColumnType("smalldatetime");
            entity.Property(e => e.IsStrike)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.SetAsCorrect)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Ask).WithMany(p => p.Answers)
                .HasForeignKey(d => d.AskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAnswer213900");

            entity.HasOne(d => d.User).WithMany(p => p.Answers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAnswer657386");
        });

        modelBuilder.Entity<Ask>(entity =>
        {
            entity.HasKey(e => e.AskId).HasName("PK__Ask__3D29B234D0461A6A");

            entity.ToTable("Ask");

            entity.Property(e => e.AskId).HasColumnName("AskID");
            entity.Property(e => e.Ask1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("Ask");
            entity.Property(e => e.AskDetail).IsUnicode(false);
            entity.Property(e => e.AskStatusId).HasColumnName("AskStatusID");
            entity.Property(e => e.Date).HasColumnType("smalldatetime");
            entity.Property(e => e.ImageUrl)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsStrike)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.AskStatus).WithMany(p => p.Asks)
                .HasForeignKey(d => d.AskStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAsk68590");

            entity.HasOne(d => d.User).WithMany(p => p.Asks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKAsk352681");
        });

        modelBuilder.Entity<AskStatus>(entity =>
        {
            entity.HasKey(e => e.AskStatusId).HasName("PK__AskStatu__0329362C939A6436");

            entity.ToTable("AskStatus");

            entity.Property(e => e.AskStatusId).HasColumnName("AskStatusID");
            entity.Property(e => e.AskStatus1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("AskStatus");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__Chat__A9FBE6267CBD4783");

            entity.ToTable("Chat");

            entity.Property(e => e.ChatId).HasColumnName("ChatID");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("smalldatetime");
            entity.Property(e => e.Message)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");
            entity.Property(e => e.SenderId).HasColumnName("SenderID");

            entity.HasOne(d => d.Receiver).WithMany(p => p.ChatReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKChat316186");

            entity.HasOne(d => d.Sender).WithMany(p => p.ChatSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKChat811353");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__Country__10D160BF4FE2F8DC");

            entity.ToTable("Country");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<General>(entity =>
        {
            entity.HasKey(e => e.Idconfig).HasName("PK__General__3E2AEF2AD094CA8C");

            entity.ToTable("General");

            entity.Property(e => e.Idconfig).HasColumnName("IDConfig");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.LikeId).HasName("PK__Like__A2922CF467C37EDA");

            entity.ToTable("Like");

            entity.Property(e => e.LikeId).HasColumnName("LikeID");
            entity.Property(e => e.AnswerId).HasColumnName("AnswerID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Answer).WithMany(p => p.Likes)
                .HasForeignKey(d => d.AnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKLike563281");

            entity.HasOne(d => d.User).WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKLike654985");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCACF3D2E750");

            entity.ToTable("User");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.BackUpEmail)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.JobDescription)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");

            entity.HasOne(d => d.Country).WithMany(p => p.Users)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser435567");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser854768");

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKUser472287");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A552AF191B8");

            entity.ToTable("UserRole");

            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.UserRole1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UserRole");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.UserStatusId).HasName("PK__UserStat__A33F541A73839787");

            entity.ToTable("UserStatus");

            entity.Property(e => e.UserStatusId).HasColumnName("UserStatusID");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
