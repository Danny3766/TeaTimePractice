﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TeaTime.DataAccess.Data;

#nullable disable

namespace TeaTime.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TeaTime.Models.CategoryModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "果汁"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "茶"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "咖啡"
                        });
                });

            modelBuilder.Entity("TeaTime.Models.ProductModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "天然果飲，迷人多變，果香茶香，迷人香",
                            Name = "特調水果茶",
                            Price = 60.0,
                            Size = "大杯"
                        },
                        new
                        {
                            Id = 2,
                            Description = "品鐵觀音，享人生味道",
                            Name = "鐵觀音",
                            Price = 55.0,
                            Size = "中杯"
                        },
                        new
                        {
                            Id = 3,
                            Description = "用咖啡建構休閒時光",
                            Name = "美式咖啡",
                            Price = 45.0,
                            Size = "中杯"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
