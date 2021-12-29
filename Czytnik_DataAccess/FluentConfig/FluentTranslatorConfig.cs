﻿using Czytnik_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Czytnik_DataAccess.FluentConfig
{
    public class FluentTranslatorConfig : IEntityTypeConfiguration<Translator>
    {
        public void Configure(EntityTypeBuilder<Translator> modelBuilder)
        {
            modelBuilder.Property(i => i.FirstName).IsRequired().HasMaxLength(30);
            modelBuilder.Property(i => i.SecondName).HasMaxLength(30);
            modelBuilder.Property(i => i.Surname).HasMaxLength(40);
            modelBuilder.Property(i => i.Id).ValueGeneratedNever();
        }
    }
}
