﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WWW.DAL;

#nullable disable

namespace WWW.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.3.23174.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ArticleTags", b =>
                {
                    b.Property<int>("ArticlesId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("ArticlesId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ArticleTags");
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.Property<int>("FavEventId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavEventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ArticleUser");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<bool>("Published")
                        .HasColumnType("bit");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AutorId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("LocationID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WWW.Domain.Entity.EventDates", b =>
                {
                    b.Property<int>("ArticleID")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date_Of_Start")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_Of_Updated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date_of_Creation")
                        .HasColumnType("datetime2");

                    b.HasKey("ArticleID");

                    b.ToTable("Date");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Location", b =>
                {
                    b.Property<int>("LocationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationID"));

                    b.Property<string>("Building")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Picture", b =>
                {
                    b.Property<int>("PictureID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("PictureID");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("WWW.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Avatar")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            NickName = "admin",
                            Role = 2
                        },
                        new
                        {
                            Id = 2,
                            Email = "ticketmaster@gmail.com",
                            NickName = "TicketMaster",
                            Role = 1
                        });
                });

            modelBuilder.Entity("WWW.Domain.Entity.User_Details", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<string>("Introdaction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserID");

                    b.ToTable("User_Details");

                    b.HasData(
                        new
                        {
                            UserID = 1,
                            Introdaction = "Admin Account",
                            Password = "03531ed23e58d474162aec45787f78c784ce246f8df573bf1f89b5d6f75b68f7"
                        },
                        new
                        {
                            UserID = 2,
                            Introdaction = "TicketMaster Official Account",
                            Password = "240c213c2ef6246c471147df64587a22d7198bf540f520e5ac04e99a45fdb6a4"
                        });
                });

            modelBuilder.Entity("ArticleTags", b =>
                {
                    b.HasOne("WWW.Domain.Entity.Article", null)
                        .WithMany()
                        .HasForeignKey("ArticlesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WWW.Domain.Entity.Tags", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ArticleUser", b =>
                {
                    b.HasOne("WWW.Domain.Entity.Article", null)
                        .WithMany()
                        .HasForeignKey("FavEventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WWW.Domain.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WWW.Domain.Entity.Article", b =>
                {
                    b.HasOne("WWW.Domain.Entity.User", "Autor")
                        .WithMany("AutorEvent")
                        .HasForeignKey("AutorId");

                    b.HasOne("WWW.Domain.Entity.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WWW.Domain.Entity.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Category");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("WWW.Domain.Entity.EventDates", b =>
                {
                    b.HasOne("WWW.Domain.Entity.Article", "Article")
                        .WithOne("Date")
                        .HasForeignKey("WWW.Domain.Entity.EventDates", "ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Picture", b =>
                {
                    b.HasOne("WWW.Domain.Entity.Article", "Article")
                        .WithOne("Picture")
                        .HasForeignKey("WWW.Domain.Entity.Picture", "PictureID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");
                });

            modelBuilder.Entity("WWW.Domain.Entity.User_Details", b =>
                {
                    b.HasOne("WWW.Domain.Entity.User", "User")
                        .WithOne("Details")
                        .HasForeignKey("WWW.Domain.Entity.User_Details", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WWW.Domain.Entity.Article", b =>
                {
                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("Picture")
                        .IsRequired();
                });

            modelBuilder.Entity("WWW.Domain.Entity.Category", b =>
                {
                    b.Navigation("Articles");
                });

            modelBuilder.Entity("WWW.Domain.Entity.User", b =>
                {
                    b.Navigation("AutorEvent");

                    b.Navigation("Details")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
