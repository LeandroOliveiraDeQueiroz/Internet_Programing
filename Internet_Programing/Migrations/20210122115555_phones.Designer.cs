﻿// <auto-generated />
using System;
using Internet_Programing.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Internet_Programing.Migrations
{
    [DbContext(typeof(ShoppingDbContext))]
    [Migration("20210122115555_phones")]
    partial class phones
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Internet_Programing.Models.CartPhone", b =>
                {
                    b.Property<int>("PhoneId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("PhoneId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("CartProduct");
                });

            modelBuilder.Entity("Internet_Programing.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Internet_Programing.Models.OS", b =>
                {
                    b.Property<int>("OSId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("OSId");

                    b.ToTable("OS");
                });

            modelBuilder.Entity("Internet_Programing.Models.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BatteryAmpere")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Memory")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<int>("OSId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("varbinary(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Processor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RAM")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OSId");

                    b.ToTable("Phone");
                });

            modelBuilder.Entity("Internet_Programing.Models.CartPhone", b =>
                {
                    b.HasOne("Internet_Programing.Models.Customer", "Customer")
                        .WithMany("Cart")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Internet_Programing.Models.Phone", "Phone")
                        .WithMany("Cart")
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Phone");
                });

            modelBuilder.Entity("Internet_Programing.Models.Phone", b =>
                {
                    b.HasOne("Internet_Programing.Models.OS", "OS")
                        .WithMany()
                        .HasForeignKey("OSId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OS");
                });

            modelBuilder.Entity("Internet_Programing.Models.Customer", b =>
                {
                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Internet_Programing.Models.Phone", b =>
                {
                    b.Navigation("Cart");
                });
#pragma warning restore 612, 618
        }
    }
}
