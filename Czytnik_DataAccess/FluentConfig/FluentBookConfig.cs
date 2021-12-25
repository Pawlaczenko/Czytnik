using Czytnik_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czytnik_DataAccess.FluentConfig
{
    public class FluentBookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> modelBuilder)
        {
            modelBuilder.Property(i => i.Title).IsRequired().HasMaxLength(100);
            modelBuilder.Property(i => i.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Property(i => i.Description).HasMaxLength(8000);
            modelBuilder.Property(i => i.Cover).HasMaxLength(200).IsRequired();
            modelBuilder.Property(i => i.IssueNumber).HasMaxLength(2).IsRequired().HasDefaultValue("I");
            modelBuilder.Property(i => i.CategoryId).IsRequired();
            modelBuilder.Property(i => i.PublisherId).IsRequired();
            modelBuilder.Property(i => i.Rating).HasDefaultValue(null);
            modelBuilder.Property(i => i.IsInStock).IsRequired().HasDefaultValue(true);

            modelBuilder.HasOne(i => i.OriginalLanguage).WithMany(i => i.OriginalBooks).HasForeignKey(i => i.OriginalLanguageId);
            modelBuilder.HasOne(i => i.EditionLanguage).WithMany(i => i.EditionBooks).HasForeignKey(i => i.EditionLanguageId);
            modelBuilder.HasOne(i => i.Category).WithMany(i => i.Books).HasForeignKey(i => i.CategoryId);
            modelBuilder.HasOne(i => i.Series).WithMany(i => i.Books).HasForeignKey(i => i.SeriesId);
            modelBuilder.HasOne(i => i.Publisher).WithMany(i => i.Books).HasForeignKey(i => i.PublisherId);
        }
    }
}
