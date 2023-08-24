using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Cukiernia.Aplication.Cukiernia.Commands.CreateProductQuantity;
using Cukiernia.Domain.Entities;
using Cukiernia.Domain.Interfaces;
using Cukiernia.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Cukiernia.Infrastructure.Repositories
{
    public class BakingRepository : IBakingRepository
    {
        private readonly CukierniaDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BakingRepository(CukierniaDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task Commit()
        => await _dbContext.SaveChangesAsync();

        public async Task Create(Baking baking)
        {
            _dbContext.Add(baking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Baking baking)
        {
            if (baking.Images.Any())
            {
                foreach (var image in baking.Images)
                {
                    var path = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", image.Url);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    _dbContext.ImageUrls.Remove(image);
                }
            }

            if (baking.ProductQuantities.Any())
            {
                var quantities = baking.ProductQuantities.ToList();

                foreach (var quantity in quantities)
                {
                    var quant = _dbContext.ProductQuantities.Where(p => p.SubProductId == quantity.SubProductId).Count();
                    if (_dbContext.ProductQuantities.Where(p=>p.SubProductId == quantity.SubProductId).Count() == 1)
                    {
                        var xxx = _dbContext.SubProducts.Where(sp=> sp.Id == quantity.SubProductId).First().IsDeletable=true;
                    }
                }

                _dbContext.ProductQuantities.RemoveRange(quantities);
            }

            _dbContext.Bakings.Remove(baking);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Baking>> GetAll()
        => await _dbContext.Bakings
            .Include(i => i.Images)
            .ToListAsync();

        public async Task<IEnumerable<SubProduct>> GetAllSubProducts()
        => await _dbContext.SubProducts
            .Include(i => i.Measure)
            .ToListAsync();

        public async Task<Baking> GetByEncodedName(string encodedName)
        => await _dbContext.Bakings
            .Include(i => i.Images)
            .Include(pq => pq.ProductQuantities).ThenInclude(sp => sp.SubProduct).ThenInclude(m => m.Measure)
            .FirstAsync(b => b.EncodedName.Equals(encodedName));

        public async Task<Baking?> GetByName(string name)
        => await _dbContext.Bakings.FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());

        public async Task CreateSubProduct(SubProduct subProduct)
        {
            _dbContext.SubProducts.Add(subProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<SubProduct?> GetSubProductByEncodedName(string encodedName)
            => await _dbContext.SubProducts.FirstOrDefaultAsync(sp => sp.EncodedName == encodedName);

        public async Task<SubProduct?> GetSubProductByName(string name)
        => await _dbContext.SubProducts.FirstOrDefaultAsync(b => b.Name.ToLower() == name.ToLower());

        public async Task DeleteSubProduct(SubProduct subProduct)
        {
            _dbContext.SubProducts.Remove(subProduct);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateProductsQuantityList(List<ProductQuantity> productsQuantity)
        {
            await _dbContext.ProductQuantities.AddRangeAsync(productsQuantity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubProduct>> GetAllSubProductsNotInBaking(string encodedName)
        {
            var listOfSubProductInBakings = _dbContext.Bakings
            .Include(pq => pq.ProductQuantities).ThenInclude(sp => sp.SubProduct)
            .Where(b => b.EncodedName.Equals(encodedName)).SelectMany(p => p.ProductQuantities.Select(b => b.SubProduct)).ToList();

            var allSubProducts = await _dbContext.SubProducts.ToListAsync();

            foreach (var product in listOfSubProductInBakings)
            {
                if (product != null) { allSubProducts.Remove(product); }
            }

            return allSubProducts;
        }

        public async Task AddSubProductsToList(IEnumerable<ProductQuantity> productQuantities, string encodedName)
        {
            var bakingId = _dbContext.Bakings.Where(b => b.EncodedName.Equals(encodedName)).ToList().First().Id;
            productQuantities.ToList().ForEach(pq => pq.BakingId = bakingId);
            _dbContext.ProductQuantities.AddRange(productQuantities);

            var xxx = _dbContext.SubProducts.Where(sp => sp.IsDeletable == false).ToList();

            foreach (var productQuantity in productQuantities)
            {
                _dbContext.SubProducts.Where(sp => sp.Id == productQuantity.SubProductId).First().IsDeletable = false;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductQuantity?> GetQuantityOfSubProductInBaking(int id)
        => await _dbContext.ProductQuantities.Include(sp => sp.SubProduct).FirstOrDefaultAsync(pq => pq.Id == id);

        public async Task DeleteProductQuantity(ProductQuantity productQuantity)
        {
            _dbContext.ProductQuantities.Remove(productQuantity);
            if (_dbContext.ProductQuantities.Where(p => p.SubProductId == productQuantity.SubProductId).ToList().Count == 1)
            {
                _dbContext.SubProducts.Where(sp => sp.Id == productQuantity.SubProductId).First().IsDeletable = true;
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ImageUrl> GetImageById(int id)
        => await _dbContext.ImageUrls.FirstAsync(i => i.Id == id);

        public async Task DeleteImage(ImageUrl image)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Fotos", image.Url);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            _dbContext.ImageUrls.Remove(image);
            await _dbContext.SaveChangesAsync();
        }
    }
}
