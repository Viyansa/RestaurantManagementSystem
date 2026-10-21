
using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManagementSystem.Models;

namespace RestaurantManagementSystem.Services
{
    public class CustomerService
    {
        public List<Customer> Customers = new List<Customer>();

        public void AddCustomer(Customer customer)
        {
            if (customer.CustomerId <= 0 ||
                Customers.Any(c => c.CustomerId == customer.CustomerId))
            {
                Console.WriteLine("Invalid or duplicate customer ID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(customer.FullName) ||
                string.IsNullOrWhiteSpace(customer.PhoneNumber))
            {
                Console.WriteLine("Name and phone are required.");
                return;
            }

            Customers.Add(customer);
            Console.WriteLine("Customer added successfully.");
        }

        public void DisplayCustomers()
        {
            if (Customers.Count == 0)
            {
                Console.WriteLine("No customers available.");
                return;
            }

            Console.WriteLine("\n--- Customer Details ---");

            foreach (Customer customer in Customers)
            {
                Console.WriteLine(
                    "ID: " + customer.CustomerId +
                    " | Name: " + customer.FullName +
                    " | Phone: " + customer.PhoneNumber +
                    " | Email: " + customer.Email);
            }
        }

        public void SearchCustomer(int id)
        {
            Customer? customer =
                Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Console.WriteLine(
                customer.CustomerId + " | " +
                customer.FullName + " | " +
                customer.PhoneNumber + " | " +
                customer.Email);
        }

        public void UpdateCustomer(
            int id, string name, string phone, string email)
        {
            Customer? customer =
                Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(phone))
            {
                Console.WriteLine("Name and phone are required.");
                return;
            }

            customer.FullName = name;
            customer.PhoneNumber = phone;
            customer.Email = email;

            Console.WriteLine("Customer updated successfully.");
        }

        public void DeleteCustomer(int id)
        {
            Customer? customer =
                Customers.FirstOrDefault(c => c.CustomerId == id);

            if (customer == null)
            {
                Console.WriteLine("Customer not found.");
                return;
            }

            Customers.Remove(customer);
            Console.WriteLine("Customer deleted successfully.");
        }
    }
}
