using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using CoreIdentity.Data;
using NpgsqlTypes;
using CoreIdentity.Model.Entities;

namespace CoreIdentity.API.Migrations
{
    [DbContext(typeof(CoreIdentityContext))]
    partial class CoreIdentityContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("CoreIdentity.Model.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescription")
                        .HasMaxLength(550);

                    b.Property<string>("CategoryName")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<long?>("ParentId");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.OrderRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<NpgsqlPoint>("Destination");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<int>("OrderStatus");

                    b.Property<NpgsqlPoint>("Origin");

                    b.Property<long?>("UserId");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OrderRequest");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CategoriId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<bool>("Delete");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("ProductDescription")
                        .HasMaxLength(550);

                    b.Property<string>("ProductName")
                        .HasMaxLength(256);

                    b.Property<string>("SKU")
                        .HasMaxLength(100);

                    b.Property<decimal>("UnitPrice");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("Id");

                    b.HasIndex("CategoriId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.ProductAttribute", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<bool>("Delete");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("Name");

                    b.Property<long>("ProductId");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.Property<string>("value");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttribute");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("Description")
                        .HasMaxLength(256);

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("RoleName")
                        .HasMaxLength(150);

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("Email");

                    b.Property<string>("FullName")
                        .HasMaxLength(250);

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("Password")
                        .HasMaxLength(256);

                    b.Property<string>("Salt")
                        .HasMaxLength(256);

                    b.Property<string>("UserName")
                        .HasMaxLength(150);

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.UserInRole", b =>
                {
                    b.Property<long>("UserId");

                    b.Property<long>("RoleId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<bool>("isActive")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValue", true);

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInRole");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.Category", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.Category", "Parent")
                        .WithMany("SubCategory")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.OrderRequest", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.Product", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.Category", "Category")
                        .WithMany("Product")
                        .HasForeignKey("CategoriId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.ProductAttribute", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.Product", "Product")
                        .WithMany("Attribute")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.UserInRole", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.Role", "Role")
                        .WithMany("UserInRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CoreIdentity.Model.Entities.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
