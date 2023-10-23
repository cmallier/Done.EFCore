using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EfCoreSamplesApp.Models
{
    public partial class EfCoreSamplesAppContext : DbContext
    {
        public virtual DbSet<Orders> Orders { get; set; }

        public EfCoreSamplesAppContext()
        {
        }

        public EfCoreSamplesAppContext(DbContextOptions<EfCoreSamplesAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Version)
                    .IsRowVersion()
                    .IsConcurrencyToken();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
