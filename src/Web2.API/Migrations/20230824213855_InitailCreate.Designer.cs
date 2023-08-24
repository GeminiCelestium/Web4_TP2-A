﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Web2.API.Models;

#nullable disable

namespace Web2.API.Migrations
{
    [DbContext(typeof(TP2A_Context))]
    [Migration("20230824213855_InitailCreate")]
    partial class InitailCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Web2.API.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Web2.API.Models.Evenement", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<int>>("CategoryIDs")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.Property<DateTime?>("DateDebut")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Organisateur")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Prix")
                        .HasColumnType("double precision");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("VilleID")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Evenements");
                });

            modelBuilder.Entity("Web2.API.Models.Participation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("EvenementId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NombrePlace")
                        .HasColumnType("integer");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Participations");
                });

            modelBuilder.Entity("Web2.API.Models.Ville", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Region")
                        .HasColumnType("integer");

                    b.HasKey("ID");

                    b.ToTable("Villes");
                });
#pragma warning restore 612, 618
        }
    }
}
