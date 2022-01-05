using Czytnik_DataAccess.FluentConfig;
using Czytnik_Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Czytnik_DataAccess.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
        }

        //public virtual Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade DatabaseF { get; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<BookDiscount> BooksDiscounts { get; set; }
        public DbSet<BookAuthor> BooksAuthors { get; set; }
        public DbSet<BookTranslator> BooksTranslators { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FluentAdminConfig());
            modelBuilder.ApplyConfiguration(new FluentAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookAuthorConfig());
            modelBuilder.ApplyConfiguration(new FluentBookConfig());
            modelBuilder.ApplyConfiguration(new FluentBookDiscountConfig());
            modelBuilder.ApplyConfiguration(new FluentBookTranslatorConfig());
            modelBuilder.ApplyConfiguration(new FluentCartItemConfig());
            modelBuilder.ApplyConfiguration(new FluentCategoryConfig());
            modelBuilder.ApplyConfiguration(new FluentDiscountConfig());
            modelBuilder.ApplyConfiguration(new FluentFavouriteConfig());
            modelBuilder.ApplyConfiguration(new FluentLanguageConfig());
            modelBuilder.ApplyConfiguration(new FluentPostConfig());
            modelBuilder.ApplyConfiguration(new FluentPublisherConfig());
            modelBuilder.ApplyConfiguration(new FluentReviewConfig());
            modelBuilder.ApplyConfiguration(new FluentSeriesConfig());
            modelBuilder.ApplyConfiguration(new FluentTemplateConfig());
            modelBuilder.ApplyConfiguration(new FluentTranslatorConfig());
            modelBuilder.ApplyConfiguration(new FluentUserConfig());
            modelBuilder.ApplyConfiguration(new FluentOrderConfig());
            modelBuilder.ApplyConfiguration(new FluentOrderItemConfig());
        }


    }

    
}
