﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZKTeco.Models;

#nullable disable

namespace ZKTeco.Migrations
{
    [DbContext(typeof(ZKTDbContext))]
    [Migration("20221103035900_usergroup")]
    partial class usergroup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ZKTeco.Models.HR_Emp_DeviceInfoHIK", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("empImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("fingerData")
                        .HasColumnType("varbinary(max)");

                    b.Property<long?>("userGroup")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("HR_Emp_DeviceInfoHIK");
                });

            modelBuilder.Entity("ZKTeco.Models.HR_MachineNoZKT", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ComId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Inout")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IsActive")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short?>("LuserId")
                        .HasColumnType("smallint");

                    b.Property<string>("Pcname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PortNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZKtPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZKtUser")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("HR_MachineNoZkt");
                });
#pragma warning restore 612, 618
        }
    }
}
