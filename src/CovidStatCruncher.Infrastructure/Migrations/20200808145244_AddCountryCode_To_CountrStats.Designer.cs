﻿// <auto-generated />
using System;
using CovidStatCruncher.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CovidStatCruncher.Infrastructure.Migrations
{
    [DbContext(typeof(CovidStatCruncherContext))]
    [Migration("20200808145244_AddCountryCode_To_CountrStats")]
    partial class AddCountryCode_To_CountrStats
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CovidStatCruncher.Infrastructure.Models.Countries", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Aged65Older")
                        .HasColumnType("double");

                    b.Property<double>("Aged70Older")
                        .HasColumnType("double");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CountrySlug")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("DiabetesPrevalence")
                        .HasColumnType("double");

                    b.Property<double>("GdpPerCapita")
                        .HasColumnType("double");

                    b.Property<double>("HandwashingFacilities")
                        .HasColumnType("double");

                    b.Property<bool?>("HasData")
                        .HasColumnType("tinyint(1)");

                    b.Property<double>("HospitalBedsPerThousand")
                        .HasColumnType("double");

                    b.Property<string>("Iso2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("LifeExpectancy")
                        .HasColumnType("double");

                    b.Property<double>("MedianAge")
                        .HasColumnType("double");

                    b.Property<double>("Population")
                        .HasColumnType("double");

                    b.Property<double>("PopulationDensity")
                        .HasColumnType("double");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CovidStatCruncher.Infrastructure.Models.CountryData", b =>
                {
                    b.Property<int>("UpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActiveCases")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("ConfirmedCases")
                        .HasColumnType("int");

                    b.Property<int?>("CountriesCountryId")
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("CountryName")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<string>("Latitude")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Longitude")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Province")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Recovered")
                        .HasColumnType("int");

                    b.HasKey("UpdateId");

                    b.HasIndex("CountriesCountryId");

                    b.ToTable("CountryData");
                });

            modelBuilder.Entity("CovidStatCruncher.Infrastructure.Models.CountryStatistics", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("CountryName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("GrangerStatistics")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MeasureImportances")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("CountryId");

                    b.ToTable("CountryStatistics");
                });

            modelBuilder.Entity("CovidStatCruncher.Infrastructure.Models.CountryData", b =>
                {
                    b.HasOne("CovidStatCruncher.Infrastructure.Models.Countries", null)
                        .WithMany("CountryData")
                        .HasForeignKey("CountriesCountryId");
                });
#pragma warning restore 612, 618
        }
    }
}
