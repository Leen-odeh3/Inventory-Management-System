using Microsoft.Extensions.Configuration;
using Simple_Inventory_Management_System.Repositories;

namespace Simple_Inventory_Management_System;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("/appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = config.GetConnectionString("Default");

        var productRepository = new ProductRepository(connectionString);

        try
        {
            Console.WriteLine("Welcome to the Simple Inventory Management System!");
            Console.WriteLine("Database connected successfully");
            string answer;
            do
            {
                Console.WriteLine("\nSelect from the options\n1.Add Product\n2.Display Products\n3.Edit Product\n4.Delete Product\n5.Search Product");
                var choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // Add Product
                        Console.WriteLine("Enter item name:");
                        var itemName = Console.ReadLine();
                        Console.WriteLine("Enter item price:");
                        while (!decimal.TryParse(Console.ReadLine(), out var itemPrice) || itemPrice < 0)
                        {
                            Console.WriteLine("Invalid price. Please enter a positive number.");
                        }
                        Console.WriteLine("Enter item quantity:");
                       
                        while (!int.TryParse(Console.ReadLine(), out var itemQuantity) || itemQuantity < 0)
                        {
                            Console.WriteLine("Invalid quantity. Please enter a positive integer.");
                        }

                        await productRepository.AddProductAsync(itemName, itemPrice, itemQuantity);
                        break;

                    case 2:
                        // Display Products
                        await productRepository.DisplayProductsAsync();
                        break;

                    case 3:
                        // Edit Product
                        Console.WriteLine("Enter the name of the product to be updated:");
                        string productNameToUpdate = Console.ReadLine();
                        Console.WriteLine("Enter the new name:");
                        string newName = Console.ReadLine();
                        Console.WriteLine("Enter the new price:");
                        decimal newPrice = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Enter the new quantity:");
                        int newQuantity = int.Parse(Console.ReadLine());

                        await productRepository.EditProductAsync(productNameToUpdate, newName, newPrice, newQuantity);
                        break;

                    case 4:
                        // Delete Product by ID
                        Console.WriteLine("Enter the ID of the product to be deleted:");
                        int productIdToDelete;
                        while (!int.TryParse(Console.ReadLine(), out productIdToDelete) || productIdToDelete < 0)
                        {
                            Console.WriteLine("Invalid ID. Please enter a positive number.");
                        }

                        await productRepository.DeleteProductAsync(productIdToDelete);
                        break;


                    case 5:
                        // Search Product
                        Console.WriteLine("Enter the name of the product to search:");
                        string productNameToSearch = Console.ReadLine();

                        await productRepository.SearchProductAsync(productNameToSearch);
                        break;

                    default:
                        Console.WriteLine("Please enter a valid choice");
                        break;
                }
                Console.WriteLine("\nDo you want to continue? (Yes/No)");
                answer = Console.ReadLine();
            } while (string.Equals(answer, "YES", StringComparison.OrdinalIgnoreCase) || string.Equals(answer, "Y", StringComparison.OrdinalIgnoreCase));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
