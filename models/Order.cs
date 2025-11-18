using System;
using System.Collections.Generic;

namespace OnlineFoodOrderingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; } = "";

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
