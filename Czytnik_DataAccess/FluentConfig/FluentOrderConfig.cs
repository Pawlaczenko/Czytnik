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
    public class FluentOrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.Property(i => i.UserId).IsRequired();
            modelBuilder.Property(i => i.OrderDate).IsRequired();

            modelBuilder.HasKey(i => new { i.UserId });
            modelBuilder.HasOne(i => i.User).WithMany(i => i.Orders).HasForeignKey(i => i.UserId);
        }
    }
}
