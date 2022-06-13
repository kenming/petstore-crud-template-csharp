using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Thinksoft.crudTutorial.Helper;

namespace Thinksoft.crudTutorial.EDM
{
    public partial class PetStoreDBContext : DbContext
    {
        public PetStoreDBContext()
        {
        }

        public PetStoreDBContext(DbContextOptions<PetStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(DbHelper.GetConnectionString("SqliteDB"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Category).HasColumnType("TEXT(12)");

                entity.Property(e => e.Name).HasColumnType("TEXT(36)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
