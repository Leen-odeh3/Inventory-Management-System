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

    public async Task UpdateProductAsync(string productName, string newName, decimal newPrice, int newQuantity)
    {
        var update = Builders<Product>.Update;
        var updateDefinition = new List<UpdateDefinition<Product>>();

        if (!string.IsNullOrEmpty(newName))
        {
            updateDefinition.Add(update.Set(p => p.Name, newName));
        }

        if (newPrice > 0)
        {
            updateDefinition.Add(update.Set(p => p.Price, newPrice));
        }

        if (newQuantity >= 0)
        {
            updateDefinition.Add(update.Set(p => p.Quantity, newQuantity));
        }

        if (updateDefinition.Count == 0)
        {
            return; 
        }

        var filter = Builders<Product>.Filter.Eq("Name", productName);
        var combinedUpdate = update.Combine(updateDefinition);
        await _products.UpdateOneAsync(filter, combinedUpdate);
    }

}
