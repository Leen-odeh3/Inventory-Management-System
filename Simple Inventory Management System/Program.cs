using MongoDB.Driver;
using Simple_Inventory_Management_System.Repositories;

namespace Simple_Inventory_Management_System;

class Program
{
    static async Task Main(string[] args)
    {

        var host = "localhost";
        var port = 27017;
        var connectionString = $"mongodb://{host}:{port}";

        var client = new MongoClient(connectionString);
        string databaseName = "InventoryManagementSystem";
        var database = client.GetDatabase(databaseName);
        var productRepository = new ProductRepository(database, "Products");

        while (true)
        {
            Console.WriteLine("\n1. Add Product");
            Console.WriteLine("2. Display Products");
            Console.WriteLine("3. Edit Product");
            Console.WriteLine("4. Delete Product");
            Console.WriteLine("5. Search for Product");
            Console.WriteLine("6. Exit");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter your choice: ");
            Console.ForegroundColor = ConsoleColor.White;
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter product name: ");
                    string name = Console.ReadLine();

                    Console.Write("Enter product price: ");
                    decimal price;
                    while (!decimal.TryParse(Console.ReadLine(), out price))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid decimal value for the price.");
                        Console.Write("Enter product price: ");
                    }

                    Console.Write("Enter product quantity: ");
                    int quantity;
                    while (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.WriteLine("Invalid input. Please enter a valid integer value for the quantity.");
                        Console.Write("Enter product quantity: ");
                    }

                    await productRepository.AddProductAsync(new Product { Name = name, Price = price, Quantity = quantity });
                    Console.WriteLine("Product added successfully.");
                    break;

                case "2":
                    var products = await productRepository.GetAllProductsAsync();
                    if (products.Count == 0)
                    {
                        Console.WriteLine("There are no products currently available.");
                    }
                    else
                    {
                        foreach (var product in products)
                        {
                            Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                        }
                    }
                    break;


                case "3":
                    Console.WriteLine("Enter the name of the product to edit: ");
                    string productName = Console.ReadLine();
                    Console.WriteLine("Enter the new name: ");
                    string newName = Console.ReadLine();
                    Console.WriteLine("Enter the new price: ");
                    decimal newPrice = Convert.ToDecimal(Console.ReadLine());
                    Console.WriteLine("Enter the new quantity: ");
                    int newQuantity = Convert.ToInt32(Console.ReadLine());
                    await productRepository.UpdateProductAsync(productName, newName, newPrice, newQuantity);
                    Console.WriteLine("Product updated successfully.");
                    break;

                case "4":
                    Console.WriteLine("Enter the name of the product to delete: ");
                    string productToDelete = Console.ReadLine();
                    await productRepository.DeleteProductAsync(productToDelete);
                    Console.WriteLine("Product deleted successfully.");
                    break;

                case "5":
                    Console.WriteLine("Enter the name of the product to search: ");
                    string productToSearch = Console.ReadLine();
                    var searchedProducts = await productRepository.SearchProductAsync(productToSearch);
                    foreach (var product in searchedProducts)
                    {
                        Console.WriteLine($"ID: {product.ProductID}, Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
                    }
                    break;

                case "6":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Exiting...");
                    Console.ForegroundColor = ConsoleColor.White;
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
