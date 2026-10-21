
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public class RestaurantService
    {
        public List<RestaurantOrder> Orders =
            new List<RestaurantOrder>();

        private readonly MenuService menuService;
        private readonly CustomerService customerService;

        public RestaurantService(
            MenuService menu, CustomerService customer)
        {
            menuService = menu;
            customerService = customer;
        }

        public void CreateOrder(int orderId, int customerId)
        {
            if (orderId <= 0 ||
                Orders.Any(o => o.OrderId == orderId))
            {
                Console.WriteLine("Invalid or duplicate order ID.");
                return;
            }

            Customer? customer = customerService.Customers
                .FirstOrDefault(c => c.CustomerId == customerId);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            RestaurantOrder order = new RestaurantOrder
            {
                OrderId = orderId,
                CustomerId = customerId,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Placed
            };

            string choice = "";

            do
            {
                menuService.DisplayMenu();

                Console.Write("Enter menu item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int itemId))
                {
                    Console.WriteLine("Invalid item ID.");
                    choice = "no";
                    continue;
                }

                Console.Write("Enter quantity: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Invalid quantity.");
                    choice = "no";
                    continue;
                }

                MenuItem? item = menuService.MenuItems
                    .FirstOrDefault(m => m.ItemId == itemId && m.IsAvailable);

                if (item != null && quantity > 0)
                {
                    order.Items.Add(new OrderItem
                    {
                        MenuItemId = item.ItemId,
                        ItemNameSnapshot = item.Name,
                        CategorySnapshot = item.Category,
                        UnitPriceSnapshot = item.Price,
                        Quantity = quantity
                    });

                    Console.WriteLine("Item added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid item or quantity.");
                }

                Console.Write("Add another item? (yes/no): ");
                choice = (Console.ReadLine() ?? "").Trim().ToLower();

            } while (choice == "yes");

            if (order.Items.Count == 0)
            {
                Console.WriteLine("Order not created because it has no items.");
                return;
            }

            Orders.Add(order);

            Console.WriteLine("Order created successfully.");
            Console.WriteLine("Total amount: " + CalculateTotal(order));
        }

        public decimal CalculateTotal(RestaurantOrder order)
        {
            decimal total = 0;

            foreach (OrderItem item in order.Items)
            {
                total += item.UnitPriceSnapshot * item.Quantity;
            }

            return total;
        }

        public void DisplayOrders()
        {
            if (Orders.Count == 0)
            {
                Console.WriteLine("No orders available.");
                return;
            }

            foreach (RestaurantOrder order in Orders)
            {
                Console.WriteLine(
                    "Order ID: " + order.OrderId +
                    " | Customer ID: " + order.CustomerId +
                    " | Status: " + order.Status +
                    " | Total: " + CalculateTotal(order));
            }
        }

        public void UpdateOrderStatus(int id, OrderStatus status)
        {
            RestaurantOrder? order =
                Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                Console.WriteLine("Order not found.");
                return;
            }

            if (order.Status == OrderStatus.Served ||
                order.Status == OrderStatus.Cancelled)
            {
                Console.WriteLine("This order cannot be changed.");
                return;
            }

            if (status == OrderStatus.Preparing && order.Items.Count == 0)
            {
                Console.WriteLine("Add items before preparing the order.");
                return;
            }

            bool valid =
                (order.Status == OrderStatus.Placed &&
                 status == OrderStatus.Preparing) ||
                (order.Status == OrderStatus.Preparing &&
                 status == OrderStatus.Ready) ||
                (order.Status == OrderStatus.Ready &&
                 status == OrderStatus.Served) ||
                status == OrderStatus.Cancelled;

            if (!valid)
            {
                Console.WriteLine("Invalid status change.");
                return;
            }

            order.Status = status;
            Console.WriteLine("Order status updated successfully.");
        }

        public void ShowServedSales()
        {
            decimal total = Orders
                .Where(o => o.Status == OrderStatus.Served)
                .Sum(o => CalculateTotal(o));

            Console.WriteLine("Total served sales: " + total);
        }
    }
}
