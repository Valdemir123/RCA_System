﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RCA.Data;

namespace RCA.Migrations
{
    [DbContext(typeof(RCAContext))]
    [Migration("20200529195029_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RCA.Models.Class_Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Book_AdultsNum");

                    b.Property<DateTime>("Book_DateIn");

                    b.Property<DateTime>("Book_DateOut");

                    b.Property<double>("Book_DayValue");

                    b.Property<int>("Book_DiscountPercent");

                    b.Property<int>("Book_GroupLevelItemId");

                    b.Property<int>("Book_GroupLevelItemTaxId");

                    b.Property<double>("Book_InputValue");

                    b.Property<int>("Book_KidsNum");

                    b.Property<bool>("Book_PCD");

                    b.Property<bool>("Book_PET");

                    b.Property<string>("Channel_Code")
                        .HasMaxLength(50);

                    b.Property<int>("Channel_GroupLevelId");

                    b.Property<int>("Channel_GroupLevelItemId");

                    b.Property<double>("Channel_Percent");

                    b.Property<double>("Channel_Tax");

                    b.Property<string>("GuestCPF")
                        .IsRequired();

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.ToTable("Book");
                });

            modelBuilder.Entity("RCA.Models.Class_Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ContactName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Phone1")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Phone2")
                        .HasMaxLength(20);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Site")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("RCA.Models.Class_GroupLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<int>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.ToTable("GroupLevel");
                });

            modelBuilder.Entity("RCA.Models.Class_GroupLevelItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Class_GroupLevelId");

                    b.Property<int>("GroupLevelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("OccupantsNum");

                    b.Property<bool>("PCD");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.HasIndex("Class_GroupLevelId");

                    b.ToTable("GroupLevelItem");
                });

            modelBuilder.Entity("RCA.Models.Class_GroupLevelItemTax", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Class_GroupLevelItemId");

                    b.Property<int>("GroupLevelItemId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("Percent");

                    b.Property<double>("Tax");

                    b.HasKey("Id");

                    b.HasIndex("Class_GroupLevelItemId");

                    b.ToTable("GroupLevelItemTax");
                });

            modelBuilder.Entity("RCA.Models.Class_Guest", b =>
                {
                    b.Property<string>("CPF")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Complement")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Phone1")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("Phone2")
                        .HasMaxLength(20);

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("CPF");

                    b.ToTable("Guest");
                });

            modelBuilder.Entity("RCA.Models.Class_GroupLevelItem", b =>
                {
                    b.HasOne("RCA.Models.Class_GroupLevel")
                        .WithMany("GroupLevelItem_LIST")
                        .HasForeignKey("Class_GroupLevelId");
                });

            modelBuilder.Entity("RCA.Models.Class_GroupLevelItemTax", b =>
                {
                    b.HasOne("RCA.Models.Class_GroupLevelItem")
                        .WithMany("GroupLevelItemTax_LIST")
                        .HasForeignKey("Class_GroupLevelItemId");
                });
#pragma warning restore 612, 618
        }
    }
}