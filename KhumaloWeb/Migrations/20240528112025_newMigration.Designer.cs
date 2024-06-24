﻿// <auto-generated />
using System;
using KhumaloWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KhumaloWeb.Migrations
{
    [DbContext(typeof(KhumaloContext))]
    [Migration("20240528112025_newMigration")]
    partial class newMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KhumaloWeb.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Availability")
                        .IsRequired()
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Category")
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasMaxLength(1200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1200)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("ProductId")
                        .HasName("PK__Products__B40CC6ED3721D24E");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("KhumaloWeb.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TransactionsID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionsId"));

                    b.Property<int?>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("TransactionsId")
                        .HasName("PK__Transact__B872AB6A18D407EA");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("KhumaloWeb.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CCACDE1A6862");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KhumaloWeb.Models.Product", b =>
                {
                    b.HasOne("KhumaloWeb.Models.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Products__UserID__4BAC3F29");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KhumaloWeb.Models.Transaction", b =>
                {
                    b.HasOne("KhumaloWeb.Models.Product", "Product")
                        .WithMany("Transactions")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK__Transacti__Produ__4E88ABD4");

                    b.HasOne("KhumaloWeb.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Transacti__UserI__4F7CD00D");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KhumaloWeb.Models.Product", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("KhumaloWeb.Models.User", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
