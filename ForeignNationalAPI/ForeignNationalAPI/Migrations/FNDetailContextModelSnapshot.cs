﻿// <auto-generated />
using ForeignNationalAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ForeignNationalAPI.Migrations
{
    [DbContext(typeof(FNDetailContext))]
    partial class FNDetailContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ForeignNationalAPI.Models.FNDetail", b =>
                {
                    b.Property<int>("FNDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DateOfBirth")
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("FNemail")
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("FNgender")
                        .HasColumnType("nvarchar(7)");

                    b.Property<string>("firstname")
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("lastname")
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("FNDetailId");

                    b.ToTable("FN_Details");
                });
#pragma warning restore 612, 618
        }
    }
}
