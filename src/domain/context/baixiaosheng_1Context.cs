using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace domain.entitys
{
    public partial class baixiaosheng_1Context : DbContext
    {
        public baixiaosheng_1Context(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public baixiaosheng_1Context(DbContextOptions<baixiaosheng_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<AdminActions> AdminActions { get; set; }
        public virtual DbSet<AdminRoleAction> AdminRoleAction { get; set; }
        public virtual DbSet<AdminRoles> AdminRoles { get; set; }
        public virtual DbSet<AdminUsers> AdminUsers { get; set; }
        public virtual DbSet<Announce> Announce { get; set; }
        public virtual DbSet<BackstageUser> BackstageUser { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<MessageInfo> MessageInfo { get; set; }
        public virtual DbSet<MessageType> MessageType { get; set; }
        public virtual DbSet<Scenic> Scenic { get; set; }
        public virtual DbSet<Shop> Shop { get; set; }
        public virtual DbSet<ShopsDetail> ShopsDetail { get; set; }
        public virtual DbSet<SystemActions> SystemActions { get; set; }
        public virtual DbSet<SystemRolePermission> SystemRolePermission { get; set; }
        public virtual DbSet<SystemRoles> SystemRoles { get; set; }
        public virtual DbSet<TaskSchedule> TaskSchedule { get; set; }
        public virtual DbSet<User> User { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = Configuration.GetConnectionString("baixiaosheng1");
                optionsBuilder.UseMySql(conStr,
                mySqlOptions =>
                     {
                         mySqlOptions.ServerVersion(new Version(5, 7, 24), ServerType.MySql); // replace with your Server Version and Type
                     });
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

            modelBuilder.Entity<Announce>(entity =>
            {
                entity.ToTable("announce");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsDel)
                    .HasColumnName("isDel")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(32)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Types)
                    .HasColumnName("types")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<BackstageUser>(entity =>
            {
                entity.ToTable("backstage_user");

                entity.HasIndex(e => e.RoleId)
                    .HasName("rId");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.AccountStatus)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.AccountType)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.FullName)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Gender)
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IdCard)
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastLoginIp)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.LoginName)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("'123456'");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.SourceType)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.BackstageUser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rId");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.ToTable("banner");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsDel)
                    .HasColumnName("isDel")
                    .HasColumnType("int(1)");

                entity.Property(e => e.JumpUrl)
                    .HasColumnName("jumpUrl")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Pic)
                    .HasColumnName("pic")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Types)
                    .HasColumnName("types")
                    .HasColumnType("int(2)");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<MessageInfo>(entity =>
            {
                entity.ToTable("messageInfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsDel)
                    .HasColumnName("isDel")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.LookCount)
                    .HasColumnName("lookCount")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pics)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Types)
                    .HasColumnName("types")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<MessageType>(entity =>
            {
                entity.ToTable("messageType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsDel)
                    .HasColumnName("isDel")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasColumnName("pic")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Types)
                    .HasColumnName("types")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<Scenic>(entity =>
            {
                entity.ToTable("scenic");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.LTitle)
                    .IsRequired()
                    .HasColumnName("lTitle")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.LookCount)
                    .IsRequired()
                    .HasColumnName("lookCount")
                    .HasColumnType("varchar(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mark1)
                    .IsRequired()
                    .HasColumnName("mark1")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Mark2)
                    .IsRequired()
                    .HasColumnName("mark2")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasColumnName("pic")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(200)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("shop");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BeginTime)
                    .HasColumnName("beginTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CloseTime)
                    .HasColumnName("closeTime")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'18'");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.EndTime)
                    .HasColumnName("endTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Latitude)
                    .HasColumnName("latitude")
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.LogoPic)
                    .HasColumnName("logoPic")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Longitude)
                    .HasColumnName("longitude")
                    .HasColumnType("decimal(10,4)");

                entity.Property(e => e.LookCount)
                    .HasColumnName("lookCount")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.OpenTime)
                    .HasColumnName("openTime")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'10'");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasColumnName("phoneNum")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("'暂无'");

                entity.Property(e => e.ShopType)
                    .HasColumnName("shopType")
                    .HasColumnType("int(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Types)
                    .HasColumnName("types")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UserId)
                    .HasColumnName("userId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<ShopsDetail>(entity =>
            {
                entity.ToTable("shopsDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasColumnName("content")
                    .HasColumnType("text");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.IsDel)
                    .HasColumnName("isDel")
                    .HasColumnType("int(2)");

                entity.Property(e => e.Pic)
                    .HasColumnName("pic")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ShopId)
                    .HasColumnName("shopId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<SystemActions>(entity =>
            {
                entity.HasKey(e => e.ActionId)
                    .HasName("PRIMARY");

                entity.ToTable("system_actions");

                entity.Property(e => e.ActionId)
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ActionDescription)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.ActionName)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Orders).HasColumnType("int(11)");

                entity.Property(e => e.ParentAction).HasColumnType("varchar(255)");
                entity.Property(e => e.Icon).HasColumnType("varchar(100)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnType("varchar(255)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<SystemRolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.ActionId })
                    .HasName("PRIMARY");

                entity.ToTable("system_role_permission");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.ActionId).HasColumnType("varchar(36)");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<SystemRoles>(entity =>
            {
                entity.ToTable("system_roles");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<TaskSchedule>(entity =>
            {
                entity.ToTable("task_schedule");

                entity.Property(e => e.Id)
                    .HasColumnType("varchar(36)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreateAuthr)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.CronExpress).HasColumnType("varchar(50)");

                entity.Property(e => e.EndRunTime).HasColumnType("datetime");

                entity.Property(e => e.JobGroup)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.JobName)
                    .IsRequired()
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.NextRunTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasColumnType("text");

                entity.Property(e => e.RunStatus)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.StarRunTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.CreateTime)
                    .HasColumnName("createTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.NickName)
                    .IsRequired()
                    .HasColumnName("nickName")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasColumnName("passWord")
                    .HasColumnType("varchar(300)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PhoneNum)
                    .IsRequired()
                    .HasColumnName("phoneNum")
                    .HasColumnType("varchar(20)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Pic)
                    .IsRequired()
                    .HasColumnName("pic")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.RefId)
                    .HasColumnName("refId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(2)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasColumnType("varchar(300)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Uid)
                    .IsRequired()
                    .HasColumnName("uid")
                    .HasColumnType("varchar(100)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.UpdateTime)
                    .HasColumnName("updateTime")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });
        }
    }
}
