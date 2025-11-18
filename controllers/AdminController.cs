using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodOrderingSystem.Data;
using OnlineFoodOrderingSystem.Models;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        private bool IsAdmin()
        {
            var role = HttpContext.Session.GetString("UserRole");
            return role == "Admin";
        }

        public IActionResult Index()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.RestaurantCount = _db.Restaurants.Count();
            ViewBag.FoodCount = _db.FoodItems.Count();
            ViewBag.OrderCount = _db.Orders.Count();
            return View();
        }

        // Restaurants
        public IActionResult Restaurants()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var list = _db.Restaurants.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateRestaurant()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public IActionResult CreateRestaurant(Restaurant restaurant)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _db.Restaurants.Add(restaurant);
                _db.SaveChanges();
                return RedirectToAction("Restaurants");
            }
            return View(restaurant);
        }

        // Food Items
        public IActionResult FoodItems()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var list = _db.FoodItems.Include(f => f.Restaurant).Include(f => f.Category).ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateFoodItem()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            ViewBag.Restaurants = _db.Restaurants.ToList();
            ViewBag.Categories = _db.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CreateFoodItem(FoodItem item)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _db.FoodItems.Add(item);
                _db.SaveChanges();
                return RedirectToAction("FoodItems");
            }

            ViewBag.Restaurants = _db.Restaurants.ToList();
            ViewBag.Categories = _db.Categories.ToList();
            return View(item);
        }

        public IActionResult Orders()
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var orders = _db.Orders.Include(o => o.Items).ThenInclude(i => i.FoodItem).ToList();
            return View(orders);
        }

        [HttpPost]
        public IActionResult UpdateOrderStatus(int id, string status)
        {
            if (!IsAdmin())
                return RedirectToAction("Login", "Account");

            var order = _db.Orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = status;
                _db.SaveChanges();
            }
            return RedirectToAction("Orders");
        }
    }
}
