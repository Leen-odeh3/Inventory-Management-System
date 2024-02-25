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
            Console.WriteLine("Enter new details (leave blank to keep unchanged):");
        }

        private void UpdateProductDetails(Product product)
        {
            string newName = GetNewNameFromUser();
            if (!string.IsNullOrEmpty(newName))
            {
                product.Name = newName;
            }

            string newPriceStr = GetNewPriceFromUser();
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

            string newQuantityStr = GetNewQuantityFromUser();
            if (!string.IsNullOrEmpty(newQuantityStr))
            {
                int newQuantity;
                if (int.TryParse(newQuantityStr, out newQuantity))
                {
                    product.Quantity = newQuantity;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid quantity format. Quantity not updated.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        private string GetNewNameFromUser()
        {
            Console.Write("New name : ");
            return Console.ReadLine();
        }

        private string GetNewPriceFromUser()
        {
            Console.Write("New price: ");
            return Console.ReadLine();
        }

        private string GetNewQuantityFromUser()
        {
            Console.Write("New quantity: ");
            return Console.ReadLine();
        }


    }
}