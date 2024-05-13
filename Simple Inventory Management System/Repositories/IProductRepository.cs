
namespace Simple_Inventory_Management_System.Repositories;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(string productName, string newName, decimal newPrice, int newQuantity);
    Task DeleteProductAsync(string productName);
    Task<List<Product>> SearchProductAsync(string productName);
}
