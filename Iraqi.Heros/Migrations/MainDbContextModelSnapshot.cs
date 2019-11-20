﻿// <auto-generated />
using System;
using Iraqi.Heros.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Iraqi.Heros.Migrations
{
    [DbContext(typeof(MainDbContext))]
    partial class MainDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Iraqi.Heros.Models.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Key")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Path")
                        .HasColumnType("text");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Iraqi.Heros.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DoB")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DoK")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Gov")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PoK")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Story")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Iraqi.Heros.Models.Users", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Iraqi.Heros.Models.Image", b =>
                {
                    b.HasOne("Iraqi.Heros.Models.Person", null)
                        .WithMany("Images")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
