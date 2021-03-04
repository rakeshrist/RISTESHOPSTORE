﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

namespace WebAPI.Migrations
{
    [DbContext(typeof(StoreDataContext))]
    partial class StoreDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.tbl_category", b =>
                {
                    b.Property<Guid>("category_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("category_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("parent_category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("updated_by")
                        .HasColumnType("int");

                    b.Property<DateTime>("updated_on")
                        .HasColumnType("datetime2");

                    b.HasKey("category_id");

                    b.ToTable("tbl_categories");
                });

            modelBuilder.Entity("WebAPI.Models.tbl_user", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("access_token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("password_key")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("user_name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("tbl_users");
                });
#pragma warning restore 612, 618
        }
    }
}
