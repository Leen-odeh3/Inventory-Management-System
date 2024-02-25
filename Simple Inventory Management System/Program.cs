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

                        decimal price;
                        while (!decimal.TryParse(Console.ReadLine(), out price))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid decimal value for the price.");
                            Console.Write("Enter product price: ");
                        }
                        int quantity;
                        Console.Write("Enter product quantity: ");
                        while (!int.TryParse(Console.ReadLine(), out quantity))
                        {
                            Console.WriteLine("Invalid input. Please enter a valid integer value for the quantity.");
                            Console.Write("Enter product quantity: ");
                        }
                        inventory.AddProduct(name, price, quantity);
                        inventory.DisplaySuccessMessage();
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}