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

        public void EditProduct(string productName)
        {
            var product = FindProduct(productName);
            if (product != null)
            {
                DisplayProductDetails(product);
                UpdateProductDetails(product);
            }
            else
            {
                Console.WriteLine($"Product with name '{productName}' was not found.");
            }
        }

        private Product FindProduct(string productName)
        {
            return products.Find(p => p.Name == productName);
        }

        private void DisplayProductDetails(Product product)
        {
            Console.WriteLine($"Product found: {product.Name} - Price: ${product.Price} - Quantity: {product.Quantity}");
            Console.WriteLine("Enter new details:");
        }

        private void UpdateProductDetails(Product product)
        {
            string newName = GetNewNameFromUser();
            if (!string.IsNullOrEmpty(newName))
            {
                product.Name = newName;
            }

            decimal newPrice = GetNewPriceFromUser();
            product.Price = newPrice;

            int newQuantity = GetNewQuantityFromUser();
            product.Quantity = newQuantity;
        }

        private string GetNewNameFromUser()
        {
            Console.Write("New name : ");
            return Console.ReadLine();
        }

        private decimal GetNewPriceFromUser()
        {
            decimal newPrice;
            Console.Write("New price: ");
            string newPriceStr = Console.ReadLine();
            while (!decimal.TryParse(newPriceStr, out newPrice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid price format. Please enter a valid price:");
                Console.ForegroundColor = ConsoleColor.White;
                newPriceStr = Console.ReadLine();
            }
            return newPrice;
        }

        private int GetNewQuantityFromUser()
        {
            int newQuantity;
            Console.Write("New quantity: ");
            string newQuantityStr = Console.ReadLine();
            while (!int.TryParse(newQuantityStr, out newQuantity))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid quantity format. Please enter a valid quantity:");
                Console.ForegroundColor = ConsoleColor.White;
                newQuantityStr = Console.ReadLine();
            }
            return newQuantity;
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

        public void SearchProduct(string productName)
        {
            var product = FindProduct(productName);
            if (product != null)
            {
                Console.WriteLine($"Product found: {product.Name} - Price: ${product.Price} - Quantity: {product.Quantity}");
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