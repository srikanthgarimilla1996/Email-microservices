﻿// <auto-generated />
using Email.Services.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Email.Services.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Email.Services.API.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "srikanthg@technoflair.com",
                            UserName = "srikanthg"
                        },
                        new
                        {
                            Id = 2,
                            Email = "revanthv@technoflair.com",
                            UserName = "revanthv"
                        },
                        new
                        {
                            Id = 3,
                            Email = "sreedhars@technoflair.com",
                            UserName = "sreedhars"
                        },
                        new
                        {
                            Id = 4,
                            Email = "mohsinm@technoflair.com",
                            UserName = "mohsinm"
                        },
                        new
                        {
                            Id = 5,
                            Email = "akhilas@technoflair.com",
                            UserName = "akhilas"
                        },
                        new
                        {
                            Id = 6,
                            Email = "ravichandrav@technoflair.com",
                            UserName = "ravichandrav"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
