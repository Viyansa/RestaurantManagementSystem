namespace RestaurantManagementSystem.Models;

public class CustomerDetails : Contactdetails
{
    public override string GetDetails()
    {
        return $"Walk-in customer | {FullName} | {PhoneNumber}";
    }
}