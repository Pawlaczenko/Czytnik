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
    public class FluentTemplateConfig : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> modelBuilder)
        {
            modelBuilder.Property(i => i.Name).IsRequired().HasMaxLength(30);
            modelBuilder.Property(i => i.Surname).IsRequired().HasMaxLength(40);
            modelBuilder.Property(i => i.PhoneNumber).IsRequired().HasMaxLength(9);
            modelBuilder.Property(i => i.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Property(i => i.HouseNumber).IsRequired();
            modelBuilder.Property(i => i.StreetName).HasMaxLength(60);
            modelBuilder.Property(i => i.Town).IsRequired().HasMaxLength(100);
            modelBuilder.Property(i => i.Post).IsRequired().HasMaxLength(100);
            modelBuilder.Property(i => i.PostCode).IsRequired().HasMaxLength(6);
        }
    }
}
