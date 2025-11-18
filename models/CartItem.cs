namespace OnlineFoodOrderingSystem.Models
{
    public class CartItem
    {
        public int FoodItemId { get; set; }
        public string FoodName { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string RestaurantName { get; set; } = "";
    }
}
