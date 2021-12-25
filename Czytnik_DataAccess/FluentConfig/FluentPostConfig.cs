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
    public class FluentPostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> modelBuilder)
        {
            modelBuilder.Property(i => i.Title).IsRequired().HasMaxLength(70);
            modelBuilder.Property(i => i.Content).IsRequired().HasMaxLength(20000);
            modelBuilder.Property(i => i.Photo).IsRequired().HasMaxLength(200).HasDefaultValue("link");

            modelBuilder.HasOne(i => i.Admin).WithOne(i => i.Post).HasForeignKey<Post>("AdminId");
        }
    }
}
