﻿// <auto-generated />
using System;
using DolphinFx.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DolphinFx.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240920052151_update")]
    partial class update
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("DolphinFx.Models.Application", b =>
                {
                    b.Property<int>("ApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ApplicationID"));

                    b.Property<string>("ApplicationDescription")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("ApplicationShortName")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.HasKey("ApplicationID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("DolphinFx.Models.ApplicationDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ApplicationID")
                        .HasColumnType("int");

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<int>("EnvironmentID")
                        .HasColumnType("int");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("User")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationID");

                    b.HasIndex("ClientID");

                    b.HasIndex("EnvironmentID");

                    b.HasIndex("UserId");

                    b.ToTable("ApplicationDetails");
                });

            modelBuilder.Entity("DolphinFx.Models.Client", b =>
                {
                    b.Property<int>("ClientID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("ClientID"));

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<long>("PrimaryContact")
                        .HasColumnType("bigint");

                    b.Property<string>("PrimaryEmailID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<long?>("SecondaryContact")
                        .HasColumnType("bigint");

                    b.Property<string>("SecondaryEmailID")
                        .HasColumnType("longtext");

                    b.HasKey("ClientID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("DolphinFx.Models.DatabaseDetail", b =>
                {
                    b.Property<int>("DbId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("DbId"));

                    b.Property<int>("ApplicationID")
                        .HasColumnType("int");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Datasource")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("EnvironmentID")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("DbId");

                    b.HasIndex("ApplicationID");

                    b.HasIndex("ClientId");

                    b.HasIndex("EnvironmentID");

                    b.ToTable("DatabaseDetails");
                });

            modelBuilder.Entity("DolphinFx.Models.Environment", b =>
                {
                    b.Property<int>("EnvironmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("EnvironmentID"));

                    b.Property<string>("EnvironmentDescription")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("EnvironmentName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("EnvironmentID");

                    b.ToTable("Environments");
                });

            modelBuilder.Entity("DolphinFx.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("TeamID"));

                    b.Property<int>("ClientID")
                        .HasColumnType("int");

                    b.Property<string>("TeamDescription")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("TeamID");

                    b.HasIndex("ClientID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("DolphinFx.Models.UserRole", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserID"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("RoleDescription")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("UserID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("DolphinFx.Models.ApplicationDetails", b =>
                {
                    b.HasOne("DolphinFx.Models.Application", "Applications")
                        .WithMany("ApplicationDetails")
                        .HasForeignKey("ApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DolphinFx.Models.Client", "Client")
                        .WithMany("ApplicationDetails")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DolphinFx.Models.Environment", "Environments")
                        .WithMany()
                        .HasForeignKey("EnvironmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DolphinFx.Models.UserRole", "UserRole")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Applications");

                    b.Navigation("Client");

                    b.Navigation("Environments");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("DolphinFx.Models.DatabaseDetail", b =>
                {
                    b.HasOne("DolphinFx.Models.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DolphinFx.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DolphinFx.Models.Environment", "Environments")
                        .WithMany()
                        .HasForeignKey("EnvironmentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Application");

                    b.Navigation("Client");

                    b.Navigation("Environments");
                });

            modelBuilder.Entity("DolphinFx.Models.Team", b =>
                {
                    b.HasOne("DolphinFx.Models.Client", "Client")
                        .WithMany("Teams")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("DolphinFx.Models.Application", b =>
                {
                    b.Navigation("ApplicationDetails");
                });

            modelBuilder.Entity("DolphinFx.Models.Client", b =>
                {
                    b.Navigation("ApplicationDetails");

                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
