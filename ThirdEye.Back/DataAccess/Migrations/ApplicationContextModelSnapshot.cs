﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThirdEye.Back.DataAccess.Contexts;

#nullable disable

namespace ThirdEye.Back.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.0");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IdentityRole");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Device", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InstalationRoomId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("LastState")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InstalationRoomId");

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.InsitutionWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WorkerAccountId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.HasIndex("RoleId");

                    b.HasIndex("WorkerAccountId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Institution", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Institutions");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("InstitutionLocatedId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InstitutionLocatedId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.RoomStateChange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ChangeTime")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RoomChangedId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("State")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoomChangedId");

                    b.ToTable("RoomsStateHistories");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Device", b =>
                {
                    b.HasOne("ThirdEye.Back.DataAccess.Entities.Room", "InstalationRoom")
                        .WithMany("Devices")
                        .HasForeignKey("InstalationRoomId");

                    b.Navigation("InstalationRoom");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.InsitutionWorker", b =>
                {
                    b.HasOne("ThirdEye.Back.DataAccess.Entities.Institution", "Job")
                        .WithMany("Workers")
                        .HasForeignKey("JobId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("ThirdEye.Back.DataAccess.Entities.User", "WorkerAccount")
                        .WithMany("Works")
                        .HasForeignKey("WorkerAccountId");

                    b.Navigation("Job");

                    b.Navigation("Role");

                    b.Navigation("WorkerAccount");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Room", b =>
                {
                    b.HasOne("ThirdEye.Back.DataAccess.Entities.Institution", "InstitutionLocated")
                        .WithMany("Rooms")
                        .HasForeignKey("InstitutionLocatedId");

                    b.Navigation("InstitutionLocated");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.RoomStateChange", b =>
                {
                    b.HasOne("ThirdEye.Back.DataAccess.Entities.Room", "RoomChanged")
                        .WithMany("StateChanges")
                        .HasForeignKey("RoomChangedId");

                    b.Navigation("RoomChanged");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Institution", b =>
                {
                    b.Navigation("Rooms");

                    b.Navigation("Workers");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.Room", b =>
                {
                    b.Navigation("Devices");

                    b.Navigation("StateChanges");
                });

            modelBuilder.Entity("ThirdEye.Back.DataAccess.Entities.User", b =>
                {
                    b.Navigation("Works");
                });
#pragma warning restore 612, 618
        }
    }
}
