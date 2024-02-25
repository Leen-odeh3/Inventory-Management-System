using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System
{
    class Inventory
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(string name, decimal price, int quantity)
        {
            Product product = new Product { Name = name, Price = price, Quantity = quantity };
            products.Add(product);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Product added successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Inventory:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} - Price: ${products[i].Price} - Quantity: {products[i].Quantity}");
            }
        }

        public void EditProduct(string productName)
        {
            Product product = products.Find(p => p.Name == productName);
            if (product != null)
            {
                Console.WriteLine($"Product found: {product.Name} - Price: ${product.Price} - Quantity: {product.Quantity}");
                Console.WriteLine("Enter new details:");

                Console.Write("New name (leave blank to keep unchanged): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    product.Name = newName;
                }

                Console.Write("New price : ");
                string newPriceStr = Console.ReadLine();
                if (!string.IsNullOrEmpty(newPriceStr))
                {
                    decimal newPrice;
                    if (decimal.TryParse(newPriceStr, out newPrice))
                    {
                        product.Price = newPrice;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid price format. Price not updated.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }

            }
        }

        public void DeleteProduct(string productName)
        {
            var product = FindProduct(productName);
            if (product != null)
            {
                products.Remove(product);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}