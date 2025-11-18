using System;
using System.Collections.Generic;

namespace OnlineFoodOrderingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem> Items { get; set; }
    }
}
