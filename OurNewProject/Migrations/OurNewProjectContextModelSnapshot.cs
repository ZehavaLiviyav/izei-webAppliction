﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OurNewProject.Data;

namespace OurNewProject.Migrations
{
    [DbContext(typeof(OurNewProjectContext))]
    partial class OurNewProjectContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.Property<int>("MyOrderListOrderID")
                        .HasColumnType("int");

                    b.Property<int>("MyProductListId")
                        .HasColumnType("int");

                    b.HasKey("MyOrderListOrderID", "MyProductListId");

                    b.HasIndex("MyProductListId");

                    b.ToTable("OrderProduct");
                });

            modelBuilder.Entity("OurNewProject.Models.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BranchName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OcHours")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branch");
                });

            modelBuilder.Entity("OurNewProject.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("OurNewProject.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeOrder")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("OrderID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("OurNewProject.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InStock")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("OurNewProject.Models.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Imge")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("productId")
                        .IsUnique();

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("OurNewProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("OrderProduct", b =>
                {
                    b.HasOne("OurNewProject.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("MyOrderListOrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OurNewProject.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("MyProductListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OurNewProject.Models.Product", b =>
                {
                    b.HasOne("OurNewProject.Models.Category", "Category")
                        .WithMany("myProducts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("OurNewProject.Models.ProductImage", b =>
                {
                    b.HasOne("OurNewProject.Models.Product", "product")
                        .WithOne("productImage")
                        .HasForeignKey("OurNewProject.Models.ProductImage", "productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("product");
                });

            modelBuilder.Entity("OurNewProject.Models.Category", b =>
                {
                    b.Navigation("myProducts");
                });

            modelBuilder.Entity("OurNewProject.Models.Product", b =>
                {
                    b.Navigation("productImage");
                });
#pragma warning restore 612, 618
        }
    }
}
