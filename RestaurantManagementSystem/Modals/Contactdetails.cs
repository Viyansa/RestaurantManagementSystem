namespace RestaurantManagementSystem.Models;

public abstract class Contactdetails
{
    public string FullName { get; set; } = "";
    public string PhoneNumber { get; set; } = "";

    public abstract string GetDetails();
}