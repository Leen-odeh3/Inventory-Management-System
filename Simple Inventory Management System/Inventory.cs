using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Inventory_Management_System
{
   public class Inventory
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(string name, decimal price, int quantity)
        {
            Product product = new Product { Name = name, Price = price, Quantity = quantity };
            products.Add(product);
        }

        public void DisplaySuccessMessage()
        {
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

    }
}