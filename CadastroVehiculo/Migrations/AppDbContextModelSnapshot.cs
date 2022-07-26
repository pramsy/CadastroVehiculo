﻿// <auto-generated />
using System;
using CadastroVehiculo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CadastroVehiculo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CadastroVehiculo.Models.Fuel", b =>
                {
                    b.Property<int>("FuelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FuelId"), 1L, 1);

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("FuelId");

                    b.ToTable("Fuels");
                });

            modelBuilder.Entity("CadastroVehiculo.Models.Gearbox", b =>
                {
                    b.Property<int>("GearboxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GearboxId"), 1L, 1);

                    b.Property<string>("GearboxType")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("GearboxId");

                    b.ToTable("Gearbox");
                });

            modelBuilder.Entity("CadastroVehiculo.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VehicleId"), 1L, 1);

                    b.Property<int>("FuelId")
                        .HasColumnType("int");

                    b.Property<int>("GearboxId")
                        .HasColumnType("int");

                    b.Property<string>("VehicleBrand")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("VehicleModel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("VehicleNomberOfDoors")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("VehicleYearOfManifacture")
                        .HasColumnType("datetime2");

                    b.HasKey("VehicleId");

                    b.HasIndex("FuelId");

                    b.HasIndex("GearboxId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CadastroVehiculo.Models.Vehicle", b =>
                {
                    b.HasOne("CadastroVehiculo.Models.Fuel", "Fuel")
                        .WithMany("Vehicles")
                        .HasForeignKey("FuelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CadastroVehiculo.Models.Gearbox", "Gearbox")
                        .WithMany("Vehicles")
                        .HasForeignKey("GearboxId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fuel");

                    b.Navigation("Gearbox");
                });

            modelBuilder.Entity("CadastroVehiculo.Models.Fuel", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("CadastroVehiculo.Models.Gearbox", b =>
                {
                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
