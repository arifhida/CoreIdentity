﻿using System;
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
    [Migration("20170308012113_PGDB")]
    partial class PGDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("CoreIdentity.Model.Entities.OrderRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<NpgsqlPoint>("Destination");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<int>("OrderStatus");

                    b.Property<NpgsqlPoint>("Origin");

                    b.Property<long?>("UserId");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("OrderRequest");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("RoleName")
                        .HasMaxLength(150);

                    b.Property<bool>("isActive");

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<string>("Password")
                        .HasMaxLength(256);

                    b.Property<string>("UserName")
                        .HasMaxLength(150);

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.UserInRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<DateTime>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:DefaultValueSql", "current_timestamp");

                    b.Property<long?>("RoleId");

                    b.Property<long?>("UserId");

                    b.Property<bool>("isActive");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInRole");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.OrderRequest", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("CoreIdentity.Model.Entities.UserInRole", b =>
                {
                    b.HasOne("CoreIdentity.Model.Entities.Role", "Role")
                        .WithMany("RoledUser")
                        .HasForeignKey("RoleId");

                    b.HasOne("CoreIdentity.Model.Entities.User", "User")
                        .WithMany("UserRole")
                        .HasForeignKey("UserId");
                });
        }
    }
}
