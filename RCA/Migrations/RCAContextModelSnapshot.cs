﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RCA.Data;

namespace RCA.Migrations
{
    [DbContext(typeof(RCAContext))]
    partial class RCAContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("RCA.Models.Class_Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<double>("Percent");

                    b.Property<int>("StatusId");

                    b.Property<double>("Tax");

                    b.Property<int>("TypeId");

                    b.HasKey("Id");

                    b.ToTable("Channel");
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

                    b.Property<int>("GroupLevelId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("OccupantsNum");

                    b.Property<int>("PCD");

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.ToTable("GroupLevelItem");
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

            modelBuilder.Entity("RCA.Models.Class_Reception", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Book_AdultsNum");

                    b.Property<DateTime>("Book_DateIn");

                    b.Property<DateTime>("Book_DateOut");

                    b.Property<double>("Book_DayValue");

                    b.Property<int>("Book_DiscountPercent");

                    b.Property<int>("Book_GroupLevelItemId");

                    b.Property<int>("Book_GroupLevelItemTaxID");

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

            modelBuilder.Entity("RCA.Models.Class_Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("StatusId");

                    b.HasKey("Id");

                    b.ToTable("Season");
                });

            modelBuilder.Entity("RCA.Models.Class_SeasonItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupLevelItemId");

                    b.Property<int>("SeasonId");

                    b.Property<double>("Tax");

                    b.HasKey("Id");

                    b.ToTable("SeasonItem");
                });

            modelBuilder.Entity("RCA.Models.Class_User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("StatusId");

                    b.Property<int>("TypeAccessId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
