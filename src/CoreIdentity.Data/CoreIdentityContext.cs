using CoreIdentity.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreIdentity.Data
{
    public class CoreIdentityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<OrderRequest> OrderRequests { get; set; }

        public CoreIdentityContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e=> e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>().Property(e => e.UserName).HasMaxLength(150);
            modelBuilder.Entity<User>().Property(e => e.FullName).HasMaxLength(250);
            modelBuilder.Entity<User>().Property(e => e.Password).HasMaxLength(256);
            modelBuilder.Entity<User>().Property(e => e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<User>().Property(e => e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<User>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);

            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().Property(e => e.RoleName).HasMaxLength(150);
            modelBuilder.Entity<Role>().Property(e=> e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<Role>().Property(e=> e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<Role>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);

            modelBuilder.Entity<UserInRole>().ToTable("UserInRole");
            modelBuilder.Entity<UserInRole>().Property(e=> e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<UserInRole>().Property(e=> e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<UserInRole>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);
            modelBuilder.Entity<UserInRole>().HasOne(a => a.User)
                .WithMany(u => u.UserRole);
            modelBuilder.Entity<UserInRole>().HasOne(x => x.Role)
                .WithMany(r => r.UserInRole);
            modelBuilder.Entity<OrderRequest>().ToTable("OrderRequest");    
            modelBuilder.Entity<OrderRequest>().Property(e=> e.CreatedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<OrderRequest>().Property(e=> e.ModifiedDate).ForNpgsqlHasDefaultValueSql("current_timestamp");
            modelBuilder.Entity<OrderRequest>().Property(e => e.isActive).ForNpgsqlHasDefaultValue(true);
            modelBuilder.Entity<OrderRequest>().HasOne(a => a.User)
                .WithMany(u => u.Order);
            base.OnModelCreating(modelBuilder);
        }
    }
}
