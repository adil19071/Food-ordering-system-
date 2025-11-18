using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodOrderingSystem.Data;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var restaurants = _db.Restaurants.Include(r => r.FoodItems).ToList();
            return View(restaurants);
        }
    }
}
