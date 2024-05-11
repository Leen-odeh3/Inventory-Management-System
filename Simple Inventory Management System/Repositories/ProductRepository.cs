using MongoDB.Bson;
using MongoDB.Driver;

namespace Simple_Inventory_Management_System.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _products = database.GetCollection<Product>("Products");
    }

    public async Task AddProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }

    public async Task<bool> UpdateProductAsync(string productName, Product product)
    {
        var update = Builders<Product>.Update;
        var updateDefinition = new List<UpdateDefinition<Product>>();

        if (product.Name != null)
        {
            updateDefinition.Add(update.Set(p => p.Name, product.Name));
        }

        if (product.Price != null)
        {
            updateDefinition.Add(update.Set(p => p.Price, product.Price));
        }

        if (product.Quantity != null)
        {
            updateDefinition.Add(update.Set(p => p.Quantity, product.Quantity));
        }

        if (updateDefinition.Count == 0)
        {
            return false;
        }

        var filter = Builders<Product>.Filter.Eq("Name", productName);
        var combinedUpdate = update.Combine(updateDefinition);
        var result = await _products.UpdateOneAsync(filter, combinedUpdate);
        return result.ModifiedCount > 0;
    }

    public async Task DeleteProductAsync(string productName)
    {
        var filter = Builders<Product>.Filter.Eq("Name", productName);
        await _products.DeleteOneAsync(filter);
    }

    public async Task<List<Product>> SearchProductAsync(string productName)
    {
        var filter = Builders<Product>.Filter.Regex("Name", new MongoDB.Bson.BsonRegularExpression(productName, "i"));
        return await _products.Find(filter).ToListAsync();
    }

    public Task UpdateProductAsync(string productName, string newName, decimal newPrice, int newQuantity)
    {
        throw new NotImplementedException();
    }
}
