﻿// <auto-generated />
using System;
using Final.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomeZillaBackend.Migrations
{
    [DbContext(typeof(HomezillaContext))]
    [Migration("20230130023301_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CustomerUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserID")
                        .IsUnique();

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Final.Entities.Authentication", b =>
                {
                    b.Property<Guid>("AuthId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("bit");

                    b.Property<int?>("OTP")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OTPExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("PasswordResetAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserRole")
                        .HasColumnType("int");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("AuthId");

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("Final.Entities.OrderDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppointmentFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AppointmentTo")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Cost")
                        .HasColumnType("int");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ServiceName")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProviderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Final.Entities.Provider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Location")
                        .HasColumnType("int");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProviderUserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProviderUserID")
                        .IsUnique();

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("Final.Entities.ProviderServices", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<Guid>("ProviderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProviderId");

                    b.ToTable("ProviderServices");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.HasOne("Final.Entities.Authentication", "customer")
                        .WithOne("Customer")
                        .HasForeignKey("Customer", "CustomerUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("customer");
                });

            modelBuilder.Entity("Final.Entities.OrderDetails", b =>
                {
                    b.HasOne("Customer", "customer")
                        .WithMany("OrderDeatils")
                        .HasForeignKey("CustomerId");

                    b.HasOne("Final.Entities.Provider", "provider")
                        .WithMany("OrderDeatils")
                        .HasForeignKey("ProviderId");

                    b.Navigation("customer");

                    b.Navigation("provider");
                });

            modelBuilder.Entity("Final.Entities.Provider", b =>
                {
                    b.HasOne("Final.Entities.Authentication", "provider")
                        .WithOne("Provider")
                        .HasForeignKey("Final.Entities.Provider", "ProviderUserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("provider");
                });

            modelBuilder.Entity("Final.Entities.ProviderServices", b =>
                {
                    b.HasOne("Final.Entities.Provider", "Provider")
                        .WithMany("Service")
                        .HasForeignKey("ProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Customer", b =>
                {
                    b.Navigation("OrderDeatils");
                });

            modelBuilder.Entity("Final.Entities.Authentication", b =>
                {
                    b.Navigation("Customer");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("Final.Entities.Provider", b =>
                {
                    b.Navigation("OrderDeatils");

                    b.Navigation("Service");
                });
#pragma warning restore 612, 618
        }
    }
}
