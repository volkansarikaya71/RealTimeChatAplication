﻿// <auto-generated />
using System;
using ChatApi.DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatApi.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20250119203837_mig_add_deletestatus_colum2")]
    partial class mig_add_deletestatus_colum2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("ChatApi.EntityLayer.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<string>("UserImage")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400)");

                    b.Property<DateTime?>("UserLastOnlineDate")
                        .HasColumnType("datetime");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.Property<bool?>("UserStatus")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Users", null, t =>
                        {
                            t.HasCheckConstraint("CK_Email_Format", "Email REGEXP '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,}$'");

                            t.HasCheckConstraint("CK_PhoneNumber_RakamsOnly", "PhoneNumber REGEXP '^[0-9]'");
                        });
                });

            modelBuilder.Entity("ChatApi.EntityLayer.UserFriendList", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("DeleteStatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("UserFriendId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserFriendId");

                    b.ToTable("userFriendLists");
                });

            modelBuilder.Entity("ChatApi.EntityLayer.UserFriendList", b =>
                {
                    b.HasOne("ChatApi.EntityLayer.User", "UserFriend")
                        .WithMany("UserFriend")
                        .HasForeignKey("UserFriendId")
                        .IsRequired();

                    b.Navigation("UserFriend");
                });

            modelBuilder.Entity("ChatApi.EntityLayer.User", b =>
                {
                    b.Navigation("UserFriend");
                });
#pragma warning restore 612, 618
        }
    }
}
