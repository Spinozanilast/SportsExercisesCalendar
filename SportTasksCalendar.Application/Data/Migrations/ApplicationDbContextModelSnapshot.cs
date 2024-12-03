﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SportTasksCalendar.Application.Data;

#nullable disable

namespace SportTasksCalendar.Application.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SportTasksCalendar.Application.Models.Calendar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.CalendarDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.HasIndex("Date", "CalendarId")
                        .IsUnique()
                        .HasDatabaseName("ux_calendar_day");

                    b.ToTable("CalendarDays");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CalendarDayId")
                        .HasColumnType("uuid");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<TimeOnly>("EndDate")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<TimeOnly>("StartDate")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CalendarDayId");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.CalendarDay", b =>
                {
                    b.HasOne("SportTasksCalendar.Application.Models.Calendar", "Calendar")
                        .WithMany("CalendarDays")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.Exercise", b =>
                {
                    b.HasOne("SportTasksCalendar.Application.Models.CalendarDay", "CalendarDay")
                        .WithMany("SportTasks")
                        .HasForeignKey("CalendarDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CalendarDay");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.Calendar", b =>
                {
                    b.Navigation("CalendarDays");
                });

            modelBuilder.Entity("SportTasksCalendar.Application.Models.CalendarDay", b =>
                {
                    b.Navigation("SportTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
