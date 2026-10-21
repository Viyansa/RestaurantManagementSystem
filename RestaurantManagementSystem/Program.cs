
using System;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Services;

namespace RestaurantManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuService menuService = new MenuService();
            CustomerService customerService = new CustomerService();

            RestaurantService restaurantService =
                new RestaurantService(menuService, customerService);

            int choice = -1;

            while (choice != 0)
            {
                Console.WriteLine("\n===== RESTAURANT MANAGEMENT SYSTEM =====");
                Console.WriteLine("1. Add Menu");
                Console.WriteLine("2. View Menu");
                Console.WriteLine("3. Search Menu");
                Console.WriteLine("4. Update Menu");
                Console.WriteLine("5. Delete Menu");
                Console.WriteLine("6. Add Customer");
                Console.WriteLine("7. View Customers");
                Console.WriteLine("8. Search Customer");
                Console.WriteLine("9. Update Customer");
                Console.WriteLine("10. Delete Customer");
                Console.WriteLine("11. Create Order");
                Console.WriteLine("12. View Orders");
                Console.WriteLine("13. Update Order Status");
                Console.WriteLine("14. Show Served Sales");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Enter a valid number.");
                    choice = -1;
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        MenuItem item = new MenuItem();

                        Console.Write("Enter Item ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int itemId))
                        {
                            Console.WriteLine("Invalid item ID.");
                            break;
                        }
                        item.ItemId = itemId;

                        Console.Write("Enter Item Name: ");
                        item.Name = Console.ReadLine() ?? "";

                        Console.Write("Enter Category: ");
                        item.Category = Console.ReadLine() ?? "";

                        Console.Write("Enter Price: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.WriteLine("Invalid price.");
                            break;
                        }

                        item.Price = price;
                        item.IsAvailable = true;
                        menuService.AddMenuItem(item);
                        break;

                    case 2:
                        menuService.DisplayMenu();
                        break;

                    case 3:
                        Console.Write("Enter item name to search: ");
                        menuService.SearchMenuItem(Console.ReadLine() ?? "");
                        break;

                    case 4:
                        Console.Write("Enter Item ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int updateItemId))
                        {
                            Console.WriteLine("Invalid item ID.");
                            break;
                        }

                        Console.Write("Enter new name: ");
                        string newName = Console.ReadLine() ?? "";

                        Console.Write("Enter new category: ");
                        string newCategory = Console.ReadLine() ?? "";

                        Console.Write("Enter new price: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal newPrice))
                        {
                            Console.WriteLine("Invalid price.");
                            break;
                        }

                        menuService.UpdateMenuItem(
                            updateItemId, newName, newCategory, newPrice);
                        break;

                    case 5:
                        Console.Write("Enter Item ID: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteItemId))
                            menuService.DeleteMenuItem(deleteItemId);
                        else
                            Console.WriteLine("Invalid item ID.");
                        break;

                    case 6:
                        Customer customer = new Customer();

                        Console.Write("Enter Customer ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int customerId))
                        {
                            Console.WriteLine("Invalid customer ID.");
                            break;
                        }
                        customer.CustomerId = customerId;

                        Console.Write("Enter Customer Name: ");
                        customer.FullName = Console.ReadLine() ?? "";

                        Console.Write("Enter Phone Number: ");
                        customer.PhoneNumber = Console.ReadLine() ?? "";

                        Console.Write("Enter Email: ");
                        customer.Email = Console.ReadLine() ?? "";

                        customerService.AddCustomer(customer);
                        break;

                    case 7:
                        customerService.DisplayCustomers();
                        break;

                    case 8:
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int searchCustomerId))
                            customerService.SearchCustomer(searchCustomerId);
                        else
                            Console.WriteLine("Invalid customer ID.");
                        break;

                    case 9:
                        Console.Write("Enter Customer ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int editCustomerId))
                        {
                            Console.WriteLine("Invalid customer ID.");
                            break;
                        }

                        Console.Write("Enter new name: ");
                        string editName = Console.ReadLine() ?? "";

                        Console.Write("Enter new phone: ");
                        string editPhone = Console.ReadLine() ?? "";

                        Console.Write("Enter new email: ");
                        string editEmail = Console.ReadLine() ?? "";

                        customerService.UpdateCustomer(
                            editCustomerId, editName, editPhone, editEmail);
                        break;

                    case 10:
                        Console.Write("Enter Customer ID: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteCustomerId))
                            customerService.DeleteCustomer(deleteCustomerId);
                        else
                            Console.WriteLine("Invalid customer ID.");
                        break;

                    case 11:
                        Console.Write("Enter Order ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int orderId))
                        {
                            Console.WriteLine("Invalid order ID.");
                            break;
                        }

                        Console.Write("Enter Customer ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int orderCustomerId))
                        {
                            Console.WriteLine("Invalid customer ID.");
                            break;
                        }

                        restaurantService.CreateOrder(orderId, orderCustomerId);
                        break;

                    case 12:
                        restaurantService.DisplayOrders();
                        break;

                    case 13:
                        Console.Write("Enter Order ID: ");
                        if (!int.TryParse(Console.ReadLine(), out int statusOrderId))
                        {
                            Console.WriteLine("Invalid order ID.");
                            break;
                        }

                        Console.WriteLine("1. Placed");
                        Console.WriteLine("2. Preparing");
                        Console.WriteLine("3. Ready");
                        Console.WriteLine("4. Served");
                        Console.WriteLine("5. Cancelled");
                        Console.Write("Choose status: ");

                        if (int.TryParse(Console.ReadLine(), out int statusChoice) &&
                            statusChoice >= 1 && statusChoice <= 5)
                        {
                            restaurantService.UpdateOrderStatus(
                                statusOrderId,
                                (OrderStatus)(statusChoice - 1));
                        }
                        else
                        {
                            Console.WriteLine("Invalid status.");
                        }
                        break;

                    case 14:
                        restaurantService.ShowServedSales();
                        break;

                    case 0:
                        Console.WriteLine("Thank you!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }
    }
}
