﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinhasFinancas.Context;

namespace MinhasFinancas.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190411201113_AdicionaNomeItem")]
    partial class AdicionaNomeItem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MinhasFinancas.Models.RelatorioDespesa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<DateTime>("DataDespesa");

                    b.Property<string>("ItemNome")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("RelatorioDespesas");
                });
#pragma warning restore 612, 618
        }
    }
}
