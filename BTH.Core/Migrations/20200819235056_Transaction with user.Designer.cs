﻿// <auto-generated />
using System;
using BTH.Core.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BTH.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200819235056_Transaction with user")]
    partial class Transactionwithuser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("BHT.Core.Entities.CoBaTransaction", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BookingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("BookingText")
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .HasColumnType("TEXT");

                    b.Property<string>("Currency")
                        .HasColumnType("TEXT");

                    b.Property<string>("TurnoverType")
                        .HasColumnType("TEXT");

                    b.Property<long>("UserAccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ValueDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookingText")
                        .IsUnique();

                    b.HasIndex("UserAccountId");

                    b.ToTable("CoBaTransactions");
                });

            modelBuilder.Entity("BTH.Core.Entities.CoBaUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BIC")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClientAccount")
                        .HasColumnType("TEXT");

                    b.Property<string>("IBAN")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IBAN")
                        .IsUnique();

                    b.ToTable("CoBaUsers");
                });

            modelBuilder.Entity("BHT.Core.Entities.CoBaTransaction", b =>
                {
                    b.HasOne("BTH.Core.Entities.CoBaUser", "UserAccount")
                        .WithMany("CoBaTransactions")
                        .HasForeignKey("UserAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}