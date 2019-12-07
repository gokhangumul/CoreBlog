﻿// <auto-generated />
using System;
using CoreBlog.Data.Concrete.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreBlog.Data.Migrations
{
    [DbContext(typeof(BlogContext))]
    [Migration("20190914175008_InitialCreateDb")]
    partial class InitialCreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.11-servicing-32099")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CoreBlog.Entity.DbModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UniqKey")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CoreBlog.Entity.DbModel.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Path")
                        .IsRequired();

                    b.Property<int>("PostId");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UniqKey")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CoreBlog.Entity.DbModel.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuthorName")
                        .IsRequired();

                    b.Property<int>("CategoryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("Hit");

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<string>("UniqKey")
                        .IsRequired();

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("Url")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("CoreBlog.Entity.DbModel.Image", b =>
                {
                    b.HasOne("CoreBlog.Entity.DbModel.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("CoreBlog.Entity.DbModel.Post", b =>
                {
                    b.HasOne("CoreBlog.Entity.DbModel.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
