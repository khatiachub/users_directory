﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using users_directory.DB;

#nullable disable

namespace users_directory.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250320130855_NewMigration")]
    partial class NewMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("users_directory.DbModels.GendersType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GendersType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Gender = "კაცი"
                        },
                        new
                        {
                            Id = 2,
                            Gender = "ქალი"
                        },
                        new
                        {
                            Id = 3,
                            Gender = "სხვა"
                        });
                });

            modelBuilder.Entity("users_directory.DbModels.PhoneNumbersType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PhoneNumbersType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "მობილური"
                        },
                        new
                        {
                            Id = 2,
                            Type = "სახლის"
                        },
                        new
                        {
                            Id = 3,
                            Type = "ოფისის"
                        });
                });

            modelBuilder.Entity("users_directory.DbModels.RelationshipType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RelationshipTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Type = "ნათესავი"
                        },
                        new
                        {
                            Id = 2,
                            Type = "კოლეგა"
                        },
                        new
                        {
                            Id = 3,
                            Type = "ნაცნობი"
                        },
                        new
                        {
                            Id = 4,
                            Type = "სხვა"
                        });
                });

            modelBuilder.Entity("users_directory.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CityName = "თბილისი"
                        },
                        new
                        {
                            Id = 2,
                            CityName = "რუსთავი"
                        },
                        new
                        {
                            Id = 3,
                            CityName = "ქუთაისი"
                        },
                        new
                        {
                            Id = 4,
                            CityName = "თელავი"
                        },
                        new
                        {
                            Id = 5,
                            CityName = "ბათუმი"
                        },
                        new
                        {
                            Id = 6,
                            CityName = "მარნეული"
                        },
                        new
                        {
                            Id = 7,
                            CityName = "ზუგდიდი"
                        },
                        new
                        {
                            Id = 8,
                            CityName = "ოზურგეთი"
                        },
                        new
                        {
                            Id = 9,
                            CityName = "ქობულეთი"
                        },
                        new
                        {
                            Id = 10,
                            CityName = "ყვარელი"
                        });
                });

            modelBuilder.Entity("users_directory.Models.PersonRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("RelatedPerson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RelatedTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RelatedTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("PersonRelationships");
                });

            modelBuilder.Entity("users_directory.Models.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("TypeId");

                    b.ToTable("PhoneNumbers");
                });

            modelBuilder.Entity("users_directory.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("BirthDate")
                        .HasColumnType("date");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GenderId")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("GenderId");

                    b.HasIndex("PersonalNumber")
                        .IsUnique();

                    b.ToTable("People");
                });

            modelBuilder.Entity("users_directory.Models.PersonRelationship", b =>
                {
                    b.HasOne("users_directory.DbModels.RelationshipType", "RelatedType")
                        .WithMany()
                        .HasForeignKey("RelatedTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("users_directory.Models.User", "User")
                        .WithMany("Relationships")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RelatedType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("users_directory.Models.PhoneNumber", b =>
                {
                    b.HasOne("users_directory.Models.User", "User")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("users_directory.DbModels.PhoneNumbersType", "NumberType")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("NumberType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("users_directory.Models.User", b =>
                {
                    b.HasOne("users_directory.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("users_directory.DbModels.GendersType", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("users_directory.Models.User", b =>
                {
                    b.Navigation("PhoneNumbers");

                    b.Navigation("Relationships");
                });
#pragma warning restore 612, 618
        }
    }
}
