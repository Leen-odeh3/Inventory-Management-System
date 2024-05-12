using System.Data;
using System.Data.SqlClient;

namespace Simple_Inventory_Management_System.Repositories;

public class ProductRepository
{
    private readonly string connectionString;

    public ProductRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public async Task AddProductAsync(string name, decimal price, int quantity)
    {
        const string query = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
        var parameters = new Dictionary<string, object>
        {
            { "@Name", name },
            { "@Price", price },
            { "@Quantity", quantity }
        };

        await ExecuteNonQueryAsync(query, parameters, "Product added successfully.");
    }

    public async Task DisplayProductsAsync()
    {
        const string query = "SELECT Name, Price, Quantity FROM Products";
        await DisplayProductListAsync(query);
    }

    public async Task EditProductAsync(string productName, string newName, decimal newPrice, int newQuantity)
    {
        var productId = await GetProductIdAsync(productName);
        if (productId.HasValue)
        {
            const string query = "UPDATE Products SET Name = @NewName, Price = @NewPrice, Quantity = @NewQuantity WHERE ProductID = @ProductId";
            var parameters = new Dictionary<string, object>
            {
                { "@NewName", newName },
                { "@NewPrice", newPrice },
                { "@NewQuantity", newQuantity },
                { "@ProductId", productId.Value }
            };

            await ExecuteNonQueryAsync(query, parameters, "Product updated successfully.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Product with name '{productName}' was not found.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public async Task DeleteProductAsync(int productId)
    {
        try
        {
            const string query = "DELETE FROM Products WHERE ProductID = @ProductId";
            var parameters = new Dictionary<string, object>
            {
                { "@ProductId", productId }
            };

            await ExecuteNonQueryAsync(query, parameters, "Product deleted successfully.");
        }
        catch (SqlException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed to delete product: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public async Task SearchProductAsync(string productName)
    {
        const string query = "SELECT Name, Price, Quantity FROM Products WHERE Name = @ProductName";
        var parameters = new Dictionary<string, object>
        {
            { "@ProductName", productName }
        };

        await DisplayProductListAsync(query, parameters);
    }

    private async Task<int?> GetProductIdAsync(string productName)
    {
        const string query = "SELECT ProductID FROM Products WHERE Name = @ProductName";
        var parameters = new Dictionary<string, object>
        {
            { "@ProductName", productName }
        };

        var reader = await ExecuteReaderAsync(query, parameters);
        return await reader.ReadAsync() ? (int?)reader["ProductID"] : null;
    }

    private async Task ExecuteNonQueryAsync(string query, Dictionary<string, object> parameters, string successMessage)
    {
        try
        {
            await using var connection = new SqlConnection(connectionString);
            await using var command = new SqlCommand(query, connection);
            AddParameters(command, parameters);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
            DisplaySuccessMessage(successMessage);
        }
        catch (SqlException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed operation: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    private async Task DisplayProductListAsync(string query, Dictionary<string, object>? parameters = null)
    {
        try
        {
            await using var reader = await ExecuteReaderAsync(query, parameters);
            Console.WriteLine("Inventory:");
            if (!reader.HasRows)
            {
                Console.WriteLine("No products found.");
            }
            else
            {
                while (await reader.ReadAsync())
                {
                    Console.WriteLine($"{reader["Name"]} - Price: ${reader["Price"]} - Quantity: {reader["Quantity"]}");
                }
            }
        }
        catch (SqlException ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Failed operation: {ex.Message}");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }


    private async Task<SqlDataReader> ExecuteReaderAsync(string query, Dictionary<string, object>? parameters = null)
    {
        var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(query, connection);
        if (parameters != null)
        {
            AddParameters(command, parameters);
        }

        await connection.OpenAsync();
        return await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
    }

    private static void AddParameters(SqlCommand command, Dictionary<string, object> parameters)
    {
        foreach (var (key, value) in parameters)
        {
            command.Parameters.AddWithValue(key, value);
        }
    }

    private void DisplaySuccessMessage(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.White;
    }
}
