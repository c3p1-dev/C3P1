﻿// <auto-generated />
using System;
using C3P1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace C3P1.Migrations.VisualCarnet
{
    [DbContext(typeof(VisualCarnetContext))]
    partial class VisualCarnetContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("C3P1.Client.Components.Apps.VisualCarnet.FicCpt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodCpt")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(35)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Visible")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FicCpts");
                });

            modelBuilder.Entity("C3P1.Client.Components.Apps.VisualCarnet.FicFam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodFam")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .HasMaxLength(35)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FicFams");
                });

            modelBuilder.Entity("C3P1.Client.Components.Apps.VisualCarnet.FicSfa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodFam")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodSfa")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nom")
                        .HasMaxLength(35)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("FicSfas");
                });

            modelBuilder.Entity("C3P1.Client.Components.Apps.VisualCarnet.WrkEcrLig", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CodCpt")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<string>("CodSfa")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("TEXT");

                    b.Property<double?>("Cre")
                        .HasColumnType("REAL");

                    b.Property<double?>("Deb")
                        .HasColumnType("REAL");

                    b.Property<DateOnly?>("Jma")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("JmaVal")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lib1")
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("Lib2")
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("NoChq")
                        .HasMaxLength(7)
                        .HasColumnType("TEXT");

                    b.Property<int>("Nol")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("SldProgressif")
                        .HasColumnType("REAL");

                    b.Property<Guid>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WrkEcrLigs");
                });
#pragma warning restore 612, 618
        }
    }
}
