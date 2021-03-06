// <auto-generated />
using System;
using AplikacjaMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AplikacjaMVC.Migrations
{
    [DbContext(typeof(ProjectDatabase))]
    [Migration("20211114214225_ImagesVotes")]
    partial class ImagesVotes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AplikacjaMVC.Models.Images", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Filename")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Filetype")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AplikacjaMVC.Models.ImagesVotes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ImageID")
                        .HasColumnType("int");

                    b.Property<int?>("ImagesId")
                        .HasColumnType("int");

                    b.Property<int?>("RegisterId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Vote")
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("ImagesId");

                    b.HasIndex("RegisterId");

                    b.ToTable("ImagesVotes");
                });

            modelBuilder.Entity("AplikacjaMVC.Models.Register", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Hash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Register");
                });

            modelBuilder.Entity("AplikacjaMVC.Models.ImagesVotes", b =>
                {
                    b.HasOne("AplikacjaMVC.Models.Images", "Images")
                        .WithMany("ImagesVotes")
                        .HasForeignKey("ImagesId");

                    b.HasOne("AplikacjaMVC.Models.Register", "Register")
                        .WithMany("ImagesVotes")
                        .HasForeignKey("RegisterId");

                    b.Navigation("Images");

                    b.Navigation("Register");
                });

            modelBuilder.Entity("AplikacjaMVC.Models.Images", b =>
                {
                    b.Navigation("ImagesVotes");
                });

            modelBuilder.Entity("AplikacjaMVC.Models.Register", b =>
                {
                    b.Navigation("ImagesVotes");
                });
#pragma warning restore 612, 618
        }
    }
}
