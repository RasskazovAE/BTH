﻿// <auto-generated />
using BHT.WPF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BHT.WPF.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200707212419_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.5");

            modelBuilder.Entity("BHT.WPF.Entities.CoBaTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("BIC")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("BookingText")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<string>("IBAN")
                        .HasColumnType("TEXT");

                    b.Property<string>("TurnoverType")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CoBaTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
