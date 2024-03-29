﻿// <auto-generated />
using System;
using MagicVilla.MagicApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MagicVilla.MagicApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231225063928_LocalUsers")]
    partial class LocalUsers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla.MagicApi.Model.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenities")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenities = "Swimming pool, jacuzzi, sauna, gym",
                            CreatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(454),
                            Details = "This is a beautiful villa with stunning views.",
                            ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
                            Name = "Villa 1",
                            Occupancy = 4,
                            Rate = 1000.0,
                            Sqft = 2000,
                            UpdatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(450)
                        },
                        new
                        {
                            Id = 2,
                            Amenities = "Garden, barbecue, fireplace",
                            CreatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(458),
                            Details = "This is a cozy villa with a private garden.",
                            ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
                            Name = "Villa 2",
                            Occupancy = 2,
                            Rate = 800.0,
                            Sqft = 1500,
                            UpdatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(456)
                        },
                        new
                        {
                            Id = 3,
                            Amenities = "Rooftop terrace, city views, outdoor kitchen",
                            CreatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(461),
                            Details = "This is a modern villa with a rooftop terrace.",
                            ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
                            Name = "Villa 3",
                            Occupancy = 6,
                            Rate = 1200.0,
                            Sqft = 2500,
                            UpdatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(459)
                        },
                        new
                        {
                            Id = 4,
                            Amenities = "Private pool, ocean views, butler service",
                            CreatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(463),
                            Details = "This is a luxurious villa with a private pool.",
                            ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
                            Name = "Villa 4",
                            Occupancy = 8,
                            Rate = 1500.0,
                            Sqft = 3000,
                            UpdatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(462)
                        },
                        new
                        {
                            Id = 5,
                            Amenities = "Fireplace, wood-burning oven, hammock",
                            CreatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(466),
                            Details = "This is a charming villa with a rustic feel.",
                            ImageURL = "https://images.unsplash.com/photo-1580587771525-78b9dba3b914?q=80&w=1374&auto=format&fit=crop",
                            Name = "Villa 5",
                            Occupancy = 3,
                            Rate = 700.0,
                            Sqft = 1200,
                            UpdatedDate = new DateTime(2023, 12, 25, 11, 39, 28, 699, DateTimeKind.Local).AddTicks(465)
                        });
                });

            modelBuilder.Entity("MagicVilla.MagicApi.Model.VillaNumber", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VillaId")
                        .HasColumnType("int");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("VillaNumbers");
                });

            modelBuilder.Entity("MagicVilla.MagicApi.Models.LocalUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LocalUsers");
                });

            modelBuilder.Entity("MagicVilla.MagicApi.Model.VillaNumber", b =>
                {
                    b.HasOne("MagicVilla.MagicApi.Model.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
