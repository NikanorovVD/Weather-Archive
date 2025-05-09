﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Entities.WeatherRecord", b =>
                {
                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("AtmosphericPressure")
                        .HasColumnType("integer");

                    b.Property<int?>("CloudCover")
                        .HasColumnType("integer");

                    b.Property<int?>("H")
                        .HasColumnType("integer");

                    b.Property<int?>("RelativeHumidity")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Td")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Temperature")
                        .HasColumnType("numeric");

                    b.Property<string>("VV")
                        .HasColumnType("text");

                    b.Property<string>("WeatherPhenomena")
                        .HasColumnType("text");

                    b.Property<string>("WindDirection")
                        .HasColumnType("text");

                    b.Property<int?>("WindSpeed")
                        .HasColumnType("integer");

                    b.HasKey("DateTime");

                    b.ToTable("WeatherRecords");
                });
#pragma warning restore 612, 618
        }
    }
}
