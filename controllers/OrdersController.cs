using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodOrderingSystem.Data;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrdersController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult MyOrders()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var orders = _db.Orders
                .Include(o => o.Items)!
                .ThenInclude(i => i.FoodItem)
                .Where(o => o.UserId == userId.Value)
                .OrderByDescending(o => o.OrderDate)
                .ToList();

            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = _db.Orders
                .Include(o => o.Items)!
                .ThenInclude(i => i.FoodItem)
                .FirstOrDefault(o => o.Id == id);

            if (order == null)
                return NotFound();

            return View(order);
        }
    }
}
