﻿// <auto-generated />
using System;
using Czytnik_DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Czytnik_DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220105195926_Added Order, OrderItem and changed Series not to be required, and removed its default value 'I'")]
    partial class AddedOrderOrderItemandchangedSeriesnottoberequiredandremoveditsdefaultvalueI
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Czytnik_Model.Models.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("link");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Cover")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Description")
                        .HasMaxLength(8000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dimensions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EditionLanguageId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsInStock")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("IssueNumber")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<short?>("NumberOfCopiesSold")
                        .HasColumnType("smallint");

                    b.Property<int?>("OriginalLanguageId")
                        .HasColumnType("int");

                    b.Property<short>("PageQuantity")
                        .HasColumnType("smallint");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PublisherId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(4,2)");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SeriesId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EditionLanguageId");

                    b.HasIndex("OriginalLanguageId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookAuthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BooksAuthors");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookDiscount", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("DiscountId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "DiscountId");

                    b.HasIndex("DiscountId");

                    b.ToTable("BooksDiscounts");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookTranslator", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("TranslatorId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "TranslatorId");

                    b.HasIndex("TranslatorId");

                    b.ToTable("BooksTranslators");
                });

            modelBuilder.Entity("Czytnik_Model.Models.CartItem", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<short>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasDefaultValue((short)1);

                    b.HasKey("BookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<short>("DiscountValue")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Favourite", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(2)
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Order", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Czytnik_Model.Models.OrderItem", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.HasKey("BookId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(20000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("link");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId")
                        .IsUnique();

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<short>("Rating")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Series");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<short>("FlatNumber")
                        .HasColumnType("smallint");

                    b.Property<short>("HouseNumber")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("Post")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("StreetName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<short>("StreetNumber")
                        .HasColumnType("smallint");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Town")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Translator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Translators");
                });

            modelBuilder.Entity("Czytnik_Model.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<string>("ProfilePicture")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasDefaultValue("link");

                    b.Property<string>("SecondName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Book", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Language", "EditionLanguage")
                        .WithMany("EditionBooks")
                        .HasForeignKey("EditionLanguageId");

                    b.HasOne("Czytnik_Model.Models.Language", "OriginalLanguage")
                        .WithMany("OriginalBooks")
                        .HasForeignKey("OriginalLanguageId");

                    b.HasOne("Czytnik_Model.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Series", "Series")
                        .WithMany("Books")
                        .HasForeignKey("SeriesId");

                    b.Navigation("Category");

                    b.Navigation("EditionLanguage");

                    b.Navigation("OriginalLanguage");

                    b.Navigation("Publisher");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookAuthor", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Author", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Book");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookDiscount", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("BookDiscounts")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Discount", "Discount")
                        .WithMany("BookDiscounts")
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("Czytnik_Model.Models.BookTranslator", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("BookTranslators")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Translator", "Translator")
                        .WithMany("BookTranslators")
                        .HasForeignKey("TranslatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Translator");
                });

            modelBuilder.Entity("Czytnik_Model.Models.CartItem", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("CartItems")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.User", "User")
                        .WithMany("CartItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Favourite", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("Favourites")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Order", b =>
                {
                    b.HasOne("Czytnik_Model.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Czytnik_Model.Models.OrderItem", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("OrderItems")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Post", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Admin", "Admin")
                        .WithOne("Post")
                        .HasForeignKey("Czytnik_Model.Models.Post", "AdminId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Review", b =>
                {
                    b.HasOne("Czytnik_Model.Models.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Czytnik_Model.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Template", b =>
                {
                    b.HasOne("Czytnik_Model.Models.User", "User")
                        .WithMany("Templates")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Admin", b =>
                {
                    b.Navigation("Post");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Author", b =>
                {
                    b.Navigation("BookAuthors");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Book", b =>
                {
                    b.Navigation("BookAuthors");

                    b.Navigation("BookDiscounts");

                    b.Navigation("BookTranslators");

                    b.Navigation("CartItems");

                    b.Navigation("Favourites");

                    b.Navigation("OrderItems");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Discount", b =>
                {
                    b.Navigation("BookDiscounts");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Language", b =>
                {
                    b.Navigation("EditionBooks");

                    b.Navigation("OriginalBooks");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Series", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Czytnik_Model.Models.Translator", b =>
                {
                    b.Navigation("BookTranslators");
                });

            modelBuilder.Entity("Czytnik_Model.Models.User", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Favourites");

                    b.Navigation("Orders");

                    b.Navigation("Reviews");

                    b.Navigation("Templates");
                });
#pragma warning restore 612, 618
        }
    }
}
