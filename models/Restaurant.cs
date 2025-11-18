namespace OnlineFoodOrderingSystem.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Description { get; set; } = "";
        public ICollection<FoodItem>? FoodItems { get; set; }
    }
}
