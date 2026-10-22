
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public class MenuService
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

        public void AddMenuItem(MenuItem item)
        {
            if (item.ItemId <= 0 ||
                MenuItems.Any(m => m.ItemId == item.ItemId))
            {
                Console.WriteLine("Invalid or duplicate item ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(item.Name) ||
                string.IsNullOrWhiteSpace(item.Category) ||
                item.Price < 0)
            {
                Console.WriteLine("Enter valid menu details.");
                return;
            }

            MenuItems.Add(item);
            Console.WriteLine("Menu item added successfully.");
        }

        public void DisplayMenu()
        {
            if (MenuItems.Count == 0)
            {
                Console.WriteLine("No menu items available.");
                return;
            }

            foreach (MenuItem item in MenuItems)
            {
                Console.WriteLine(
                    $"ID: {item.ItemId} | Name: {item.Name} | " +
                    $"Category: {item.Category} | Price: {item.Price} | " +
                    $"Available: {item.IsAvailable}");
            }
        }

        public void SearchMenuItem(string name)
        {
            var results = MenuItems
                .Where(m => m.Name.Contains(
                    name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("Menu item not found.");
                return;
            }

            foreach (MenuItem item in results)
            {
                Console.WriteLine(
                    $"{item.ItemId} | {item.Name} | " +
                    $"{item.Category} | {item.Price}");
            }
        }

        public void UpdateMenuItem(
            int id, string name, string category, decimal price)
        {
            MenuItem? item = MenuItems.FirstOrDefault(m => m.ItemId == id);

            if (item == null)
            {
                Console.WriteLine("Menu item not found.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(category) || price < 0)
            {
                Console.WriteLine("Invalid menu details.");
                return;
            }

            item.Name = name;
            item.Category = category;
            item.Price = price;

            Console.WriteLine("Menu item updated successfully.");
        }

        public void DeleteMenuItem(int id)
        {
            MenuItem? item = MenuItems.FirstOrDefault(m => m.ItemId == id);

            if (item == null)
            {
                Console.WriteLine("Menu item not found.");
                return;
            }

            MenuItems.Remove(item);
            Console.WriteLine("Menu item deleted successfully.");
        }
    }
}
