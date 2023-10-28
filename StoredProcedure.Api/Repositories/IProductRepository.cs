using StoredProcedure.Api.Models.Entities;

namespace StoredProcedure.Api.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId);
    Task<int> AddProductAsync(Product product);
    Task<int> UpdateProductAsync(Product product);
    Task<int> DeleteProductAsync(int ProductId);
}