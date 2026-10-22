
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public class RestaurantService
    {
        public List<RestaurantOrder> Orders { get; set; } =
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

            while (true)
            {
                menuService.DisplayMenu();

                Console.Write("Enter menu item ID: ");
                if (!int.TryParse(Console.ReadLine(), out int itemId))
                {
                    Console.WriteLine("Invalid item ID.");
                    continue;
                }

                MenuItem? item = menuService.MenuItems
                    .FirstOrDefault(m => m.ItemId == itemId && m.IsAvailable);

                if (item == null)
                {
                    Console.WriteLine("Menu item not found or unavailable.");
                }
                else
                {
                    Console.Write("Enter quantity: ");

                    if (!int.TryParse(Console.ReadLine(), out int quantity) ||
                        quantity <= 0)
                    {
                        Console.WriteLine("Enter a valid positive quantity.");
                    }
                    else
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
                }

                Console.Write("Add another item? (yes/no): ");
                string answer = (Console.ReadLine() ?? "").Trim();

                if (!answer.Equals("yes", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
            }

            if (order.Items.Count == 0)
            {
                Console.WriteLine("Order not created because it has no items.");
                return;
            }

            Orders.Add(order);

            Console.WriteLine("Order created successfully.");
            Console.WriteLine("Customer: " + customer.FullName);
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
                    $"Order ID: {order.OrderId} | " +
                    $"Customer ID: {order.CustomerId} | " +
                    $"Date: {order.CreatedAt} | Status: {order.Status} | " +
                    $"Total: {CalculateTotal(order)}");

                foreach (OrderItem item in order.Items)
                {
                    Console.WriteLine(
                        $"  {item.ItemNameSnapshot} x {item.Quantity} = " +
                        $"{item.LineTotal}");
                }
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
            decimal total = 0;

            foreach (RestaurantOrder order in Orders)
            {
                if (order.Status == OrderStatus.Served)
                {
                    total += CalculateTotal(order);
                }
            }

            Console.WriteLine("Total served sales: " + total);
        }
    }
}
