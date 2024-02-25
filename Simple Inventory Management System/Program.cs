using System;

namespace Simple_Inventory_Management_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            while (true)
            {
                Console.WriteLine("\n1. Add Product");

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
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter product quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        inventory.AddProduct(name, price, quantity);
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}