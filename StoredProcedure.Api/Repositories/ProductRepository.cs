using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoredProcedure.Api.Models.Data;
using StoredProcedure.Api.Models.Entities;

namespace StoredProcedure.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ProductRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Product>> GetProducts()
    {
        return await _dbContext.Product
            .FromSqlRaw<Product>("GetProductList")
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductByIdAsync(int ProductId)
    {
        var param = new SqlParameter("@ProductId", ProductId);
        var productDetails = await Task.Run(() => _dbContext.Product
            .FromSqlRaw(@"exec GetProductByID @ProductId", param).ToListAsync());
        return productDetails;
    }

    public async Task<int> AddProductAsync(Product product)
    {
        var parameter = new List<SqlParameter>();
        parameter.Add(new SqlParameter("@ProductName", product.ProductName));
        parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
        parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
        parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));
        var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(@"exec AddNewProduct @ProductName, @ProductDescription, @ProductPrice, @ProductStock",
                parameter.ToArray()));
        return result;
    }

    public async Task<int> UpdateProductAsync(Product product)
    {
        var parameter = new List<SqlParameter>();
        parameter.Add(new SqlParameter("@ProductId", product.ProductId));
        parameter.Add(new SqlParameter("@ProductName", product.ProductName));
        parameter.Add(new SqlParameter("@ProductDescription", product.ProductDescription));
        parameter.Add(new SqlParameter("@ProductPrice", product.ProductPrice));
        parameter.Add(new SqlParameter("@ProductStock", product.ProductStock));
        var result = await Task.Run(() => _dbContext.Database
            .ExecuteSqlRawAsync(
                @"exec UpdateProduct @ProductId, @ProductName, @ProductDescription, @ProductPrice, @ProductStock",
                parameter.ToArray()));
        return result;
    }

    public async Task<int> DeleteProductAsync(int ProductId)
    {
        return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeletePrductByID {ProductId}"));
    }
}