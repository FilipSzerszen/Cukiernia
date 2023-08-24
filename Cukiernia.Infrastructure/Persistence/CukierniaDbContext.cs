using Cukiernia.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Infrastructure.Persistence
{
    public class CukierniaDbContext : IdentityDbContext
    {
        public CukierniaDbContext(DbContextOptions<CukierniaDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Measure>(eb =>
            {
                eb.HasMany(m => m.SubProducts)
                .WithOne(sp => sp.Measure)
                .HasForeignKey(i => i.MeasureId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            modelBuilder.Entity<Baking>(eb =>
            {
                eb.HasMany(b => b.ProductQuantities)
                .WithOne(i => i.Baking)
                .HasForeignKey(i => i.BakingId)
                .OnDelete(DeleteBehavior.Cascade); 

                eb.HasMany(b => b.Images)
                .WithOne(i => i.Baking)
                .HasForeignKey(i => i.BakingId)
                .OnDelete(DeleteBehavior.Cascade);

                //eb.Property(x => x.Price).HasPrecision(5, 2);
            });

            modelBuilder.Entity<SubProduct>(eb =>
            {
                eb.HasMany(p => p.ProductQuantities)
                .WithOne(sp => sp.SubProduct)
                .HasForeignKey(i => i.SubProductId)
                .OnDelete(DeleteBehavior.NoAction); ;

                //eb.Property(x => x.Price).HasPrecision(5, 2);
                              
            });

            



            //modelBuilder.Entity<SubProduct>()
            //    .Property(x => x.Price).HasPrecision(5,2);

        }

        public DbSet<Baking> Bakings { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        public DbSet<ImageUrl> ImageUrls { get; set; }
        public DbSet<ProductQuantity> ProductQuantities { get; set; }
        public DbSet<Measure> Measures { get; set; }
    }
}
