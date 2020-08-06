﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebFrontend.Data;

namespace WebFrontend.Migrations.WebFrontend
{
    [DbContext(typeof(WebFrontendContext))]
    [Migration("20200804133931_CreateOrder")]
    partial class CreateOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebFrontend.Model.Author", b =>
                {
                    b.Property<int>("AuthorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuthorID");

                    b.HasIndex("BookID");

                    b.ToTable("Author");
                });

            modelBuilder.Entity("WebFrontend.Model.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BillingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShippingAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("UserID")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebFrontend.Model.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("PaymentTypeID");

                    b.HasIndex("OrderID");

                    b.ToTable("PaymentType");
                });

            modelBuilder.Entity("WebFrontend.Model.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Product");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Product");
                });

            modelBuilder.Entity("WebFrontend.Model.Publisher", b =>
                {
                    b.Property<int>("PublisherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookID")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PublisherID");

                    b.HasIndex("BookID");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("WebFrontend.Model.ShipmentType", b =>
                {
                    b.Property<int>("ShipmentTypeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("ShipmentTypeID");

                    b.HasIndex("OrderID");

                    b.ToTable("ShipmentType");
                });

            modelBuilder.Entity("WebFrontend.Model.Book", b =>
                {
                    b.HasBaseType("WebFrontend.Model.Product");

                    b.Property<int>("BookID")
                        .HasColumnType("int");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishedOn")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("WebFrontend.Model.Author", b =>
                {
                    b.HasOne("WebFrontend.Model.Book", "Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookID");
                });

            modelBuilder.Entity("WebFrontend.Model.PaymentType", b =>
                {
                    b.HasOne("WebFrontend.Model.Order", null)
                        .WithMany("PaymentID")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("WebFrontend.Model.Publisher", b =>
                {
                    b.HasOne("WebFrontend.Model.Book", "Book")
                        .WithMany("Publishers")
                        .HasForeignKey("BookID");
                });

            modelBuilder.Entity("WebFrontend.Model.ShipmentType", b =>
                {
                    b.HasOne("WebFrontend.Model.Order", null)
                        .WithMany("ShipmentID")
                        .HasForeignKey("OrderID");
                });
#pragma warning restore 612, 618
        }
    }
}
