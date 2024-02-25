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
    }
}