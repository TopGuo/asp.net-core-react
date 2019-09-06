using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace domain.entitys
{
    public partial class baixiaosheng_1Context : DbContext
    {
        public baixiaosheng_1Context()
        {
        }

        public baixiaosheng_1Context(DbContextOptions<baixiaosheng_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminActions> AdminActions { get; set; }
        public virtual DbSet<AdminRoleAction> AdminRoleAction { get; set; }
        public virtual DbSet<AdminRoles> AdminRoles { get; set; }
        public virtual DbSet<AdminUsers> AdminUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminActions>(entity =>
            {
                entity.ToTable("admin_actions");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionName)
                    .HasColumnName("action_name")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Enable)
                    .HasColumnName("enable")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<AdminRoleAction>(entity =>
            {
                entity.ToTable("admin_role_action");

                entity.HasIndex(e => new { e.RoleId, e.ActionId })
                    .HasName("rright")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionId)
                    .HasColumnName("action_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<AdminRoles>(entity =>
            {
                entity.ToTable("admin_roles");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Info)
                    .HasColumnName("info")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("is_deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasColumnType("varchar(50)");
            });

            modelBuilder.Entity<AdminUsers>(entity =>
            {
                entity.ToTable("admin_users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("create_time")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasColumnType("varchar(30)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(32)");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasColumnType("varchar(11)");
            });
        }
    }
}
