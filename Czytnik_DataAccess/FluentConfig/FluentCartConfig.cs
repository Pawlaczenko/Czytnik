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
    public class FluentCartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> modelBuilder)
        {
            modelBuilder.Property(i => i.Quantity).IsRequired().HasDefaultValue(1);

            modelBuilder.HasKey(i => new { i.BookId, i.UserId });
            modelBuilder.HasOne(i => i.Book).WithMany(i => i.Carts).HasForeignKey(i => i.BookId);
            modelBuilder.HasOne(i => i.User).WithMany(i => i.Carts).HasForeignKey(i => i.UserId);
        }
    }
}
