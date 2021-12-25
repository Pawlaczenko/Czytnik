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
    public class FluentUserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasIndex(i => i.Username).IsUnique();
            modelBuilder.HasIndex(i => i.Email).IsUnique();
            modelBuilder.Property(i => i.Username).IsRequired().HasMaxLength(50);
            modelBuilder.Property(i => i.FirstName).HasMaxLength(30);
            modelBuilder.Property(i => i.SecondName).HasMaxLength(30);
            modelBuilder.Property(i => i.Surname).HasMaxLength(40);
            modelBuilder.Property(i => i.BirthDate).IsRequired();
            modelBuilder.Property(i => i.ProfilePicture).IsRequired().HasMaxLength(200).HasDefaultValue("link");
            modelBuilder.Property(i => i.Password).IsRequired().HasMaxLength(200);
            modelBuilder.Property(i => i.Email).IsRequired().HasMaxLength(100);
            modelBuilder.Property(i => i.PhoneNumber).HasMaxLength(9);
            modelBuilder.Property(i => i.IsActive).IsRequired().HasDefaultValue(true);
        }
    }
}
