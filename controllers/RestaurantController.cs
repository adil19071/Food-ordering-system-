using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineFoodOrderingSystem.Data;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RestaurantController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Details(int id)
        {
            var restaurant = _db.Restaurants
                .Include(r => r.FoodItems)
                .ThenInclude(f => f.Category)
                .FirstOrDefault(r => r.Id == id);

            if (restaurant == null)
                return NotFound();

            return View(restaurant);
        }
    }
}
