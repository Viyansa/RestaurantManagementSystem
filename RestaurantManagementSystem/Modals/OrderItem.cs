
namespace RestaurantManagementSystem.Models
{
    public class OrderItem
    {
        public int MenuItemId { get; set; }
        public string ItemNameSnapshot { get; set; } = "";
        public string CategorySnapshot { get; set; } = "";
        public decimal UnitPriceSnapshot { get; set; }
        public int Quantity { get; set; }

        public decimal LineTotal
        {
            get
            {
                return UnitPriceSnapshot * Quantity;
            }
        }
    }
}
