using System;
namespace RestaurantManagementSystem.Models;

public class RestaurantData
{
    public List<MenuItem> MenuItems { get; set; } = new();
    public List<Customer> Customers { get; set; } = new();
    public List<RestaurantOrder> Orders { get; set; } = new();
    
}
