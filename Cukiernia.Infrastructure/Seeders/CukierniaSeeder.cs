using Cukiernia.Domain.Entities;
using Cukiernia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Cukiernia.Infrastructure.Seeders
{
    public class CukierniaSeeder
    {
        private readonly CukierniaDbContext _dbContext;

        public CukierniaSeeder(CukierniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if(pendingMigrations != null && pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync();
                }

                if (!_dbContext.Bakings.Any())
                {

                    var baking1 = new Baking()
                    {
                        Name = "ciasto truskawkowe",
                        Description = "Pyszne ciasto truskawkowe, takie nie za słodkie",
                        Price = 35.80M,
                    };
                    baking1.EncodeName();
                    var baking2 = new Baking()
                        {
                            Name = "ciasto z borówkami",
                            Description ="Pyszne ciasto borówkowe, na pewno uwalisz sobie tą białą luzkę",
                            Price = 42.50M,
                        
                    };
                    baking2.EncodeName();

                    _dbContext.Bakings.Add(baking1);
                    _dbContext.Bakings.Add(baking2);
                    _dbContext.SaveChanges();
                }

                if (!_dbContext.Measures.Any())
                {
                    List<Measure> measures = new List<Measure>() {
                    new Measure() { Id=1, Value = "g" },
                    new Measure() { Id=2, Value = "ml" },
                    new Measure() { Id=3, Value = "szt" }
                    };
                    _dbContext.Measures.AddRange(measures);

                    _dbContext.Database.OpenConnection();
                    try
                    {
                        _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Measures] ON");
                        _dbContext.SaveChanges();
                        _dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Measures] OFF");
                    }
                    finally
                    {
                        _dbContext.Database.CloseConnection();
                    }
                }

                if (!_dbContext.SubProducts.Any() && _dbContext.Measures.Any())
                {
                    List<SubProduct> subProducts = new List<SubProduct>() {
                    new SubProduct() { Name="mąka typ 450", IsAllergenic = false, MeasureId=1, Price=4.50M, Package=1000},
                    new SubProduct() { Name="jajka L", IsAllergenic = true, MeasureId=3, Price=1.20M, Package=10},
                    new SubProduct() { Name="śmietana 30%", IsAllergenic = false, MeasureId=2, Price=3.60M, Package=200},
                    new SubProduct() { Name="borówki", IsAllergenic = false, MeasureId=1, Price=30.00M, Package=1000},
                    new SubProduct() { Name="truskawki", IsAllergenic = false, MeasureId=1, Price=14.50M, Package=1000},
                    new SubProduct() { Name="masło", IsAllergenic = false, MeasureId=1, Price=5.50M, Package=200},
                    };
                    subProducts.ForEach(sp => sp.EncodeName());
                    _dbContext.SubProducts.AddRange(subProducts);
                    _dbContext.SaveChanges();
                }

                if (false && !_dbContext.ProductQuantities.Any() && _dbContext.SubProducts.Any() && _dbContext.Bakings.Any())
                {
                    int b = _dbContext.Bakings.First().Id - 1;
                    int sp = _dbContext.SubProducts.First().Id - 1;
                    List<ProductQuantity> ProductQuantities = new List<ProductQuantity>() {
                        new ProductQuantity()
                        {                            
                            SubProductId = 1+sp,
                            SubProductQuantity = 500,
                            BakingId = 1+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 2+sp,
                            SubProductQuantity = 6,
                            BakingId = 1+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 6+sp,
                            SubProductQuantity = 200,
                            BakingId = 1+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 5+sp,
                            SubProductQuantity = 500,
                            BakingId = 1+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 1+sp,
                            SubProductQuantity = 600,
                            BakingId = 2+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 2+sp,
                            SubProductQuantity = 8,
                            BakingId = 2+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 4+sp,
                            SubProductQuantity = 300,
                            BakingId = 2+b
                        },
                        new ProductQuantity()
                        {                            
                            SubProductId = 6+sp,
                            SubProductQuantity = 250,
                            BakingId = 2+b
                        },
                    };
                    _dbContext.ProductQuantities.AddRange(ProductQuantities);
                    _dbContext.SaveChanges();
                }
                
            }
        }
    }
}
