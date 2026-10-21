namespace RestaurantManagementSystem.Exceptions;

public class BusinessRule : Exception
{
    public BusinessRule(string message) : base(message)
    {
    }
}