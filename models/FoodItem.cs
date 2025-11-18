using System;
using System.Collections.Generic;

namespace OnlineFoodOrderingSystem.Models
{
    public class FoodItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";
        public decimal Price { get; set; }

        public string Description { get; set; } = "";
        public string ImageUrl { get; set; } = "";

        public int RestaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
