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
    [Migration("20230509061745_HR_ZktFinger_unilever")]
    partial class HR_ZktFinger_unilever
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ZKTeco.Models.Blocklist", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<DateTime?>("blockdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("comId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isBlock")
                        .HasColumnType("bit");

                    b.Property<string>("remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("unblockdate")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.ToTable("Blocklist");
                });

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

                    b.Property<string>("empCode")
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

            modelBuilder.Entity("ZKTeco.Models.HR_ZktFinger_unilever", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DeviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("cardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createdAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("empCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("empName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex10")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex6")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex8")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("fingerindex9")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isDelete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("updatedAt")
                        .HasColumnType("datetime2");

                    b.Property<long?>("userGroup")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.ToTable("HR_ZktFinger_unilever");
                });

            modelBuilder.Entity("ZKTeco.Models.RTevevnt", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"), 1L, 1);

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("empCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("remark")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("verifystyle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("RTevevnt");
                });
#pragma warning restore 612, 618
        }
    }
}
