using Cukiernia.Domain.Entities;

namespace Cukiernia.Domain.Interfaces
{
    public interface IBakingRepository
    {
        Task Create(Domain.Entities.Baking baking);
        Task Delete(Domain.Entities.Baking baking);
        Task<Domain.Entities.Baking?> GetByName(string name);
        Task<IEnumerable<Domain.Entities.Baking>> GetAll();
        Task<Domain.Entities.Baking> GetByEncodedName(string encodedName);
        Task Commit();
        Task<IEnumerable<SubProduct>> GetAllSubProducts();
        Task CreateSubProduct(SubProduct subProduct);
        Task<Domain.Entities.SubProduct?> GetSubProductByEncodedName(string encodedName);
        Task<SubProduct?> GetSubProductByName(string name);
        Task DeleteSubProduct(SubProduct subProduct);
        Task CreateProductsQuantityList(List<ProductQuantity> productsQuantity);
        Task<IEnumerable<SubProduct>> GetAllSubProductsNotInBaking(string encodedName);
        Task AddSubProductsToList(IEnumerable<ProductQuantity> productQuantities, string encodedName);
        Task<ProductQuantity?> GetQuantityOfSubProductInBaking(int id);
        Task DeleteProductQuantity(ProductQuantity productQuantity);
        Task<ImageUrl> GetImageById(int id);
        Task DeleteImage(ImageUrl image);

    }
}
