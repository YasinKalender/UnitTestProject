// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnitTestProject.UI.Data;

#nullable disable

namespace UnitTestProject.UI.Migrations
{
    [DbContext(typeof(ProjectContext))]
    partial class ProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("UnitTestProject.UI.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Category1"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Category1"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Category1"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Category1"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Category1"
                        });
                });

            modelBuilder.Entity("UnitTestProject.UI.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Product1",
                            Price = 1000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 2,
                            Name = "Product2",
                            Price = 2000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 3,
                            Name = "Product3",
                            Price = 3000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 4,
                            Name = "Product4",
                            Price = 4000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 5,
                            Name = "Product4",
                            Price = 5000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 6,
                            Name = "Product5",
                            Price = 6000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 7,
                            Name = "Product6",
                            Price = 7000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 8,
                            Name = "Product7",
                            Price = 8000m,
                            Stock = 100
                        },
                        new
                        {
                            Id = 9,
                            Name = "Product8",
                            Price = 9000m,
                            Stock = 100
                        });
                });

            modelBuilder.Entity("UnitTestProject.UI.Entities.Product", b =>
                {
                    b.HasOne("UnitTestProject.UI.Entities.Category", null)
                        .WithMany("Product")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("UnitTestProject.UI.Entities.Category", b =>
                {
                    b.Navigation("Product");
                });
#pragma warning restore 612, 618
        }
    }
}
