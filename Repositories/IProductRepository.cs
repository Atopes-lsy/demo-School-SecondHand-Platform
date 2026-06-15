using System.Collections.Generic;
using School二手Platform.Models;

namespace School二手Platform.Repositories
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        Product? GetProductById(int id);
        List<Product> GetFilteredProducts(string? keyword, string? category, ProductCondition? condition, PriceStrategy? strategy);
        List<Product> GetProductsBySeller(int sellerId);
        List<Product> GetAllProducts();
    }
}
