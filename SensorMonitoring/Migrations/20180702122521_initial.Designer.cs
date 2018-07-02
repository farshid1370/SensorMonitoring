﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SensorMonitoring.Model;

namespace SensorMonitoring.Migrations
{
    [DbContext(typeof(SensorMonitoringContext))]
    [Migration("20180702122521_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("SensorMonitoring.Model.Lab", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Labs");
                });

            modelBuilder.Entity("SensorMonitoring.Model.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IndicatorCode");

                    b.Property<int>("LabId");

                    b.Property<string>("SubTitle")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("LabId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("SensorMonitoring.Model.SensorData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Priorty");

                    b.Property<int>("SensorId");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Value")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("SensorDatas");
                });

            modelBuilder.Entity("SensorMonitoring.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SensorMonitoring.Model.Lab", b =>
                {
                    b.HasOne("SensorMonitoring.Model.User")
                        .WithMany("Labs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SensorMonitoring.Model.Sensor", b =>
                {
                    b.HasOne("SensorMonitoring.Model.Lab")
                        .WithMany("Sensors")
                        .HasForeignKey("LabId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SensorMonitoring.Model.SensorData", b =>
                {
                    b.HasOne("SensorMonitoring.Model.Sensor")
                        .WithMany("SensorDatas")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}