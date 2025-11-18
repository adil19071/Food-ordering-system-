using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineFoodOrderingSystem.Data;
using OnlineFoodOrderingSystem.Models;
using Microsoft.AspNetCore.Http;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private const string CartSessionKey = "Cart";

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(cartJson))
                return new List<CartItem>();

            return JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var json = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        public IActionResult Add(int id)
        {
            var item = _db.FoodItems.FirstOrDefault(f => f.Id == id);
            if (item == null)
                return NotFound();

            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.FoodItemId == id);
            if (existing == null)
            {
                cart.Add(new CartItem
                {
                    FoodItemId = item.Id,
                    FoodName = item.Name,
                    Price = item.Price,
                    Quantity = 1,
                    RestaurantName = item.Restaurant?.Name ?? ""
                });
            }
            else
            {
                existing.Quantity += 1;
            }

            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(c => c.FoodItemId == id);
            if (existing != null)
            {
                cart.Remove(existing);
                SaveCart(cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            SaveCart(new List<CartItem>());
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userName = HttpContext.Session.GetString("UserName");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction("Index");
            }

            var order = new Order
            {
                UserId = userId.Value,
                UserName = userName ?? "",
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalAmount = cart.Sum(c => c.Price * c.Quantity),
                Items = cart.Select(c => new OrderItem
                {
                    FoodItemId = c.FoodItemId,
                    Quantity = c.Quantity,
                    UnitPrice = c.Price
                }).ToList()
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            Clear();

            return RedirectToAction("Details", "Orders", new { id = order.Id });
        }
    }
}
